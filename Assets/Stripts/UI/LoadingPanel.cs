using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private Slider loadingSlider;
    

    private void OnEnable()
    {
        StartCoroutine(LoadScene());
    }
    private IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Level1");
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            loadingSlider.value = asyncOperation.progress;
            loadingText.SetText($"LOADING SCENES: {asyncOperation.progress * 100}% ");
            if (asyncOperation.progress >= 0.9f)
            {
                loadingText.SetText("Press the space bar to continue");
                loadingSlider.value = 1f;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    asyncOperation.allowSceneActivation = true;
                    if (UIManager.HasInstance && GameManager.HasInstance)
                    {
                        UIManager.Instance.ActiveGamePanel(true);
                        UIManager.Instance.ActiveLoadingPanel(false);
                        GameManager.Instance.StartGame();

                    }
                }
            }
            yield return null;
        }
    }
}
