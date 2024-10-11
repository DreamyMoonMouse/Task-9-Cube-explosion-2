using UnityEngine;
using UnityEngine.Serialization;


public class Cube : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    
    private float _splitChance = 1f;
    private float _chanceDenominator = 2f;
    
    public void Initialize(Vector3 position, Vector3 scale, float splitChance, Color color)
    {
        transform.position = position;
        transform.localScale = scale;
        _splitChance = splitChance/_chanceDenominator;
        GetComponent<Renderer>().material.color = color;
    }

    private void OnMouseDown()
    {
        spawner.OnCubeClicked(this);
    }
    
    public float GetSplitChance()
    {
        return _splitChance;
    }
}

