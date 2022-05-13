using UnityEngine;

public class MovingObstacle : MonoBehaviour
{

    [Header("Properties")]
    [SerializeField] private Vector3 _moveDistance;
    [SerializeField] private float _moveSpeed;

    [Header("State")]

    [SerializeField][Tooltip("If you want to rotate object activate this.")] public bool _useRotation = false;
    private bool _reverse = false;

    private Vector3 _startPos = Vector3.zero;
    private Vector3 _endPos = Vector3.zero;

    private float _timeStartedLerping;

    private void Start()
    {
        ChangeDestination();
    }

    private void ChangeDestination()
    {
        _startPos = transform.position;
        _endPos = _reverse ? transform.position - _moveDistance : transform.position + _moveDistance;
        _timeStartedLerping = Time.time;
        _startPos = transform.position;
    }

    private void FixedUpdate()
    {
        float timeSinceLerp = Time.time - _timeStartedLerping;
        float completeAmount = timeSinceLerp / _moveSpeed;

        transform.position = Vector3.Lerp(_startPos, _endPos, completeAmount);

        if (completeAmount >= 1f)
        {
            _reverse = !_reverse;
            ChangeDestination();
        }
    }

}