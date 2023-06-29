using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPanel : MonoBehaviour
{
    public void OnClickedStartGameButton()
    {
        SceneManager.LoadScene("Level1");
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayMusic(AUDIO.Music_CHASE_AT_RUSH_HOUR);
        }
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ActiveMenuPanel(false);
            UIManager.Instance.ActiveLoadingPanel(true);

        }
    }
    public void OnSettingButtonClick()
    {
        if(UIManager.HasInstance)
        {
            UIManager.Instance.ActiveSettingPanel(true);
        }
    }
    public void OnClickedExitButton()
    {
        if (GameManager.HasInstance)
        {
            GameManager.Instance.EndGame();
        }

    }
}
