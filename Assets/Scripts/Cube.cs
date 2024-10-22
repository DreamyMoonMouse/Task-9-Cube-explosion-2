using UnityEngine;
using System;


[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    private float _chanceDenominator = 2f;
    private Explosion _explosion;
    
    public event Action<Cube> Clicked;
    
    public float SplitChance { get; private set; } = 1f;
    
    public void Initialize(Vector3 position, Vector3 scale, float splitChance)
    {
        transform.position = position;
        transform.localScale = scale;
        SplitChance = splitChance/_chanceDenominator;
        SetRandomColor();
    }
    
    public void Explode() 
    {
        _explosion.Explode(this);
    }
    
    private void Awake()
    {
        _explosion = new Explosion();
    }
    
    private void OnMouseDown()
    {
        Clicked.Invoke(this);
        Destroy(gameObject);
    }
    
    private void SetRandomColor()
    {
        GetComponent<Renderer>().material.color =  UnityEngine.Random.ColorHSV();
    }
}

