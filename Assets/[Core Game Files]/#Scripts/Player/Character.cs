using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public abstract class Character : MonoBehaviour
{
    [Header("Character Movement Properties")]
    [SerializeField] protected Vector2 _movementBoundary = Vector2.zero;
    [SerializeField][Range(0f, 100f)] protected float _forwardSpeed = 5f;
    [SerializeField][Range(0f, 100f)] protected float _sidewaySpeed = 20f;

    [Header("Component References")]
    [SerializeField] protected Animator _animator;
    protected Rigidbody _rigidBody = null;
    protected CapsuleCollider _capsuleCollider = null;

    [Header("States")]
    [SerializeField] protected bool _isMoving = false;

    public float xPos = 0;

    protected virtual void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Default character movement system.
    protected virtual void Move(float horizontal, float vertical = 1f)
    {
        float forwardSpeed = vertical * _forwardSpeed * Time.deltaTime;
        float sidewaySpeed = horizontal * _sidewaySpeed * Time.deltaTime;

        xPos += sidewaySpeed;
        xPos = Mathf.Clamp(xPos, _movementBoundary.x, _movementBoundary.y);

        Vector3 newPosition = new Vector3(xPos,transform.position.y, transform.position.z + forwardSpeed);
        transform.position = newPosition;
    }
}
