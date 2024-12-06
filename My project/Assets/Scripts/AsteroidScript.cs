using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public float health = 100f;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float healthPercent = health / 100f;
        spriteRenderer.color = Color.Lerp(Color.red, Color.white, healthPercent);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Asteroid hit by bullet!");
            health -= 50f;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
