using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static bool isSoundOn = true;
    private bool isControlsPanelOpen = false; // Kontrol panelinin açık olup olmadığını takip et
    [SerializeField] private Button soundButton; // Ses butonu referansı
    [SerializeField] private GameObject controlsPanel; // Tuş Kontrolleri Paneli referansı
     [SerializeField] private GameObject settingsPanel; // Settings Panel referansı
    public void StartGame()
    {
        AudioListener.pause = !SettingsManager.isSoundOn;
        soundButton.GetComponentInChildren<TextMeshProUGUI>().text = SettingsManager.isSoundOn ? "SES KAPAT" : "SES AÇ";
        // Oyunun ana sahnesine geç
        SceneManager.LoadScene("SampleScene"); // Ana sahnenizin adını buraya yazın
    }

    public void QuitGame()
    {
        // Oyunu kapat
        Application.Quit();
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
        isControlsPanelOpen = !isControlsPanelOpen;
        controlsPanel.SetActive(isControlsPanelOpen);
    }
}