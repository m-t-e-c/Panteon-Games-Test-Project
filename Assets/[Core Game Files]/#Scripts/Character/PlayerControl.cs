using System.Collections;
using UnityEngine;

public class PlayerControl : Character, ISlipable, IPushable
{
    [Header("Character Movement Properties")]
    [SerializeField] private Vector2 _movementBoundaryHorizontal = Vector2.zero;
    [SerializeField] private Vector2 _movementBoundaryVertical = Vector2.zero;

    // Animation Hashes
    private int isMovingAnimHash = -1;
    private int isPaintingAnimHash = -1;

    [Space(10)]
    [SerializeField][Range(0f, 100f)] private float _forwardSpeed;
    [SerializeField][Range(0f, 200f)] private float _sidewaySpeed;

    // States
    private bool _isPainting = false;
    private bool _isMoving = false;
    private bool _isPushing = false;

    // Private Fields 
    private bool _isSliping;
    private float _slipDirection;

    [SerializeField] private GameObject _failStunParticle;

    #region Unity Methods

    private void Start()
    {
        CameraFollower.OnCameraSetted?.Invoke(CameraType.Player_Cam, transform);

        isMovingAnimHash = Animator.StringToHash("isMoving");
        isPaintingAnimHash = Animator.StringToHash("isPainting");
    }

    private void Update()
    {
        CheckIsMoving();
        AnimationControl();

        if (GameManager.instance.currentGameState == GameState.Playing)
        {
            if (_isPainting)
                _rigidBody.isKinematic = true;
            else
                Movement();
        }
        else
        {
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularVelocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PaintableWall"))
        {
            StartCoroutine(WaitForPaintingStage());
        }

        if (other.CompareTag("Water") || other.CompareTag("Obstacle"))
        {
            // Activating stun particle.
            _failStunParticle.SetActive(true);

            // When Character collide with obstacle we turning ragdoll on.
            ToggleRagdoll(true);

            UIManager.OnCommandExecuted?.Invoke("Lose Game");

            // Setting camera to Fail Camera.
            CameraFollower.OnCameraSetted?.Invoke(CameraType.Fail_Cam, transform);
        }
    }

    // Delaying the painting stage transition.
    IEnumerator WaitForPaintingStage()
    {
        _isPainting = true;
        yield return new WaitForSeconds(1f);
        // Setting camera to Paint Camera.
        CameraFollower.OnCameraSetted?.Invoke(CameraType.Paint_Cam, transform);
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }

    #endregion

    #region PlayerControl Methods

    // Default character movement system.
    protected virtual void Movement()
    {

        if (_isPushing) 
            return;

        float mouseXAxis = Input.GetAxis("Mouse X") * _sidewaySpeed + _slipDirection;

        float clampedX = Mathf.Clamp(transform.position.x, _movementBoundaryHorizontal.x, _movementBoundaryHorizontal.y);
        float clampedY = Mathf.Clamp(transform.position.y, _movementBoundaryVertical.x, _movementBoundaryVertical.y);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

        Vector3 newVelocity = new Vector3(mouseXAxis, _rigidBody.velocity.y, _forwardSpeed);
        _rigidBody.velocity = newVelocity;

        if (_isSliping)
            _rigidBody.velocity += new Vector3(_slipDirection * _sidewaySpeed * Time.deltaTime, 0, 0);
    }

    // Checking the IsMoving parameter by current game state and painting state.
    private void CheckIsMoving()
    {
        if (GameManager.instance.currentGameState != GameState.Playing || _isPainting == true)
            _isMoving = false;
        else
            _isMoving = true;
    }

    // Controls the animation state by hash values.
    private void AnimationControl()
    {
        if (_animator == null) 
            return;
        _animator.SetBool(isMovingAnimHash, _isMoving);
        _animator.SetBool(isPaintingAnimHash, _isPainting);
    }


    // Finding all RigidBody part we have, and we turning off/on them.
    // And we are deactivating our CapsuleCollider and RigidBody to not collide with ragdoll colliders.
    private void ToggleRagdoll(bool x)
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

    #region ISlipable

    public void Slip(bool status, int direction = 1)
    {
        _slipDirection = direction;
        _isSliping = status;
        _rigidBody.velocity = Vector3.zero;
    }
    #endregion

    #region IPushable
    public void Push(float pushForce)
    {
        StartCoroutine(StopPushingDelay(pushForce));
    }

    IEnumerator StopPushingDelay(float pushForce)
    {
        _isPushing = true;
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.AddForce(-Vector3.forward * pushForce);
        yield return new WaitForSeconds(1f);
        _rigidBody.velocity = Vector3.zero;
        _isPushing = false;
    }

    #endregion
}