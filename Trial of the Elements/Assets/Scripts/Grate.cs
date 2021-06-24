using UnityEngine;

public class Grate : MonoBehaviour
{
    public Vector3 upPos;
    public Vector3 downPos;

    private void Start()
    {
        Vector3 currentPos = transform.position;


        upPos = currentPos + new Vector3(0, 1, 0);
        downPos = currentPos + new Vector3(0, -1, 0);
    }

    public Vector3 GetGratePosition(Vector3 input)
    {
        if(input.y > transform.position.y)
        {
            return downPos;
        }
        else
        {
            return upPos;
        }
    }
}