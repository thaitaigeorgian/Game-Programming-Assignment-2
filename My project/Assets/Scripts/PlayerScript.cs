using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float thrustForce = 5f;
    public float rotationSpeed = 200f;
    private Rigidbody2D rb;

    public AudioClip fireSound;
    public AudioClip engineSound;
    public AudioClip explosionSound;
    public AudioClip teleportSound;

    private AudioSource audioSource;

    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {

            float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward * -rotation);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(transform.up * thrustForce);
            audioSource.PlayOneShot(engineSound);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TeleportToRandomPosition();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        CheckWrapAround();
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        audioSource.PlayOneShot(fireSound);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.up * bulletSpeed; // Đạn di chuyển theo hướng "up" của firePoint

            Destroy(bullet, 5f);
        }
    }

    void CheckWrapAround()
    {
        Vector2 position = transform.position;

        // Nếu vị trí vượt qua giới hạn trục X, chuyển sang phía đối diện
        if (position.x > 10f)
        {
            position.x = -10f;
        }
        else if (position.x < -10f)
        {
            position.x = 10f;
        }

        // Nếu vị trí vượt qua giới hạn trục Y, chuyển sang phía đối diện
        if (position.y > 5f)
        {
            position.y = -5f;
        }
        else if (position.y < -5f)
        {
            position.y = 5f;
        }

        // Cập nhật vị trí của player
        transform.position = position;
    }

    void TeleportToRandomPosition()
    {
        float randomX = Random.Range(-10f, 10f);
        float randomY = Random.Range(-5f, 5f);

        transform.position = new Vector3(randomX, randomY, transform.position.z);
        audioSource.PlayOneShot(teleportSound);

        Debug.Log($"Teleported to: {randomX}, {randomY}");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            audioSource.PlayOneShot(explosionSound);
            print("Player and Asteroid");
            transform.position = new Vector2(0, 0);
        }
    }

}
