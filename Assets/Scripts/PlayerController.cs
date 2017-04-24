using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody myRigidbody;
    public float acceleration = 1f;
    public float maxWalkSpeed = 5f;

    private GameObject mainCamera;
    private SceneController sceneController;
    private bool move = false;
    private bool isInMenu = true;

    private void Start()
    {
        mainCamera = Camera.main.gameObject;
    }

    private void Update()
    {
        if (!isInMenu && GvrViewer.Instance.Triggered)
        {
            move = !move;
            //Debug.Log(move);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnLevelWasLoaded(int level)
    {
        sceneController = FindObjectOfType<SceneController>();
        isInMenu = sceneController.gameManager.IsInMenu();
        Spawn();
    }

    private void Spawn()
    {
        move = false;
        transform.position = sceneController.spawnSpot.transform.position;
        myRigidbody.velocity = Vector3.zero;
        myRigidbody.isKinematic = isInMenu;
    }

    private void Move()
    {
        if (move)
        {
            Vector3 direction = new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z).normalized;
            //Debug.Log(myRigidbody.velocity.magnitude);
            if (myRigidbody.velocity.magnitude < maxWalkSpeed)
            {
                myRigidbody.AddForce(direction * acceleration *Time.deltaTime, ForceMode.Force);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            sceneController.ReturnToMenu();
        }
    }

}
