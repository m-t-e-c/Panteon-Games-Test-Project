using UnityEngine;
using UnityEngine.AI;

public class AIControl : Character
{
    // Animation Hashes
    private int isMovingAnimHash = -1;

    // References
    [SerializeField] private GameObject _hitParticle;
    private Vector3 _startPos;
    private NavMeshAgent _navAgent;

    [Header("NavMesh Properties")]
    [SerializeField][Range(0, 15f)] private float _moveSpeed = 5f;

    public Transform currentDestination;

    private void Start()
    {
        _startPos = transform.position;

        isMovingAnimHash = Animator.StringToHash("isMoving");

        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.speed = _moveSpeed;
        _navAgent.SetDestination(currentDestination.position);
    }

    private void Update()
    {
        if (currentDestination == null) return;

        if (GameManager.instance.currentGameState == GameState.Playing && _navAgent.remainingDistance > 0.1f)
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
        //Taking AI to the start position.
        if (other.CompareTag("Obstacle"))
        {
            print("OBSTACLE HIT");
            if (_hitParticle != null)
                Instantiate(_hitParticle, transform.position, Quaternion.identity).ParentSet(LevelManager.instance.debrisParent);

            transform.position = _startPos;
        }
    }
}
