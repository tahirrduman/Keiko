using UnityEngine;

public class KameraTakibi : MonoBehaviour
{
    public Transform oyuncu;
    public Vector3 kameraMesafe = new Vector3(0, 0, -10);
    public float takipHizi = 5f;

    void FixedUpdate()
    {
        if (oyuncu != null)
        {
            Vector3 hedefPozisyon = oyuncu.position + kameraMesafe;
            transform.position = Vector3.Lerp(transform.position, hedefPozisyon, takipHizi * Time.deltaTime);
        }
    }
}
