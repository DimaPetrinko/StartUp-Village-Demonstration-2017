using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody myRigidbody;
    public float acceleration = 1f;
    public float maxWalkSpeed = 5f;
    public float maxHeadTiltAngle = 45f;

    private GameObject mainCamera;
    private SceneController sceneController;
    private GvrPointerInputModule gvrpim;
    private bool move = false;
    private bool isInMenu = true;
    private bool headWasTilted = false;

    private bool HeadTilted
    {
        get
        {
            float angle = mainCamera.transform.eulerAngles.z;
            if (angle > 180) angle -= 360f;
            return Mathf.Abs(angle) >= maxHeadTiltAngle;
        }
    }

    private void Start()
    {
        mainCamera = Camera.main.gameObject;
        gvrpim = FindObjectOfType<GvrPointerInputModule>();
    }

    private void Update()
    {
        bool headTrigger = !headWasTilted && HeadTilted;
        if (headTrigger)
        {
            headWasTilted = true;
            gvrpim.HandleTriggerDown();
        }
        if (!HeadTilted)
            headWasTilted = false;
            //Debug.Log(headTrigger);
        if (!isInMenu && (GvrViewer.Instance.Triggered || headTrigger))
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
        transform.rotation = sceneController.spawnSpot.transform.rotation;
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
