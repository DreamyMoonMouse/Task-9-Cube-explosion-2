using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float _explosionForce = 300f;
    private float _explosionRadius = 2f;

    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody) && rigidbody.gameObject != this.gameObject)
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }
    }
}
