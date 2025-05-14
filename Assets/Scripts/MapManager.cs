using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject mapUI; // Harita UI elemanı
    [SerializeField] private GameObject[] gorevIsaretleri; // Her görev için ünlem işareti
    [SerializeField] private GameManager gameManager; // GameManager referansı
    [SerializeField] private RectTransform karakterIkonu; // Karakterin haritadaki ikonu
    [SerializeField] private Transform karakterTransform; // Karakterin dünya üzerindeki transform'u
    [SerializeField] private RectTransform haritaTransform; // Harita UI'sinin RectTransform'u
    [SerializeField] private Vector2 haritaBoyutlari; // Haritanın boyutları (dünya koordinatlarına göre)

    private bool isMapOpen = false; // Harita açık mı?

    void Update()
    {
        // M tuşuna basıldığında haritayı aç/kapat
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap();
        }

        // Harita açıkken karakterin yerini güncelle
        if (isMapOpen)
        {
            KarakterKonumunuGuncelle();
        }
    }

    private void ToggleMap()
    {
        isMapOpen = !isMapOpen;
        mapUI.SetActive(isMapOpen); // Haritayı aç/kapat

        if (isMapOpen)
        {
            UpdateMap(); // Harita açıldığında güncelle
        }
        else
        {
            // Harita kapatıldığında tüm işaretleri gizle
            foreach (GameObject isaret in gorevIsaretleri)
            {
                isaret.SetActive(false);
            }
        }
    }

    private void UpdateMap()
    {
        // Tüm görev işaretlerini gizle
        foreach (GameObject isaret in gorevIsaretleri)
        {
            isaret.SetActive(false);
        }

        // Aktif görevin işaretini göster
        int aktifGorevIndex = gameManager.AktifGorevIndex; // Getter kullanılıyor
        if (aktifGorevIndex >= 0 && aktifGorevIndex < gorevIsaretleri.Length)
        {
            gorevIsaretleri[aktifGorevIndex].SetActive(true);
        }
    }

    private void KarakterKonumunuGuncelle()
    {
        // Karakterin dünya koordinatlarını harita koordinatlarına dönüştür
        Vector3 karakterPozisyon = karakterTransform.position;

        // Dünya koordinatlarını harita koordinatlarına ölçekle
        float xOrani = karakterPozisyon.x / haritaBoyutlari.x;
        float yOrani = karakterPozisyon.y / haritaBoyutlari.y;

        // Harita üzerindeki pozisyonu hesapla
        float haritaX = xOrani * haritaTransform.rect.width;
        float haritaY = yOrani * haritaTransform.rect.height;

        // Karakter ikonunun pozisyonunu güncelle
        karakterIkonu.anchoredPosition = new Vector2(haritaX, haritaY);
    }
}