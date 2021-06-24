using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Patrol : MonoBehaviour
{
    public List<Vector3> positions;

    private Vector3 nextPos;
    private int onIndex;

    private bool reverse;

    public bool hasWeakness;
    public ObjectType weaknessType;

    private void Start()
    {
        nextPos = transform.position;
    }

    private void Update()
    {
        if (transform.position != nextPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextPos, 4f * Time.deltaTime);
        }
        else
        {
            nextPos = GetNextPos();
        }
    }

    private Vector3 GetNextPos()
    {
        Vector3 newpos = Vector3.zero;

        if (onIndex < positions.Count - 1 && !reverse && onIndex != 0)
        {
            newpos = positions[onIndex];
            onIndex++;
        }

        else if (onIndex < positions.Count - 1 && reverse && onIndex != 0)
        {
            newpos = positions[onIndex];
            onIndex--;
        }

        else if (onIndex == positions.Count - 1)
        {
            reverse = true;
            newpos = positions[onIndex];
            onIndex--;
        }

        else if (onIndex == 0)
        {
            reverse = false;
            newpos = positions[onIndex];
            onIndex++;
        }

        return newpos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Element") || collision.collider.CompareTag("Lava"))
        {
            if (!hasWeakness)
            {
                if (collision.collider.CompareTag("Element"))
                {
                    collision.collider.GetComponent<Element_Push>().SpawnParticleEffect();
                }

                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else
            {
                if (collision.collider.CompareTag("Element"))
                {
                    if (weaknessType == collision.collider.GetComponent<Element_Push>().elementType)
                    {

                        collision.collider.GetComponent<Element_Push>().SpawnParticleEffect();


                        Destroy(collision.gameObject);
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                    }
                }
            }
        }
    }
}
