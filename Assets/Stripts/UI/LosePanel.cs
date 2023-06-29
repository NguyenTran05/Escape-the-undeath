using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highscoreText;
    private float distance = 0f;
    private float highscore = 0f;

    private void Awake()
    {
        distance = GameManager.Instance.Distance;
        highscore = GameManager.Instance.Highscore;
        scoreText.text = distance.ToString("Score: " + "0");
        highscoreText.text = highscore.ToString("HighScore: " + "0");
    }

    public void OnClickedMenuButton()
    {
        SceneManager.LoadScene("Menu");
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayMusic(AUDIO.Music_HEROIC_INTRUSION);
        }
        if (GameManager.HasInstance)
        {
            GameManager.Instance.Menu();
        }
    }
    public void OnClickedExitButton()
    {
        if(GameManager.HasInstance)
        {
            GameManager.Instance.EndGame(); 
        }
    }
    
}
