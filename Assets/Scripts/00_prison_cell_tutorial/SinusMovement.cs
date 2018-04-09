using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusMovement : MonoBehaviour
{

    public float amplitude;
    public float frequency;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += amplitude * (Mathf.Sin(2 * Mathf.PI * frequency * Time.time) - Mathf.Sin(2 * Mathf.PI * frequency * (Time.time - Time.deltaTime))) * transform.up;
    }
}
