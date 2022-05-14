using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public abstract class Character : MonoBehaviour
{

    [Header("References")]
    [SerializeField] protected Animator _animator;
    protected Rigidbody _rigidBody = null;
    protected CapsuleCollider _capsuleCollider = null;

    #region Unity Methods

    protected virtual void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    #endregion
}
