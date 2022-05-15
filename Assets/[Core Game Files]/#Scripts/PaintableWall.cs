using UnityEngine;
using DG.Tweening;

public class PaintableWall : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation _frontWallTween;
    [SerializeField] private GameObject _paintableWall;
    [SerializeField] private GameObject _confettiHolder;
    [SerializeField] private PaintIn3D.P3dChannelCounter _channelCounter;

    private void Start()
    {
        _paintableWall.SetActive(false);
        _confettiHolder.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _paintableWall.SetActive(true);
            _frontWallTween.DOPlay();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CountColor();
        }
    }

    private void CountColor()
    {
        if (_channelCounter.RatioA >= .95f && GameManager.instance.currentGameState == GameState.Playing)
        {
            _confettiHolder.SetActive(true);
            UIManager.OnCommandExecuted?.Invoke("Win Game");
        }
    }
}
