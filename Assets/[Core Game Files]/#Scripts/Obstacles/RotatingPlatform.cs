using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [Header("Rotation Properties")]
    [SerializeField] private float _rotationSpeed = 30f;

    [Tooltip("If you activate this platform will rotate other side.")]
    [SerializeField] private bool _reverse = false;

    private void Update()
    {
        if (_reverse)
            transform.GetChild(0).Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        else
            transform.GetChild(0).Rotate(0, 0, -_rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ISlipable slipable))
        {
            int direction = _reverse ? -1 : 1;
            slipable.Slip(true, direction);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ISlipable slipable))
        {
            slipable.Slip(false);
        }
    }
}
