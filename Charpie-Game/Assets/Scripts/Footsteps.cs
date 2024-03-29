// Script Written By: Andrew

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource footstepsSound;

    void Update()
    {
        // If user is inputting wasd or arrow keys, footstep sounds will play, otherwise it will not.
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            footstepsSound.enabled = true;
        }
           
        else
        {
            footstepsSound.enabled = false;
        }
    }
}
