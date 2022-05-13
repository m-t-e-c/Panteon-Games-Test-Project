using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public abstract class Character : MonoBehaviour
{
    [Header("Character Movement Properties")]
    [SerializeField] protected Vector2 _movementBoundary = Vector2.zero;

    [Header("Component References")]
    [SerializeField] protected Animator _animator;
    protected Rigidbody _rigidBody = null;
    protected CapsuleCollider _capsuleCollider = null;

    [Header("States")]
    [SerializeField] protected bool _isMoving = false; 
    [SerializeField] protected bool _isPainting= false;

    [SerializeField] protected Transform _footTransform;
    [SerializeField] protected GameObject _stepDust;

    public float xPos = 0;

    #region Unity Methods

    protected virtual void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }


    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            _isPainting = true;
        }
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // When Character collide with obstacle we turning ragdoll on.
            ToggleRagdoll(true);
        }
    }

    #endregion

    #region Character Virtual Methods

    // Finding all RigidBody part we have, and we turning off/on them.
    // And we are deactivating our CapsuleCollider and RigidBody to not collide with ragdoll colliders.
    protected virtual void ToggleRagdoll(bool x)
    {
        Rigidbody[] rigids = GetComponentsInChildren<Rigidbody>();
        Animator animator = GetComponentInChildren<Animator>();
        foreach (Rigidbody rb in rigids)
        {
            rb.isKinematic = !x;
            rb.GetComponent<Collider>().isTrigger = !x;
        }

        _capsuleCollider.isTrigger = x;
        _rigidBody.isKinematic = x;
        animator.enabled = !x;
    }
    #endregion
}
