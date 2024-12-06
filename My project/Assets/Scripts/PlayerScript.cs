using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float thrustForce = 5f;
    public float rotationSpeed = 200f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward * -rotation);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(transform.up * thrustForce);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TeleportToRandomPosition();
        }
    }

    void TeleportToRandomPosition()
    {
        float randomX = Random.Range(-10f, 10f);
        float randomY = Random.Range(-5f, 5f);

        transform.position = new Vector3(randomX, randomY, transform.position.z);

        Debug.Log($"Teleported to: {randomX}, {randomY}");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {

        }
    }

}
