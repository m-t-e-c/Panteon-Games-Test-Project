using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private Transform PlayerCam;
    [SerializeField] private Transform PaintCam;

    // References
    private Camera cam;
    private Transform target;

    [Header("Camera Properties")]
    [SerializeField] private Vector3 offset = new Vector3(0,10f, -10f);
    [SerializeField, Range(30f, 110f)] private float fov = 60f;
    [SerializeField, Range(0f, 1f)] private float lerpTime = 1f;

    private float defaultFov;

    private void Start()
    {
        cam = Camera.main.GetComponent<Camera>();

        defaultFov = cam.fieldOfView;
        target = PlayerCam;
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (!target) return;

        cam.fieldOfView = fov;
        
        Vector3 followPos = new Vector3(target.position.x, target.position.y, target.position.z) + offset;
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, followPos.x, lerpTime),followPos.y, followPos.z);
    }
}
