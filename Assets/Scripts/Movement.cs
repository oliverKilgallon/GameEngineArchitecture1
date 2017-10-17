using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 10;
    public float rotSpeed = 5;
    
    private Quaternion startRot;
    private bool resetRot = false;
    private bool rotating = false;
    private float resetSpeed = 1f;

    void Start()
    {
        startRot = gameObject.transform.rotation;
        
    }
    public void Move()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        float xRot = Input.GetAxis("xRot");

        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);

        forward.y = 0f;
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0f, -forward.x);
        Vector3 movement = horiz * right + vert * forward;

        if (!rotating)
        {
            transform.RotateAround(transform.position, Vector3.up, xRot * rotSpeed);
            transform.position += movement * speed * Time.deltaTime;
        }
    }

    void Update()
    {
        if (Input.GetKeyUp("r"))
        {
            if (gameObject.GetComponent<PlayerController>() == null)
            {
                if (gameObject.name.Equals("Main Camera"))
                {
                    gameObject.GetComponent<Movement>().ResetRot();
                }
            }
            if (gameObject.GetComponent<PlayerController>() != null)
            {
                gameObject.GetComponent<Movement>().ResetRot();
            }
        }
        
        if (resetRot.Equals(true))
        {
            if (!transform.rotation.Equals(startRot))
            {
                rotating = true;
                transform.rotation = Quaternion.Lerp(transform.rotation, startRot, resetSpeed * (Time.time % 1));
            }
            else
            {
                rotating = false;
                resetRot = false;
            }
        }
    }
    public void ResetRot()
    {
        //transform.rotation = startRot;
        resetRot = true;
    }
}
