using UnityEngine;
using UnityEngine.Serialization;
using System;
using UnityEngine.Events;

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    public UnityEvent<Cube> OnCubeClicked;
    private float _chanceDenominator = 2f;
    
    public float SplitChance { get; private set; } = 1f;
    
    private void OnMouseDown()
    {
        OnCubeClicked.Invoke(this);
        Destroy(gameObject);
    }
    public void Initialize(Vector3 position, Vector3 scale, float splitChance)
    {
        transform.position = position;
        transform.localScale = scale;
        SplitChance = splitChance/_chanceDenominator;
        SetRandomColor();
    }
    
    private void SetRandomColor()
    {
        GetComponent<Renderer>().material.color =  UnityEngine.Random.ColorHSV();
    }
}

