using UnityEngine;

public class Grate : MonoBehaviour
{
    public bool grateUp;
    public Vector3 newPos;

    private void Start()
    {
        Vector3 currentPos = transform.position;

        if (grateUp)
        {
            newPos = currentPos + new Vector3(0, 1, 0);
        }
        else
        {
            newPos = currentPos + new Vector3(0, -1, 0);
        }
    }
}