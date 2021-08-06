using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiramidAroundTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PersonController.singlton.cameraDistans = 20;
            PiramidController.projectileSpeed = 0.5f;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PersonController.singlton.cameraDistans = 10;
            PiramidController.projectileSpeed = 3f;
        }
    }
}
