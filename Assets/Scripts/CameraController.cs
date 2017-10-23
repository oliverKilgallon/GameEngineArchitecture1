using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 10;
    public float rotSpeed = 5;
    public GameObject player;
    private Vector3 startPos;
    private Quaternion startRot;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        ProcessInputs();
        transform.LookAt(player.transform);
        if (PlayerController.isInEditor.Equals(true))
        {
            gameObject.GetComponent<Movement>().Move();
        }
    }

    public void ResetPos()
    {
        transform.position = startPos;
        transform.rotation = startRot;
    }

    void ProcessInputs()
    {
        if (Input.GetKeyDown("f") && PlayerController.isInEditor.Equals(false))
        {
            startPos = transform.position;
            startRot = transform.rotation;
        }
        if (Input.GetKeyDown("f") && PlayerController.isInEditor.Equals(true))
        {
            ResetPos();
        }
    }

    void LateUpdate()
    {
        if (PlayerController.isInEditor.Equals(false))
        {
            transform.position = player.transform.position + offset;
        }
    }
}
