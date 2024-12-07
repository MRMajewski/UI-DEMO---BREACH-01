using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleUIPanelMobilesManager : MonoBehaviour
{
    [SerializeField]
    private SimpleUIPanelMobiles firstPanel;

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
    public float TransitionTime { get=>transitionTime; }

    [SerializeField]
    private bool isTransitioning = false;

    [Header("Transition Elements")]
    [SerializeField] private RectTransform leftCurtain;  
    [SerializeField] private RectTransform rightCurtain; 

    public enum SimplePanelNames
    {
        Welcome,
        Main,
        Dice         
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
        InitSimpleUIPanels();
    }

    private void InitSimpleUIPanels()
    {
        isTransitioning = true;
        currentPanel = firstPanel;
        currentPanel.EnablePanel();
        isTransitioning = false;

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
        StartCoroutine(EnablePanelAfterDelay());
    }

    public void SwitchPanel(string name)
    {
        Enum.TryParse(name, out SimplePanelNames result);


        foreach (var panel in  SimplePanels)
        {
            if(panel.panelName== result)
            {
                SwitchPanel(panel.simplePanel);
                break;
            }
        }
    }

    private IEnumerator EnablePanelAfterDelay()
    {
        isTransitioning = true;
        yield return new WaitForSecondsRealtime(transitionTime);
        currentPanel.EnablePanel();
        isTransitioning = false;
    }

    public void PlayCurtainScaleAnimation()
    {
        leftCurtain.localScale = new Vector3(0, 1, 1);
        rightCurtain.localScale = new Vector3(0, 1, 1);

        Sequence curtainSequence = DOTween.Sequence();

        curtainSequence
            .Append(leftCurtain.DOScaleX(1, transitionTime).SetEase(Ease.InOutQuad))
            .Join(rightCurtain.DOScaleX(1, transitionTime).SetEase(Ease.InOutQuad))

            .AppendInterval(transitionTime / 2)

            .Append(leftCurtain.DOScaleX(0, transitionTime).SetEase(Ease.InOutQuad))
            .Join(rightCurtain.DOScaleX(0, transitionTime).SetEase(Ease.InOutQuad));

          //  .OnComplete(() => Debug.Log("Animacja kurtyn zako�czona!"));
    }

    private void HandleBackButton()
    {
        if (currentPanel == firstPanel)
        {
            Application.Quit();
            return;
        }

        if (currentPanel != null && previousPanel != null)
        {
            SwitchPanel(previousPanel);
        }
    }
}
