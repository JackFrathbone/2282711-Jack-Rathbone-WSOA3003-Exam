using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void Update()
    {
    transform.position += -transform.up * 4f * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Door"))
        {
            Destroy(gameObject);
        }
    }
}
