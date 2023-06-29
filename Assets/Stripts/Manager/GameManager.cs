using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : BaseManager<GameManager>
{
    [Header("Health")]
    public float startingHealth;
    public float currentHealth;

    public float curDist;
    private float distance = 0f;

    public float Distance => distance;

    private bool isplaying = true;
    private float highscore = 0f;
    public float Highscore => highscore;
    public bool Isplaying => isplaying;

    private const string HighScoreKey = "HighScore";

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        currentHealth = startingHealth;
        highscore = PlayerPrefs.GetFloat(HighScoreKey, 0f);
    }
    public void SetHealth(float health)
    {
        currentHealth = health;
    }
    public void UpdateHighscore(float value) 
    {
        if(distance < highscore)
        {
            highscore = value;
            PlayerPrefs.SetFloat(HighScoreKey, highscore);
        }
    }
    public void UpdateDistance(float value)
    {
        distance = value;
    }

    public void StartGame()
    {
        isplaying = true;
        Time.timeScale = 1f;
    }
    public void PauseGame()
    {
        isplaying = false;
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        if(!isplaying)
        {
            isplaying = true;
            Time.timeScale = 1f;
        }
    }
    public void Menu()
    {
        
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ActiveMenuPanel(true);
            UIManager.Instance.ActivePausePanel(false);
            UIManager.Instance.ActiveLosingPanel(false);
            UIManager.Instance.ActiveGamePanel(false);
        }
    }
    public void RestartGame()
    {
        ChangeScene("Menu");
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayMusic(AUDIO.Music_HEROIC_INTRUSION);
        }
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ActiveGamePanel(true);
            UIManager.Instance.ActivePausePanel(false);

        }
    }
    public void Setting()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ActiveSettingPanel(true);
            UIManager.Instance.ActivePausePanel(false);
        }
    }
    public void EndGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    public void ChangeScene(string scenename) 
    {
        SceneManager.LoadScene(scenename);
    }



}
