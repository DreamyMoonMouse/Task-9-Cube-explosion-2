using UnityEngine;
using System.Collections.Generic;

public class Explosion
{
    private float _baseExplosionForce = 300f;
    private float _baseExplosionRadius = 2f;
    private float _sizeEffectFactor = 1f;
    private float _splitExplosionForceMultiplier = 0.01f; 
    
    public void Explode(List<Rigidbody> targetCubes, bool isSplit, Cube clickedCube)
    {
        Vector3 explosionPosition = clickedCube.transform.position;
        float cubeSizeFactor = _sizeEffectFactor / clickedCube.transform.localScale.magnitude;
        float explosionForce = _baseExplosionForce * cubeSizeFactor; 
        float explosionRadius = _baseExplosionRadius * cubeSizeFactor;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, _baseExplosionRadius);

        if (isSplit)
        {
            foreach (Rigidbody rigidbody in targetCubes)
            {
                if (rigidbody != null)
                {
                    Vector3 direction = Random.insideUnitSphere.normalized;
                    rigidbody.AddForce(direction * explosionForce* _splitExplosionForceMultiplier, ForceMode.Impulse);
                }
            }
        }
        else
        {
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out Rigidbody rigidbody) && rigidbody.gameObject != clickedCube.gameObject)
                {
                    rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
                }
            }
        }
    }
}
