using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LevelFailedManager : MonoBehaviour
{
    [SerializeField] private GameObject levelFailedUI; // Level Failed UI paneli
    [SerializeField] private VideoPlayer videoPlayer; // Video Player bileşeni
    [SerializeField] private float uiGecikmeSuresi = 5f; // UI'nin görünmesi için bekleme süresi

    private void Start()
    {
        // Videoyu oynat
        videoPlayer.Play();
  
        // Belirli bir süre sonra UI'yi göster
        Invoke(nameof(LevelFailedUIyiGoster), uiGecikmeSuresi);
    }

    private void LevelFailedUIyiGoster()
    {
            levelFailedUI.SetActive(true);
    }
    // Retry butonuna bağlanacak fonksiyon
    public void Retry()
    {
        // Ana sahneyi yeniden yükle
        SceneManager.LoadScene("SampleScene"); // Ana sahnenizin adını buraya yazın
    }
    public void MainMenu()
    {
        // Ana menü sahnesine geç
        SceneManager.LoadScene("MainMenuScene"); // Ana menü sahnenizin adını buraya yazın
    }
}