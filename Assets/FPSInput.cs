using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    private CharacterController charController;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement.y = gravity;


        movement = Vector3.ClampMagnitude(movement, speed); // limit diagonal movement to same speed as along axis
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement); // world movement (instead of player movement)
        charController.Move(movement);

        transform.Translate(deltaX * Time.deltaTime, 0, deltaZ * Time.deltaTime);
    }
}