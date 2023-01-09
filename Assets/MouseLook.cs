using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes // associate names with settings
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float sensitivityHorizontal = 9.0f; // speed of rotation
    public float sensitivityVertical = 9.0f; // speed of rotation

    public float minimumVertical = -45.0f;
    public float maximumVertical = 45.0f;
    private float verticalRotationAngle = 0;

    void Start()
    {
        // Make the rigid body not change rotation
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }
    }

    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHorizontal, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            verticalRotationAngle -= Input.GetAxis("Mouse Y") * sensitivityVertical; // vertical angle tracked with mouse
            verticalRotationAngle = Mathf.Clamp(verticalRotationAngle, minimumVertical, maximumVertical); // clamp angle between min, max

            float horizontalRotation = transform.localEulerAngles.y; // no horizontal rotation

            transform.localEulerAngles = new Vector3(verticalRotationAngle, horizontalRotation, 0); // create new vector
        }
        else
        {
            verticalRotationAngle -= Input.GetAxis("Mouse Y") * sensitivityVertical;
            verticalRotationAngle = Mathf.Clamp(verticalRotationAngle, minimumVertical, maximumVertical);

            float delta = Input.GetAxis("Mouse X") * sensitivityHorizontal;
            float horizontalRotation = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(verticalRotationAngle, horizontalRotation, 0);
        }
    }
}