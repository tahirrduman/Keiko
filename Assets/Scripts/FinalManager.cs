using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class FinalSceneManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer; // Video Player referansı
    [SerializeField] private GameObject continueText; // "Devam Et" yazısı referansı

    private bool canContinue = false; // Devam edilebilir mi?

    void Start()
    {
        // "Devam Et" yazısını başlangıçta gizle
        continueText.SetActive(false);

        // Video bittiğinde bir olay tetikle
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        // Eğer devam edilebilir durumdaysa ve herhangi bir tuşa basılırsa
        if (canContinue && Input.anyKeyDown)
        {
            ReturnToMainMenu();
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // Video bittiğinde "Devam Et" yazısını göster
        continueText.SetActive(true);
        canContinue = true;
    }

    public void ReturnToMainMenu()
    {
        // Ana menü sahnesine dön
        SceneManager.LoadScene("MainMenuScene"); // Ana menü sahnesinin adını buraya yazın
    }
}