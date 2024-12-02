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
    private List<SimpleUIPanelMobiles> Panels;


    [SerializeField]
    private float transitionDelay = 0.5f; 

    [SerializeField]
    private bool isTransitioning = false; 


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
        SwitchPanel(firstPanel);
    }
    public void SwitchPanel(SimpleUIPanelMobiles newPanel)
    {
        if (isTransitioning) return;

        if (currentPanel != null)
        {
            previousPanel = currentPanel;
            previousPanel.DisablePanel();
        }

        currentPanel = newPanel;
        StartCoroutine(EnablePanelAfterDelay());
    }

    private IEnumerator EnablePanelAfterDelay()
    {
        isTransitioning = true;
        yield return new WaitForSecondsRealtime(transitionDelay);
        currentPanel.EnablePanel();
        isTransitioning = false;
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
