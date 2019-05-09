using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    
    public float rotateSpeed;

    private Renderer render;
    private Vector3 rotate;
    private Color pickUpColor;

    private void Start()
    {
        render = GetComponent<Renderer>();
        pickUpColor = new Color(Random.value, Random.value, Random.value);
    }

    // Update is called once per frame
    void Update()
    {
        rotate = new Vector3(15, 30, 45);
        render.material.color = pickUpColor;
        transform.Rotate(rotate * (rotateSpeed * Time.deltaTime));
    }
}  