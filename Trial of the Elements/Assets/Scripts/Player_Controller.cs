using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float moveSpeed;

    private Vector3 newPos;
    private Vector3 oldPos;

    private bool moving;
    private bool againstObject;

    private bool stopControl;

    private bool moveStop;

    private Game_Manager manager;
    private Animator anim;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_Manager>();
        anim = GetComponent<Animator>();
        newPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (!stopControl)
        {
            CheckInput();
        }
        else
        {
            IceSlide();
        }

        if (transform.position != newPos)
        {
            anim.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, newPos, moveSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isWalking", false);
            moving = false;
        }

        if (!moving && againstObject)
        {
            RoundToNearestHalf(oldPos.x);
            RoundToNearestHalf(oldPos.y);
            newPos = oldPos;
            moving = true;
        }
    }

    private void IceSlide()
    {
        if (!moving)
        {
            if (newPos.y > oldPos.y)
            {
                oldPos = transform.position;
                newPos = oldPos + new Vector3(0, 1, 0);

                moving = true;
            }
            else if (newPos.y < oldPos.y)
            {
                oldPos = transform.position;
                newPos = oldPos + new Vector3(0, -1, 0);

                moving = true;
            }

            if (newPos.x > oldPos.x)
            {
                oldPos = transform.position;
                newPos = oldPos + new Vector3(1, 0, 0);

                moving = true;
            }
            else if (newPos.x < oldPos.x)
            {
                oldPos = transform.position;
                newPos = oldPos + new Vector3(-1, 0, 0);

                moving = true;
            }
        }
    }

    private void CheckInput()
    {
        if (!moving && !moveStop)
        {
            if (Input.GetButton("Vertical"))
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    oldPos = transform.position;
                    newPos = oldPos + new Vector3(0, 1, 0);

                    moving = true;
                    moveStop = true;
                    StartCoroutine(WaitAndMove());
                }
                else
                if (Input.GetAxis("Vertical") < 0)
                {
                    oldPos = transform.position;
                    newPos = oldPos + new Vector3(0, -1, 0);

                    moving = true;
                    moveStop = true;
                    StartCoroutine(WaitAndMove());
                }
            }

            if (Input.GetButton("Horizontal"))
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    oldPos = transform.position;
                    newPos = oldPos + new Vector3(1, 0, 0);

                    moving = true;
                    moveStop = true;
                    StartCoroutine(WaitAndMove());
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    oldPos = transform.position;
                    newPos = oldPos + new Vector3(-1, 0, 0);

                    moving = true;
                    moveStop = true;
                    StartCoroutine(WaitAndMove());
                }
            }
        }
    }

    public float RoundToNearestHalf(float a)
    {
        return a = Mathf.Round(a * 2f) * 0.5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Door") || collision.collider.CompareTag("Grate"))
        {
            if (moving)
            {
                stopControl = false;
                newPos = oldPos;
            }
        }

        else if(collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Hole") || collision.collider.CompareTag("Lava"))
        {
            manager.PlayerLose();
        }

        else if (collision.collider.CompareTag("Sign"))
        {
            collision.gameObject.GetComponent<Pop_Up>().PopUpOn();
        }

        else if (collision.collider.CompareTag("Ice"))
        {
            stopControl = true;
        }

        else if (collision.collider.CompareTag("Endpoint"))
        {
            manager.PlayerWin();
        }

        else if (collision.collider.CompareTag("Element"))
        {
            againstObject = true;

            string direction = "";

            if (newPos.y > oldPos.y)
            {
                direction = "Up";
            }
            else if (newPos.y < oldPos.y)
            {
                direction = "Down";
            }

            if (newPos.x > oldPos.x)
            {
                direction = "Right";
            }
            else if (newPos.x < oldPos.x)
            {
                direction = "Left";
            }

            collision.collider.GetComponent<Element_Push>().PushObject(direction);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Element"))
        {
            againstObject = false;
        }

        else if (collision.collider.CompareTag("Sign"))
        {
            collision.gameObject.GetComponent<Pop_Up>().PopUpOff();
        }
    }

    private IEnumerator WaitAndMove()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        moveStop = false;
    }
}
