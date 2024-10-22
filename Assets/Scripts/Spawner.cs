using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    
    private int _minNewCubes = 2;
    private int _maxNewCubes = 6;
    private int _scaleDenominator = 2;

    private void OnEnable()
    {
        _cubePrefab.Clicked += HandleCubeClick; 
    }

    private void OnDisable()
    {
        _cubePrefab.Clicked -= HandleCubeClick; 
    }

    private void HandleCubeClick(Cube clickedCube)
    {
        Vector3 spawnPosition = clickedCube.transform.position;
        float randomValue = Random.value;
        clickedCube.Clicked -= HandleCubeClick;

        if (randomValue <= clickedCube.SplitChance)
        {
            SpawnNewCubes(spawnPosition, clickedCube.transform.localScale / _scaleDenominator, clickedCube.SplitChance,
                clickedCube);
        }
        else
        {
            clickedCube.Explode();
        }
    }

    private void SpawnNewCubes(Vector3 parentPosition, Vector3 scale, float splitChance, Cube parentCube)
    {
        int newCubesCount = Random.Range(_minNewCubes, _maxNewCubes + 1);
        float minHeightAboveTerrain = 2f;

        for (int i = 0; i < newCubesCount; i++)
        {
            Vector3 newPosition = parentPosition + Random.insideUnitSphere;
            newPosition.y = Mathf.Max(newPosition.y, minHeightAboveTerrain);
            Cube newCube = Instantiate(parentCube);
            newCube.Initialize(newPosition, scale, splitChance);
        }
    }
}
