using UnityEngine;
using UnityEngine.AI;

public class AIControl : Character
{
    // Animation Hashes
    private int isMovingAnimHash = -1;
    private int isCheeringAnimHash = -1;

    // References
    [SerializeField] private GameObject _hitParticle;
    private Vector3 _startPos;
    private NavMeshAgent _navAgent;
    private FinishLine _finishLine;

    // States
    private bool _isReached = false;

    [Header("NavMesh Properties")]
    [SerializeField][Range(0, 15f)] private float _moveSpeed = 5f;

    public Transform currentDestination;

    private void Start()
    {
        _startPos = transform.position;
        isMovingAnimHash = Animator.StringToHash("isMoving");
        _finishLine = GameObject.FindWithTag("FinishLine").GetComponent<FinishLine>();
        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.speed = _moveSpeed;

        currentDestination = _finishLine.GetStandPoint();
        _navAgent.SetDestination(currentDestination.position);
    }

    private void Update()
    {
        if (GameManager.instance.currentGameState == GameState.Playing && _isReached == false)
        {
            _rigidBody.isKinematic = true;
            _animator.SetBool(isMovingAnimHash, true);
            _navAgent.isStopped = false;
            return;
        }

        _navAgent.isStopped = true;
        _animator.SetBool(isMovingAnimHash, false);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            _isReached = true;
            _navAgent.isStopped = true;
            _animator.SetBool(isMovingAnimHash, false);
            _animator.SetBool(isCheeringAnimHash, true);
        }
    }

    protected override void OnCollisionEnter(Collision other)
    {
        //Taking AI to the start position.
        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (_hitParticle != null)
                Instantiate(_hitParticle, transform.position, Quaternion.identity).ParentSet(LevelManager.instance.debrisParent);

            transform.position = _startPos;
        }
    }
}
