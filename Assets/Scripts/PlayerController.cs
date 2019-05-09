using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [Range(1, 20)]
    public float speed;

    private bool allowMotion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        allowMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowMotion)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * speed);
        }
    }

    // Prevent the player from moving before the timer start
    public bool AllowMotion
    {
        set { allowMotion = value; }
        get { return allowMotion; }
    }
}
