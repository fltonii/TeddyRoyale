using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 6f;
    private Vector3 movement;
    private Animator anim;
    private Rigidbody playerRigidbody;
    private int floorMask;
    float camRayLength = 100f;

    private void Awake() {
        floorMask = LayerMask.GetMask ("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    } 

    void FixedUpdate() {
        float h = Input.GetAxisRaw ("Horizontal");
        float v = Input.GetAxisRaw ("Vertical");
        // Debug.Log("horizontal: " + h.ToString() + "vertical: " + v.ToString());
        
        Move(h, v);
        Turning();
        Animating(h, v);
    }
    
    private void Move(float h, float v) {
        movement.Set(h, 0.0f, v); 

        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    private void Turning() {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);  
        }
    }
    
    private void Animating(float h, float v) {
        bool walking = h != 0.0f || v != 0.0f;

        anim.SetBool("IsWalking", walking);
    }
}
