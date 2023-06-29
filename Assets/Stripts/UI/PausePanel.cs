using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public void OnClickedResumeButton()
    {
        if (GameManager.HasInstance)
        {
            GameManager.Instance.Resume();
            UIManager.Instance.ActivePausePanel(false);
        }
    }
    public void OnClickedRestartButton()
    {
        if (GameManager.HasInstance)
        {
            GameManager.Instance.RestartGame();
        }
    }
    public void OnClickedSettingButton()
    {
        if (GameManager.HasInstance)
        {
            GameManager.Instance.Setting();
        }
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
