using System.Collections;
using UnityEngine;

public class PlayerControl : Character
{
    // Animation Hashes
    private int isMovingAnimHash = -1;
    private int isPaintingAnimHash = -1;

    [Header("Movement Properties")]
    [SerializeField][Range(0f, 100f)] protected float _forwardSpeed = 5f;
    [SerializeField][Range(0f, 100f)] protected float _sidewaySpeed = 20f;

    // Private Fields 
    private float xPos = 0;
    private float horizontal = 0;

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

        horizontal = Input.GetAxis("Mouse X");

        if (GameManager.instance.currentGameState == GameState.Playing)
        {
            if (_isPainting)
            {
                _rigidBody.isKinematic = true;
                return;
            }

            Move(horizontal);
        }
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);

        if (other.gameObject.CompareTag("Obstacle"))
        {
            // Activating stun particle.
            _failStunParticle.SetActive(true);

            UIManager.OnCommandExecuted?.Invoke("Lose Game");

            // Setting camera to Fail Camera.
            CameraFollower.OnCameraSetted?.Invoke(CameraType.Fail_Cam, transform);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("FinishLine"))
        {
           StartCoroutine(WaitForPaintingStage());
        }
    }

    IEnumerator WaitForPaintingStage()
    {
        yield return new WaitForSeconds(2f);
        // Setting camera to Paint Camera.
        CameraFollower.OnCameraSetted?.Invoke(CameraType.Paint_Cam, transform);
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }

    #endregion

    #region PlayerControl Methods

    // Default character movement system.
    protected virtual void Move(float horizontal, float vertical = 1f)
    {
        float forwardSpeed = vertical * _forwardSpeed * Time.deltaTime;
        float sidewaySpeed = horizontal * _sidewaySpeed * Time.deltaTime;

        xPos += sidewaySpeed;
        xPos = Mathf.Clamp(xPos, _movementBoundary.x, _movementBoundary.y);

        Vector3 newPosition = new Vector3(xPos, transform.position.y, transform.position.z + forwardSpeed);
        transform.position = newPosition;
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
        if (_animator == null) return;
        _animator.SetBool(isMovingAnimHash, _isMoving);
        _animator.SetBool(isPaintingAnimHash, _isPainting);
    }
    #endregion
}