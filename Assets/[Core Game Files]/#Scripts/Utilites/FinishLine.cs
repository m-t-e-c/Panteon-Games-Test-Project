using UnityEngine;
using DG.Tweening;

public class FinishLine : MonoBehaviour
{

    // References
    [SerializeField] private GameObject _confetties;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _confetties.SetActive(true);
        }
    }
}
