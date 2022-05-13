using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject _confetties;
    [SerializeField] private GameObject _paintWall;
    [SerializeField] private DOTweenAnimation _wallGateTween;

    [SerializeField] private List<Transform> _aiStandPoints;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _paintWall.SetActive(true);
            _wallGateTween.DOPlay();
            _confetties.SetActive(true);
        }
    }

    public Transform GetStandPoint()
    {
        if (_aiStandPoints.Count == 0)
            return null;
        
        Transform go = _aiStandPoints[0];
        _aiStandPoints.RemoveAt(0);
        return go;
    }
}
