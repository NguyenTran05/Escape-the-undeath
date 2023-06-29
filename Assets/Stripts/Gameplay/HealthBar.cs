using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Image totalhealthbar;
    [SerializeField] private Image currenthealthbar;

    private void Start()
    {
        totalhealthbar.fillAmount = GameManager.Instance.currentHealth / 10;
    }
    private void Update()
    {
        currenthealthbar.fillAmount = GameManager.Instance.currentHealth / 10;
    }
}
