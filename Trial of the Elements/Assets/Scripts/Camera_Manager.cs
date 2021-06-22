using UnityEngine;

public class Camera_Manager : MonoBehaviour
{
    public float smoother;
    public Transform target;
    private Camera cam;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 point = cam.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, smoother);
        }

    }
}
