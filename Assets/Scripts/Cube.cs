using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private float _chanceDenominator = 2f;
    private Explosion _explosion;
    private float _splitChance = 1f;
    private float _randomeValue = 0f;
    private List<Rigidbody> _spawnedCubes = new List<Rigidbody>();
    
    public event Action<Vector3, Vector3, float> Split;
    
    private void OnMouseDown()
    {
        Spliter();
        Exploder();
        Destroy(gameObject);
    }
    
    public void Initialize(Vector3 position, Vector3 scale, float splitChance)
    {
        _randomeValue = UnityEngine.Random.value;
        transform.position = position;
        transform.localScale = scale;
        _splitChance = splitChance/_chanceDenominator;
        SetRandomColor();
    }
    
    public void SetExplosion(Explosion explosion)
    {
        _explosion = explosion;
    }
    
    public void RegisterSpawnedCube(Rigidbody cubeRigidbody)
    {
        _spawnedCubes.Add(cubeRigidbody);
    }
    
    private void Explode(bool isSplit) 
    {
        _explosion.Explode(_spawnedCubes, isSplit, this);
        _spawnedCubes.Clear();
    }
    
    private void SetRandomColor()
    {
        GetComponent<Renderer>().material.color =  UnityEngine.Random.ColorHSV();
    }
    
    private void Spliter()
    {
        if (_randomeValue <= _splitChance)
        {
            Split?.Invoke(transform.position, transform.localScale, _splitChance);
        }
    }
    
    private void Exploder()
    {
        if (_randomeValue <= _splitChance)
        {
            
            Explode(true);
        }
        else
        {
            Explode(false);
        }
    }
}

