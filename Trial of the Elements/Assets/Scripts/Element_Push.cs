using UnityEngine;

public class Element_Push : MonoBehaviour
{
    public ObjectType elementType;
    public float moveSpeed;

    private Vector3 newPos;
    private Vector3 oldPos;

    private bool moving;

    private Combination_Manager com_manager;

    private void Start()
    {
        com_manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<Combination_Manager>();
        newPos = transform.position;
    }

    private void Update()
    {
        if (transform.position != newPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, newPos, moveSpeed * Time.deltaTime);
        }
        else
        {
            moving = false;
        }
    }

    public void PushObject(string direction)
    {
        if (!moving)
        {
            if (direction == "Up")
            {
                oldPos = transform.position;
                newPos = oldPos + new Vector3(0, 1, 0);

                moving = true;
            }
            else if (direction == "Down")
            {
                oldPos = transform.position;
                newPos = oldPos + new Vector3(0, -1, 0);

                moving = true;
            }
            else if (direction == "Right")
            {
                oldPos = transform.position;
                newPos = oldPos + new Vector3(1, 0, 0);

                moving = true;
            }
            else if (direction == "Left")
            {
                oldPos = transform.position;
                newPos = oldPos + new Vector3(-1, 0, 0);

                moving = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            if (moving)
            {
                newPos = oldPos;
            }
        }
        else if (collision.collider.CompareTag("Player"))
        {
            return;
        }
        else if (collision.collider.CompareTag("Grate"))
        {
            newPos = collision.gameObject.GetComponent<Grate>().newPos;
        }
        else
        {
            if (!com_manager.CheckCombination(elementType, gameObject, collision.gameObject))
            {
                if (moving)
                {
                    newPos = oldPos;
                }
            }
        }
    }
}
