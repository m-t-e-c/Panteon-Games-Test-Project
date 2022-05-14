using UnityEngine;

public class PushObstacle : MonoBehaviour
{
    [SerializeField] [Range(0,1000f)]private float _pushForce = 200f;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent(out IPushable pushable))
        {
            pushable.Push(_pushForce);
        }
    }
}
