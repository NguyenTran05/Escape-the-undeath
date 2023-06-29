using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Event : MonoBehaviour
{
    private UnityEvent movesound;
    private UnityEvent enemyatksound;
    private UnityEvent impactsound;
    private UnityEvent jumpsound;
    private UnityEvent hurtsound;

    void PlayerMove()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySound(AUDIO.Sound_WALKING);
        }
    }

    void Atk()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySound(AUDIO.Sound_ATTACK);

        }
    }
    void Impact()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySound(AUDIO.Sound_IMPACT);
        }
    }

    void Jump()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySound(AUDIO.Sound_JUMP);
        }
    }
    void Hurt()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySound(AUDIO.Sound_HURT);
        }
    }
}
