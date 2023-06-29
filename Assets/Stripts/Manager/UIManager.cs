using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIManager : BaseManager<UIManager> 
{
    [SerializeField] private MenuPanel menuPanel;
    public MenuPanel MenuPanel => menuPanel;

    [SerializeField] private GamePanel gamePanel;
    public GamePanel GamePanel => gamePanel;

    [SerializeField] private SettingPanel settingPanel;
    public SettingPanel SettingPanel => settingPanel;

    [SerializeField] private LoadingPanel loadingPanel;
    public LoadingPanel LoadingPanel => loadingPanel;

    [SerializeField] private LosePanel losePanel;
    public LosePanel LosePanel => losePanel;

   

    [SerializeField] private PausePanel pausePanel;
    public PausePanel PausePanel => pausePanel;
    private bool isplaying = true;
    protected override void Awake()
    {
        base.Awake();
        isplaying = GameManager.Instance.Isplaying;
    }
    void Start()
    {
        ActiveMenuPanel(true);
        ActiveGamePanel(false);
        ActiveSettingPanel(false);
        ActiveLoadingPanel(false);
        ActiveLosingPanel(false);
        ActivePausePanel(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameManager.HasInstance && GameManager.Instance.Isplaying)
            {
                GameManager.Instance.PauseGame();
                isplaying = false;
                ActivePausePanel(true);
            }
        }
    }
    public void ActiveMenuPanel(bool active)
    {
        menuPanel.gameObject.SetActive(active);
    }
    public void ActiveGamePanel(bool active)
    {
        gamePanel.gameObject.SetActive(active);
    }
    public void ActiveSettingPanel(bool active)
    {
        settingPanel.gameObject.SetActive(active);
    }
    public void ActiveLoadingPanel(bool active)
    {
        loadingPanel.gameObject.SetActive(active);
    }
    public void ActiveLosingPanel(bool active)
    {
        losePanel.gameObject.SetActive(active);
    }
    public void ActivePausePanel(bool active)
    {
        pausePanel.gameObject.SetActive(active);
    }
}
