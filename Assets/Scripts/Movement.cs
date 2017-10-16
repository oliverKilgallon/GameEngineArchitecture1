using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed = 10;
    public float rotSpeed = 5;
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

        transform.RotateAround(transform.position, Vector3.up, xRot * rotSpeed);
        transform.position += movement * speed * Time.deltaTime;
    }
}
