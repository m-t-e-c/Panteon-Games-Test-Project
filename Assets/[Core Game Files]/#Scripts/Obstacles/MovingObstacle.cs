using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Header("Object To Move")]
    [SerializeField] private Transform _transform;

    [Header("Properties")]
    [SerializeField] private Vector3 _moveDistance;
    [SerializeField] private float _reverseDelay;
    [SerializeField] private float _duration;

    // States
    private bool _reverse = false;

    //Private References
    private Vector3 _startPos = Vector3.zero;
    private Vector3 _endPos = Vector3.zero;

    private float _timeStartedLerping;
    private float waitTime;

    #region Unity Methods
    private void Start()
    {
        if (_transform == null)
            _transform = transform;
        ChangeDestination();
    }

    private void FixedUpdate()
    {
        float timeSinceLerp = Time.time - _timeStartedLerping;
        float completeAmount = timeSinceLerp / _duration;

        _transform.position = Vector3.Lerp(_startPos, _endPos, completeAmount);

        if (completeAmount >= 1f)
        {
            waitTime += Time.deltaTime;
            if (waitTime >= _reverseDelay)
            {
                waitTime = 0;
                _reverse = !_reverse;
                ChangeDestination();
            }
        }
    }
    #endregion

    #region MovingObstacle Methods
    private void ChangeDestination()
    {
        _startPos = _transform.position;
        _endPos = _reverse ? _transform.position - _moveDistance : _transform.position + _moveDistance;
        _timeStartedLerping = Time.time;
        _startPos = _transform.position;
    }
    #endregion
}