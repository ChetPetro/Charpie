using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        // Keep the camera above the player at all times
        transform.position = new Vector3(player.position.x, player.position.y, -30);
    }
}
