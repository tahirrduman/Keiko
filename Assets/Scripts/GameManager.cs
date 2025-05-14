using UnityEngine;
using TMPro; // TextMeshPro için gerekli
using System.Collections;
using UnityEngine.Video; // IEnumerator için gerekli
using UnityEngine.SceneManagement; // SceneManager için gerekli
public class GameManager : MonoBehaviour
{
    
    public GameObject[] paneller; // Tüm panelleri tutan bir dizi
    private int aktifPanelIndex = -1; // Şu anda açık olan panelin indexi
    [SerializeField] private KarakterHareketi karakterHareketi; // Karakter hareketi referansı
    private int aktifGorevIndex = 0; // Şu anda aktif olan görev (sıralı görev sistemi için)
    [SerializeField] private TextMeshProUGUI gorevSayaci; // Görev sayacı UI referansı
    private int toplamGorev; // Toplam görev sayısı
    [SerializeField] private TextMeshProUGUI bombaSayaci; // Bomba sayacı TMP referansı
   
    private int kalanHak = 3; // Oyuncunun kalan hakkı
    private bool isPaused = false; // Oyunun durdurulup durdurulmadığını takip eder
    private AudioSource backgroundMusic;
    
    
    // Aktif görev indexine sadece okunabilir erişim sağlamak için bir getter
    public int AktifGorevIndex
    {
        get
        {
            // Eğer tüm görevler tamamlandıysa, -1 döndür(mapte ünlem kalmasın diye)
            if (aktifGorevIndex >= toplamGorev)
            {
                return -1;
            }
            return aktifGorevIndex;
        }
    }

    void Start()
    {
        // Tüm değişkenleri başlangıç durumuna sıfırla
        aktifPanelIndex = -1;
        aktifGorevIndex = 0;
        kalanHak = 3;
    
        // Görev ve bomba sayaçlarını güncelle
        toplamGorev = paneller.Length;
        GorevSayaciniGuncelle();
        bombaSayaci.text = $"<color=#ffdf00>{kalanHak}</color>";
        // Sample Scene'deki BackgroundMusic objesini bul
        GameObject musicObject = GameObject.Find("BackgroundMusic");
        backgroundMusic = musicObject.GetComponent<AudioSource>();
    }
    public void Update()
    {
        // ESC tuşuna basıldığında pause menüsünü aç/kapat
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); // Oyunu devam ettir
            }
            else
            {
                OpenPauseMenu(); // Pause menüsünü aç
            }
        }
    }
    public void ResumeGame()
    {
        if (isPaused)
        {
            // Pause menüsünü kapat
            SceneManager.UnloadSceneAsync("PauseMenuScene");
            Time.timeScale = 1; // Oyunu devam ettir
            isPaused = false; // Oyunun devam ettiğini işaretle
             backgroundMusic.Play();
        }
    }

    public void OpenPauseMenu()
    {
        if (!isPaused)
        {
            // Pause menüsünü aç
            SceneManager.LoadScene("PauseMenuScene", LoadSceneMode.Additive);
            Time.timeScale = 0; // Oyunu durdur
            isPaused = true; // Oyunun durdurulduğunu işaretle
            backgroundMusic.Pause();

        }
    }
    // Belirli bir paneli aç
    public void PanelAc(int panelIndex)
    {
        if (panelIndex != aktifGorevIndex)
        {
            return;
        }

        paneller[panelIndex].SetActive(true);
        aktifPanelIndex = panelIndex;

        if (karakterHareketi != null)
        {
            karakterHareketi.enabled = false;
        }
    }

    // Şu anki paneli kapat
    public void PanelKapat()
    {
        if (aktifPanelIndex != -1)
        {
            paneller[aktifPanelIndex].SetActive(false);
            aktifPanelIndex = -1;

            if (karakterHareketi != null)
            {
                karakterHareketi.enabled = true;
            }
        }
    }

    // Doğru cevap seçildiğinde çağrılır
    public void DogruCevap()
    {
        PanelKapat();

        // Görevi tamamla ve bir sonraki görevi aktif yap
        aktifGorevIndex++;
        GorevSayaciniGuncelle(); // Görev sayacını güncelle

        // Eğer son görev tamamlandıysa Final Scene'e geç
        if (aktifGorevIndex >= toplamGorev)
        {
            CompleteFinalMission();
        }
    }

    // Yanlış cevap seçildiğinde çağrılır
    public void YanlisCevap()
    {
        PaneliTitret(); // Paneli titret
         // Kalan hakkı azalt
        kalanHak--;

        // Bomba sayacını güncelle
        bombaSayaci.text = $"<color=#ffdf00>{kalanHak}</color>";

        // Eğer haklar biterse patlama efektini oynat
        if (kalanHak <= 0)
        {
            SceneManager.LoadScene("FailedScene"); // LevelFailedScene sahnesine geç
        }
    }

    public void PaneliTitret()
    {
        if (aktifPanelIndex != -1)
        {
            StartCoroutine(Titre(paneller[aktifPanelIndex].GetComponent<RectTransform>(), 0.3f, 10f));
        }
    }

    IEnumerator Titre(RectTransform panel, float sure, float siddet)
    {
        Vector3 orijinalKonum = panel.anchoredPosition;
        float gecenSure = 0f;

        while (gecenSure < sure)
        {
            float rastgeleX = Random.Range(-1f, 1f) * siddet;
            float rastgeleY = Random.Range(-1f, 1f) * siddet;

            panel.anchoredPosition = new Vector3(orijinalKonum.x + rastgeleX, orijinalKonum.y + rastgeleY, orijinalKonum.z);

            gecenSure += Time.deltaTime;
            yield return null;
        }

        panel.anchoredPosition = orijinalKonum; // Paneli eski konumuna döndür
    }

    // Görev sayacını güncelle
    private void GorevSayaciniGuncelle()
    {
        gorevSayaci.text = $"<color=#ffdf00>Görevler: {aktifGorevIndex}/{toplamGorev}</color>";
    }

    // Aktif görevi kontrol et
    public bool GorevAktifMi(int panelIndex)
    {
        return panelIndex == aktifGorevIndex;
    }
    public void CompleteFinalMission()
    {
         // BackgroundMusic objesini bul ve müziği durdur
        GameObject musicObject = GameObject.Find("BackgroundMusic");
        AudioSource backgroundMusic = musicObject.GetComponent<AudioSource>();
        backgroundMusic.Stop(); // Müziği durdur
        
        // Final sahnesine geç
        SceneManager.LoadScene("FinalScene");
    }

}