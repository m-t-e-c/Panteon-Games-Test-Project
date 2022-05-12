using UnityEngine;

public class PlayerControl : Character
{
    // Animation Hashes
    private int isMovingAnimHash = -1;
    private int isCheeringAnimHash = -1;

    // Movement Properties
    private float horizontal = 0;

    [Header("States")]
    [SerializeField] private bool _isPainting = false;
    [SerializeField] private bool _isCheering = false;

    private void Start()
    {
        isMovingAnimHash = Animator.StringToHash("isMoving");
        isCheeringAnimHash = Animator.StringToHash("isCheering");
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Mouse X");
        if(GameManager.instance.currentGameState == GameState.Playing)
        {
            if (_isPainting)
            {
                _isMoving = false;
                Paint();
            }
            else
            {
                _isMoving = true;   
                Move(horizontal);
            }
        }

        if(GameManager.instance.currentGameState == GameState.Finished)
        {
            _isCheering = true;
        }

        AnimationControl();
    }

    private void Paint()
    {

    }

    private void AnimationControl()
    {
        if (_animator == null) return;
        _animator.SetBool(isMovingAnimHash, _isMoving);
        _animator.SetBool(isCheeringAnimHash, _isCheering);
    }
}
