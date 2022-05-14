using UnityEngine;
using DG.Tweening;

public class FinishLine : MonoBehaviour
{

    // References
    [SerializeField] private GameObject _confetties;
    [SerializeField] private GameObject _paintWall;
    [SerializeField] private DOTweenAnimation _wallGateTween;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _paintWall.SetActive(true);
            _wallGateTween.DOPlay();
            _confetties.SetActive(true);
        }
    }
}
