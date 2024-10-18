using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Spawner _spawnerPrefab;
    
    private float _baseExplosionForce = 300f;
    private float _baseExplosionRadius = 2f;
    private float _sizeEffectFactor = 1f;

    public void OnEnable()
    {
        _spawnerPrefab.CubeTriggered.AddListener(HandleCubeExplosion);
    }
    
    private void HandleCubeExplosion(Cube clickedCube)
    {
        Explode(clickedCube);
    }
    private void Explode(Cube clickedCube)
    {
        Vector3 explosionPosition = clickedCube.transform.position;
        float cubeSizeFactor = _sizeEffectFactor / clickedCube.transform.localScale.magnitude;
        float explosionForce = _baseExplosionForce * cubeSizeFactor; 
        float explosionRadius = _baseExplosionRadius * cubeSizeFactor;
        Collider[] colliders = Physics.OverlapSphere(transform.position, _baseExplosionRadius);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody) && rigidbody.gameObject != this.gameObject)
            {
                rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
            }
        }
    }
}
