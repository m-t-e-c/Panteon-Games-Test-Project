using UnityEngine;
using UnityEngine.AI;

public class AIControl : Character
{
    // Animation Hashes
    private int isMovingAnimHash = -1;
    private int isCheeringAnimHash = -1;

    // References
    private Vector3 _startPos;
    private NavMeshAgent _navAgent;
    private Transform _finishLine;

    [Header("NavMesh Properties")]
    [SerializeField][Range(0, 15f)] private float _moveSpeed = 5f;

    private void Start()
    {
        _startPos = transform.position;

        isMovingAnimHash = Animator.StringToHash("isMoving");
        isCheeringAnimHash = Animator.StringToHash("isCheering");

        _finishLine = GameObject.FindWithTag("FinishLine").transform;

        _navAgent = GetComponent<NavMeshAgent>();

        _navAgent.SetDestination(_finishLine.position);
        _navAgent.speed = _moveSpeed;
    }

    private void Update()
    {
        if (GameManager.instance.currentGameState == GameState.Playing)
        {
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
            print("YO");
            _navAgent.isStopped = true;
            _animator.SetBool(isMovingAnimHash, false);
            _animator.SetBool(isCheeringAnimHash, true);
        }
    }

    protected override void OnCollisionEnter(Collision other)
    {
        //base.OnCollisionEnter(other);

        if (other.gameObject.CompareTag("Obstacle"))
            transform.position = _startPos;
    }
}
