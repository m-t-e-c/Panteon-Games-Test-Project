using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private Vector3 _moveDistance;
    [SerializeField] private float _duration;

    [Header("States")]
    [SerializeField][Tooltip("If you want to rotate object activate this.")] public bool _useRotation = false;
    private bool _reverse = false;

    // References
    private Vector3 _startPos = Vector3.zero;
    private Vector3 _endPos = Vector3.zero;

    // Properties
    private float _timeStartedLerping;

    #region Unity Methods
    private void Start()
    {
        ChangeDestination();
    }

    private void FixedUpdate()
    {
        float timeSinceLerp = Time.time - _timeStartedLerping;
        float completeAmount = timeSinceLerp / _duration;

        transform.position = Vector3.Lerp(_startPos, _endPos, completeAmount);

        if (completeAmount >= 1f)
        {
            _reverse = !_reverse;
            ChangeDestination();
        }
    }
    #endregion

    #region MovingObstacle Methods
    private void ChangeDestination()
    {
        _startPos = transform.position;
        _endPos = _reverse ? transform.position - _moveDistance : transform.position + _moveDistance;
        _timeStartedLerping = Time.time;
        _startPos = transform.position;
    }
    #endregion
}