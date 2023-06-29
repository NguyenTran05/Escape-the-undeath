using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GamePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highscoreText;
    [SerializeField] private Transform player; 
    private float distance = 0f;
    private float highscore = 0f;
    private float curDist;
    private bool isplaying = true;
         
    private void Awake()
    {
        distance = GameManager.Instance.Distance;
        highscore = GameManager.Instance.Highscore;
        isplaying = GameManager.Instance.Isplaying;
        scoreText.text = distance.ToString("Score: " + "0.0") + "m";
        highscoreText.text = highscore.ToString("HighScore: " + "0.0") + "m";
    }

    private void Update()
    {
        curDist = this.player.position.x;
        if (isplaying)
        {
            if (curDist > distance)
            {
                distance = curDist;
                scoreText.text = distance.ToString("Score: " + "0.0") + "m";
                GameManager.Instance.UpdateDistance(distance);

                if (distance > highscore)
                {
                    highscore = distance;
                    highscoreText.text = highscore.ToString("HighScore: " + "0.0") + "m";
                    GameManager.Instance.UpdateHighscore(highscore);
                }
            }
        }
        
    }
    private void OnLevelWasLoaded(int level)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        distance = 0f;
    }


}
