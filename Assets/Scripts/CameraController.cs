using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Get the distance between the camera and the player
    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // This make the camera follow the player from the distance specified by the offset
    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
