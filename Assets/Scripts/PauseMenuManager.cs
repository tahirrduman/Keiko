using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Toggle için gerekli
using TMPro; // TextMeshPro için gerekli

public class PauseMenuManager : MonoBehaviour
{
    public static bool isSoundOn = true;
    [SerializeField] private Button soundButton; // Ses butonu referansı
    [SerializeField] private GameObject controlsPanel; // Tuş Kontrolleri Paneli referansı
    [SerializeField] private GameObject settingsPanel; // Settings Panel referansı

    void Start()
    {
        AudioListener.pause = !SettingsManager.isSoundOn;
        soundButton.GetComponentInChildren<TextMeshProUGUI>().text = SettingsManager.isSoundOn ? "SES KAPAT" : "SES AÇ";
    }
    
    public void RestartGame()
    {
        // Oyunu yeniden başlat
        Time.timeScale = 1;
        GameObject musicObject = GameObject.Find("BackgroundMusic");
        if (musicObject != null)
        {
            AudioSource backgroundMusic = musicObject.GetComponent<AudioSource>();
            if (backgroundMusic != null)
                backgroundMusic.Stop();
        }
        SceneManager.LoadScene("SampleScene");
    }

    public void ReturnToMainMenu()
    {
        // BackgroundMusic objesini bul ve müziği durdur
        GameObject musicObject = GameObject.Find("BackgroundMusic");
        if (musicObject != null)
        {
            AudioSource backgroundMusic = musicObject.GetComponent<AudioSource>();
            if (backgroundMusic != null)
                backgroundMusic.Stop(); // Müziği durdur
        }

        // Ana menü sahnesine geç
        Time.timeScale = 1; // Zamanı sıfırla
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OpenSettings()
    {
        // Ayarlar panelini aç
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        // Ayarlar panelini kapat
        settingsPanel.SetActive(false);
    }
    public void ToggleSound()
    {
        SettingsManager.isSoundOn = !SettingsManager.isSoundOn;
        AudioListener.pause = !SettingsManager.isSoundOn;
        soundButton.GetComponentInChildren<TextMeshProUGUI>().text = SettingsManager.isSoundOn ? "SES KAPAT" : "SES AÇ";
    }

    public void ToggleControlsPanel()
    {
        // Tuş Kontrolleri Paneli açık/kapalı durumunu değiştir
        controlsPanel.SetActive(!controlsPanel.activeSelf);
    }

    public void QuitGame()
    {
        // Oyunu kapat
        Application.Quit();
    }
}