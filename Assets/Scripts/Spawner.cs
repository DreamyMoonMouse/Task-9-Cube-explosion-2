using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    
    private Explosion _explosion = new Explosion();
    private int _minNewCubes = 2;
    private int _maxNewCubes = 6;
    private int _scaleDenominator = 2;

    private void Awake()
    {
        if (_cubePrefab != null)
        {
            _cubePrefab.SetExplosion(_explosion);
        }
    }
    private void OnEnable()
    {
        _cubePrefab.Split += SpawnNewCubes;
    }

    private void OnDisable()
    {
        _cubePrefab.Split -= SpawnNewCubes;
    }

    private void SpawnNewCubes(Vector3 parentPosition, Vector3 scale, float splitChance)
    {
        int newCubesCount = Random.Range(_minNewCubes, _maxNewCubes + 1);
        float minHeightAboveTerrain = 2f;
        Cube parentCube = _cubePrefab;

        for (int i = 0; i < newCubesCount; i++)
        {
            Vector3 newPosition = parentPosition + Random.insideUnitSphere;
            newPosition.y = Mathf.Max(Random.insideUnitSphere.y, minHeightAboveTerrain);
            Cube newCube = Instantiate(parentCube);
            newCube.Initialize(newPosition, scale / _scaleDenominator, splitChance);
            newCube.SetExplosion(_explosion);
            
            if (newCube.TryGetComponent(out Rigidbody rigidbody))
            {
                parentCube.RegisterSpawnedCube(rigidbody); 
            }
        }
    }
}
