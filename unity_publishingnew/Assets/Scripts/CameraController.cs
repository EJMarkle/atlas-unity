using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    private Transform playerLoc;
    
    /// preload player location
    void Start()
    {
        playerLoc = player.transform;

        if (offset == Vector3.zero)
        {
            offset = transform.position - playerLoc.position;
        }
    }

    /// set camera to player location
    void LateUpdate()
    {
        Vector3 newPosition = playerLoc.position + offset;
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(75f, 0f, 0f);
    }
}
