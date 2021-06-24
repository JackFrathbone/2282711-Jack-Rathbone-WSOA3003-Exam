using System.Collections;
using UnityEngine;

public class Enemy_Ranged : MonoBehaviour
{
    public GameObject projectile;

    private bool canFire = true;

    private void Update()
    {
        if (canFire)
        {
            Shoot();
            canFire = false;
            StartCoroutine("WaitToShoot");
        }
    }

    private void Shoot()
    {
        Instantiate(projectile, transform.position, projectile.transform.rotation);
    }

    private IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(1f);
        canFire = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Element") || collision.collider.CompareTag("Lava"))
        {
            if (collision.collider.CompareTag("Element"))
            {
                collision.collider.GetComponent<Element_Push>().SpawnParticleEffect();
            }

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
