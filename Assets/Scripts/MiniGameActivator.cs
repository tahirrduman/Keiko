using UnityEngine;

public class MinigameActivator : MonoBehaviour
{
    public int panelIndex; // Bu tetikleyici hangi paneli açacak?
    public GameObject etkilesimTusu; // UI ipucu için
    private bool isPlayerInRange = false; // Oyuncu tetikleyicide mi?
    private bool isTaskCompleted = false; // Görev tamamlandı mı?
    [SerializeField] private GameManager gameManager; // GameManager referansı

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !isTaskCompleted)
        {
            if (gameManager.GorevAktifMi(panelIndex))
            {
                OpenMinigame();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTaskCompleted)
        {
            if (gameManager.GorevAktifMi(panelIndex))
            {
                isPlayerInRange = true;
                etkilesimTusu.SetActive(true); // UI ipucunu göster
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            etkilesimTusu.SetActive(false); // UI ipucunu gizle
        }
    }

    void OpenMinigame()
    {
        gameManager.PanelAc(panelIndex); // İlgili paneli aç
        etkilesimTusu.SetActive(false); // UI ipucunu gizle
    }
}