using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleUIPanelMobilesManager : MonoBehaviour
{
    [SerializeField]
    private SimpleUIPanelMobiles firstPanel;
    [SerializeField]
    private SimpleUIPanelMobiles panelBeforeApplicationQuit;

    public static SimpleUIPanelMobilesManager Instance { get; private set; }

    [SerializeField]
    private SimpleUIPanelMobiles currentPanel;


    [SerializeField]
    private SimpleUIPanelMobiles previousPanel;
    public SimpleUIPanelMobiles CurrentPanel { get; }
    public SimpleUIPanelMobiles PreviousPanel { get; }

    [SerializeField]
    private List<Panels> SimplePanels;

    [System.Serializable]
    public struct Panels
    {
        [SerializeField]
        public SimplePanelNames panelName;
        [SerializeField]
        public SimpleUIPanelMobiles simplePanel;
    }

    [SerializeField]
    private float transitionTime = 0.5f;
    public float TransitionTime { get => transitionTime; }

    [SerializeField]
    private bool isTransitioning = false;

    [Header("Tween Animation Elements")]
    [SerializeField] private RectTransform leftCurtain;
    [SerializeField] private RectTransform rightCurtain;

    private Vector2 leftStartPos;
    private Vector2 rightStartPos;
    private Vector2 leftTargetPos;
    private Vector2 rightTargetPos;

    [Space]
    [SerializeField] private CanvasGroup splashImage;

    [Space]
    [SerializeField] private RectTransform backgroundDecor;
    [SerializeField] private RectTransform backgroundStillDecor;
    [SerializeField] private float backGroundAnimationTime = 2f;


    [SerializeField]
    private EnumTranslatorInitializer enumTranslatorInitializer;

    public enum SimplePanelNames
    {
        Welcome,
        Main,
        Dice,
        Knowledge,
        Armory,
        Classes,
        Intro,
        Definitions,
        Neoscience,
        Trainings
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        ResizeTransitionCurtains();

        EnumTranslatorInitializer.RegisterAll();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isTransitioning)
        {
            HandleBackButton();
        }
    }
    private void Start()
    {
        StartCoroutine(InitSimpleUIPanels());
    }

    public void InitPanelsData()
    {
        foreach (var simplePanel in SimplePanels)
        {
            simplePanel.simplePanel.InitializePanel();
        }
    }
    public IEnumerator InitSimpleUIPanels()
    {
        PlaySplashAnimation();

        isTransitioning = true;
        currentPanel = firstPanel;

        foreach (var simplePanel in SimplePanels)
        {
            simplePanel.simplePanel.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame();
        
            simplePanel.simplePanel.InitializePanel();
            yield return new WaitForEndOfFrame();
            simplePanel.simplePanel.InitializePanelData();

            yield return new WaitForEndOfFrame();

            simplePanel.simplePanel.DisablePanel();
        }
        isTransitioning = false;
    }

    private void ResizeTransitionCurtains()
    {
        if(!leftCurtain.gameObject.activeSelf)
        leftCurtain.gameObject.SetActive(true);
        if (!rightCurtain.gameObject.activeSelf)
            rightCurtain.gameObject.SetActive(true);

        leftTargetPos = leftCurtain.anchoredPosition;
        rightTargetPos = rightCurtain.anchoredPosition;

        float screenWidth = Screen.width;
        leftStartPos = leftTargetPos + Vector2.left * screenWidth;
        rightStartPos = rightTargetPos + Vector2.right * screenWidth;

        leftCurtain.anchoredPosition = leftStartPos;
        rightCurtain.anchoredPosition = rightStartPos;
    }

    public void SwitchPanel(SimpleUIPanelMobiles newPanel)
    {
        if (isTransitioning) return;

        PlayCurtainScaleAnimation();

        if (currentPanel != null)
        {
            previousPanel = currentPanel;
            previousPanel.DisablePanel();
        }
        currentPanel = newPanel;
        MusicManager.Instance.PlayPanelTransitionSFX();
        StartCoroutine(EnablePanelAfterDelay());
    }

    public void SwitchPanelWithoutChangingPreviousPanel(SimpleUIPanelMobiles newPanel)
    {
        if (isTransitioning) return;

        PlayCurtainScaleAnimation();

        if (currentPanel != null)
        {
            currentPanel.DisablePanel();
        }
        currentPanel = newPanel;

        MusicManager.Instance.PlayPanelTransitionSFX();

        StartCoroutine(EnablePanelAfterDelay());
    }

    public void SwitchPanel(string name)
    {
        Enum.TryParse(name, out SimplePanelNames result);

        foreach (var panel in SimplePanels)
        {
            if (panel.panelName == result)
            {
                SwitchPanel(panel.simplePanel);
                break;
            }
        }
    }

    public void SwitchPanelWithoutChangingPreviousPanel(string name)
    {
        Enum.TryParse(name, out SimplePanelNames result);

        foreach (var panel in SimplePanels)
        {
            if (panel.panelName == result)
            {
                SwitchPanelWithoutChangingPreviousPanel(panel.simplePanel);
                break;
            }
        }
    }

    private IEnumerator EnablePanelAfterDelay()
    {
        isTransitioning = true;
        yield return new WaitForSecondsRealtime(transitionTime);
        currentPanel.EnablePanel();
        yield return new WaitForSecondsRealtime(transitionTime * 2f);
        isTransitioning = false;
    }

    public void InitBackGroundDecor()
    {
        StartCoroutine(InitBackgroundDecorCoroutine());
    }
    IEnumerator InitBackgroundDecorCoroutine()
    {
        yield return new WaitForSecondsRealtime(transitionTime*2f);
        InitBackgroundDecorMethod();
    }

    public void InitBackgroundDecorMethod()
    {
        backgroundStillDecor.gameObject.SetActive(true);

        PlayBackGroundAnimation();

            void PlayBackGroundAnimation()
        {
            float screenHeight = Screen.height;
            float objectHeight = backgroundDecor.rect.height;

            float startY = -screenHeight / 2 - objectHeight / 2;
            float endY = screenHeight / 2 + objectHeight / 2;

            backgroundDecor.anchoredPosition = new Vector2(backgroundDecor.anchoredPosition.x, startY);

            DOTween.Sequence()
                .Append(backgroundDecor.DOAnchorPosY(endY, backGroundAnimationTime).SetEase(Ease.Linear))
                .AppendCallback(() => backgroundDecor.anchoredPosition = new Vector2(backgroundDecor.anchoredPosition.x, startY))
                .SetLoops(-1);
        }
    }

    public void PlayCurtainScaleAnimation()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(leftCurtain.DOAnchorPos(leftTargetPos, transitionTime).SetEase(Ease.OutCubic));
        seq.Join(rightCurtain.DOAnchorPos(rightTargetPos, transitionTime).SetEase(Ease.OutCubic));

        seq.AppendInterval(transitionTime / 2);

        seq.Append(leftCurtain.DOAnchorPos(leftStartPos, transitionTime).SetEase(Ease.InCubic));
        seq.Join(rightCurtain.DOAnchorPos(rightStartPos, transitionTime).SetEase(Ease.InCubic));
    }

    public void PlayBackGroundAnimation()
    {
        float screenHeight = Screen.height;
        float objectHeight = backgroundDecor.rect.height;

        float startY = -screenHeight / 2 - objectHeight / 2; 
        float endY = screenHeight / 2 + objectHeight / 2; 

        backgroundDecor.anchoredPosition = new Vector2(backgroundDecor.anchoredPosition.x, startY);

        DOTween.Sequence()
            .Append(backgroundDecor.DOAnchorPosY(endY, backGroundAnimationTime).SetEase(Ease.Linear))
            .AppendCallback(() => backgroundDecor.anchoredPosition = new Vector2(backgroundDecor.anchoredPosition.x, startY))
            .SetLoops(-1);
    }

    public void PlaySplashAnimation()
    {
        if (!splashImage.gameObject.activeSelf)
            splashImage.gameObject.SetActive(true);

        Sequence splashSequence = DOTween.Sequence();

        splashSequence
            .Append(splashImage.DOFade(1, 1f).SetEase(Ease.InOutQuad))
            .AppendInterval(4f)
            .Append(splashImage.DOFade(0, 1f).SetEase(Ease.InOutQuad))
            .OnComplete(() =>
              {
                  splashImage.gameObject.SetActive(false);          
                  firstPanel.EnablePanel();
                  splashSequence.Kill();
              }
              ).SetUpdate(true);
    }  

    private void HandleBackButton()
    {
        if (currentPanel == panelBeforeApplicationQuit)
        {
            Application.Quit();
            return;
        }

        if (currentPanel != null && previousPanel != null)
        {
            SwitchPanel(previousPanel);
        }
    }

    public void ReturnToPreviousPanel()
    {
        if (!isTransitioning)
            HandleBackButton();
    }
}
