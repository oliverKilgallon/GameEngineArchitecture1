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

    void Awake()
    {
        SpawnPlayer.playerCreate += ParentCamera;
    }

    void OnDisable()
    {
        SpawnPlayer.playerCreate -= ParentCamera;
    }

    void Start()
    {
        
    }
    void ParentCamera()
    {
        gameObject.transform.SetParent(player.transform);
        transform.position = new Vector3(0f, transform.position.y, 0f);
    }

    void Update()
    {
        ProcessInputs();
        
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

            gameObject.transform.SetParent(null);
        }
        if (Input.GetKeyDown("f") && PlayerController.isInEditor.Equals(true))
        {
            ResetPos();

            gameObject.transform.SetParent(player.transform);
        }
    }

    void LateUpdate()
    {
        if (PlayerController.isInEditor.Equals(false))
        {
            transform.LookAt(player.transform);
        }
    }
}
