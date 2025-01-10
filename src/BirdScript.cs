using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    // Refrensi Rigidbody2D untuk mengontrol sifat burung terhadap obstacle
    public Rigidbody2D myRigidbody;
    // Inisiasi kontrol burung
    public float flapStrength;
    // Refrensi untuk LogicScript
    public LogicScript logic;
    // Boolean mengindikasi apakah burung hidup atau mayi
    public bool birdIsAlive = true;
    // Inisiasi beberapa boolean dan float pada fitur dashing 
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    // Komponen visual efek pada dashing
    [SerializeField] private TrailRenderer tr;

    // Refrensi Camera serta offset posisi Vector3 terhadap kamera
    private Camera mainCamera;
    private Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {   
        // Mengambil refrensi script LogicScript 
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        // Mengambil referensi Camera.main serta menghitung offset kamera
        mainCamera = Camera.main; 
        cameraOffset = mainCamera.transform.position - transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        // Mengabaikan fungsi lain ketika burung sedang dash
        if (isDashing)
        {
            return;
        }
        // Input tombol space agar burung bisa loncat ke atas
        if (Input.GetKeyDown(KeyCode.Space) == true && birdIsAlive == true)
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
        }
        // Input tombol shift kiri agar burung bisa dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && birdIsAlive == true)
        {
            // Corountine untuk fungsi dash
            StartCoroutine(Dash());
        }
        // Fungsi untuk emastikan kamera tetap mengikuti burung
        LockCameraToBird();
    }
    // Memanggil fungsi void saat burung bertabrakan dengan objek lain
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Hasil: burung akan mati dan game akan berhenti
        logic.gameOver();
        birdIsAlive = false;
    }
    // Coroutine untuk menangani logika dash
    private IEnumerator Dash()
    {
        // Insiasi awal burung agar tidak dash pada awalnya
        canDash = false;
        isDashing = true;
        // Menyimpan gravitasi burung pada saat sebelum dash. Lalu pada saat dash, set gravitasi ke 0
        float originalGravity = myRigidbody.gravityScale;
        myRigidbody.gravityScale = 0f;
        // Mengatur kekuatan,arah, serta kecepatan horizontal dash (pada konteks ini burung melakukan dash ke kanan)
        myRigidbody.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        // Mengaktifkan efek visual trail
        tr.emitting = true;
        // Memberikan efek durasi dash
        yield return new WaitForSeconds(dashingTime);
        // Setelah selesai durasi dash, maka akan terjadi:
        // Efek trail akan habis
        tr.emitting = false;
        // Gravitasi akan diset menjadi sediakala
        myRigidbody.gravityScale = originalGravity;
        // Nonaktifkan status dash
        isDashing = false;
        // Memberi efek cooldown dash sebelum nantinya dash akan dapat digunakan lagi
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    // Fungsi LockCameraToBird untuk memastikan kamera mengikuti burung
    private void LockCameraToBird()
    {
        // Memperbarui posisi kamera berdasarkan posisi burung dan offset
        if (mainCamera != null)
        {
            mainCamera.transform.position = transform.position + cameraOffset;
        }
    }
}
