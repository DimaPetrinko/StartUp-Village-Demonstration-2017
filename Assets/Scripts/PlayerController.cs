using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rigidbody;

    private SceneController sceneController;

    private void OnLevelWasLoaded(int level)
    {
        sceneController = FindObjectOfType<SceneController>();
        Spawn();
    }

    private void Spawn()
    {
        transform.position = sceneController.spawnSpot.transform.position;
        if (!sceneController.gameManager.IsInMenu())
        {
            rigidbody.isKinematic = true;
        }
        else
        {
            rigidbody.isKinematic = false;
        }
    }

}
