using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private Explosion explosionPrefab;
    
    private int _minNewCubes = 2;
    private int _maxNewCubes = 6;
    private int _scaleDenominator = 2;
    
    public void OnCubeClicked(Cube clickedCube)
    {
        Vector3 spawnPosition = clickedCube.transform.position;
        float randomValue = Random.value;
        Debug.Log($"Log: Random.value: {randomValue}, splitChance: {clickedCube.GetSplitChance()}");
        
         if (randomValue <= clickedCube.GetSplitChance())
         {
            SpawnNewCubes(spawnPosition, clickedCube.transform.localScale / _scaleDenominator,clickedCube.GetSplitChance());
         }
         
        explosionPrefab.Explode();
        Destroy(clickedCube.gameObject); 
    }
    
    private void SpawnNewCubes(Vector3 parentPosition, Vector3 newScale,float newSplitChance)
    {
        int newCubesCount = Random.Range(_minNewCubes, _maxNewCubes + 1);
        float minHeightAboveTerrain = 0.1f;
        Debug.Log($"Log: CubesCount Random.value: {newCubesCount}");

        for (int i = 0; i < newCubesCount; i++)
        {
            Vector3 newPosition = parentPosition + Random.insideUnitSphere;
            newPosition.y = Mathf.Max(newPosition.y, minHeightAboveTerrain);
            Cube newCube = Instantiate(cubePrefab);
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            newCube.Initialize(newPosition, newScale, newSplitChance, randomColor);
        }
    }
}
