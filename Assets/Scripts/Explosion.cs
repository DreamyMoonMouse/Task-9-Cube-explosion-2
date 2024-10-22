using UnityEngine;

public class Explosion
{
    private float _baseExplosionForce = 300f;
    private float _baseExplosionRadius = 2f;
    private float _sizeEffectFactor = 1f;
    
    public void Explode(Cube clickedCube)
    {
        Vector3 explosionPosition = clickedCube.transform.position;
        float cubeSizeFactor = _sizeEffectFactor / clickedCube.transform.localScale.magnitude;
        float explosionForce = _baseExplosionForce * cubeSizeFactor; 
        float explosionRadius = _baseExplosionRadius * cubeSizeFactor;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, _baseExplosionRadius);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody) && rigidbody.gameObject != clickedCube.gameObject)
            {
                rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
            }
        }
    }
}
