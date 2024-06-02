using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingAnimation : MonoBehaviour
{
    public float frequency;
    public float magnitude;
    public Vector3 direction;
    Vector3 initialposition;
    void Start()
    {
        initialposition = transform.position;    
    }
    void Update()
    {
        transform.position = initialposition + direction * Mathf.Sin(Time.time * frequency) * magnitude;        
    }
}
