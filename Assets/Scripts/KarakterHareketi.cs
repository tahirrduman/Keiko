using UnityEngine;
using System;

public class KarakterHareketi : MonoBehaviour
{
    public float hiz = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private AudioSource audioSource; // Ses kaynağı
    [SerializeField] private AudioClip adimSesi; // Adım sesi

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // AudioSource bileşenini al
    }

    void FixedUpdate()
    {
        float yatay = Input.GetAxisRaw("Horizontal");
        float dikey = Input.GetAxisRaw("Vertical");

        Vector2 hareket = new Vector2(yatay, dikey).normalized;
        rb.linearVelocity = hareket*hiz; 

            if(yatay > 0)
            {
                sr.flipX = false; 
            }
            else if (yatay < 0)
            {
                sr.flipX = true; 
            }

         anim.SetFloat("Speed", Mathf.Abs(yatay)+Mathf.Abs(dikey)); 

         // Hareket varsa adım sesini çal
        if (hareket.magnitude > 0 && !audioSource.isPlaying)
        {
            audioSource.clip = adimSesi;
            audioSource.Play();
        }
        else if (hareket.magnitude == 0)
        {
            audioSource.Stop(); // Karakter durduğunda sesi durdur
        }
    }
}
