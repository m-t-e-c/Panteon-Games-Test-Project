using UnityEngine;

public class PlayerControl : Character
{
    // Animation Hashes
    private int isMovingAnimHash = -1;
    private int isCheeringAnimHash = -1;

    // Movement Properties
    private float horizontal = 0;

    [SerializeField] private bool _isCheering = false;
    [SerializeField] private bool _isPainting = false;


    private void Start()
    {
        isMovingAnimHash = Animator.StringToHash("isMoving");
        isCheeringAnimHash = Animator.StringToHash("isCheering");
    }

    private void Update()
    {
        CheckIsMoving();
        AnimationControl();
        
        horizontal = Input.GetAxis("Mouse X");

        if (GameManager.instance.currentGameState == GameState.Playing)
        {
            if (_isPainting)
                Paint();
            else
                Move(horizontal);
        }
    }

    private void CheckIsMoving()
    {
        if (GameManager.instance.currentGameState != GameState.Playing || _isPainting == true)
            _isMoving = false;
        else 
            _isMoving = true;
    }

    private void Paint()
    {
        // Painting.
    }

    public void ToggleRagdoll(bool x)
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

    private void AnimationControl()
    {
        if (_animator == null) return;
        _animator.SetBool(isMovingAnimHash, _isMoving);
        _animator.SetBool(isCheeringAnimHash, _isCheering);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            ToggleRagdoll(true);
            UIManager.instance.ExecuteCommand("Lose Game");
        }
    }
}
