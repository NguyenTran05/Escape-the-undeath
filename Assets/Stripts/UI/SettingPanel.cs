using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    private float musicValue;
    private float soundValue;

    private void OnEnable()
    {
        if (AudioManager.HasInstance)
        {
            musicValue = AudioManager.Instance.AttachMusicSource.volume;
            soundValue = AudioManager.Instance.AttachSoundSource.volume;
            musicSlider.value = musicValue;
            soundSlider.value = soundValue;
        }
    }
    public void OnSliderChangeMusicValue(float value)
    {
        musicValue = value;
        Debug.Log(value);
    }
    public void OnSliderChangeSoundValue(float value)
    {
        soundValue = value;
        Debug.Log(soundValue);

    }
    public void OnClickedCancelButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ActiveSettingPanel(false);
        }
        SettingPanelcallBack();
    }
    public void OnClickedSaveButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.ChangeMusicVolume(musicValue);
            
            AudioManager.Instance.ChangeSoundVolume(soundValue);
        }
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ActiveSettingPanel(true);
        }
    }
    private void SettingPanelcallBack()
    {
        if(GameManager.HasInstance && UIManager.HasInstance)
        {
            if(GameManager.Instance.Isplaying == false && UIManager.Instance.MenuPanel.gameObject.activeSelf == false)
            {
                UIManager.Instance.ActivePausePanel(true);
            }
        }
    }
}
