using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 10.0f;
    [SerializeField] private float backingSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 100.0f;
    [SerializeField] private float interactionRange = 4.0f;
    [SerializeField] private Transform robotHead;
    [SerializeField] private GameObject raycastTarget;
    Rigidbody rb;
    private Vector3 jumpVector;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpHeight;
    private bool isGrounded;
    RaycastHit hitInfo;

    private Animator animator;
    public event Action OnActivatingTurret;
    // Start is called before the first frame update
    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * forwardSpeed * Time.deltaTime;
            animator.SetFloat("Speed", forwardSpeed);           
        } else if (Input.GetKeyUp(KeyCode.W)) animator.SetFloat("Speed", 0);
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * backingSpeed * Time.deltaTime;
            animator.SetFloat("Speed", backingSpeed);
        } else if (Input.GetKeyUp(KeyCode.S)) animator.SetFloat("Speed", 0);
        float h = Input.GetAxis("Horizontal");
        transform.Rotate(0, h * Time.deltaTime * rotationSpeed, 0);
        animator.SetFloat("Rotation", h);
        
    }

    void Interact()
    {
        hitInfo = new RaycastHit();
        bool interacting = Physics.Raycast(robotHead.transform.position, transform.forward, out hitInfo, interactionRange);
        Debug.DrawRay(robotHead.transform.position, transform.forward * interactionRange, Color.red, 0.1f);
        if (interacting & Input.GetKeyDown(KeyCode.E))
        {
            if(hitInfo.collider.gameObject.tag == "NerveColumn")
            {
                print("Talked to Nerve Column");
            }
            if (hitInfo.collider.gameObject.tag == "InactiveTurret")
            {
                print("Activated Receptor Turret");
                raycastTarget = hitInfo.collider.gameObject;
                
                // Aquí quiero suscribir al método "ActivatingTurret" de la Clase Turret
                
            }
        }
    }

    private bool DetectGround()
    {
        Vector3 down = transform.TransformDirection(Vector3.down);
        return Physics.Raycast(transform.position, down, 0.2f);
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(jumpVector * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.applyRootMotion = false;
        rb = GetComponent<Rigidbody>();
        jumpVector = new Vector3(0.0f, jumpHeight, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Interact();
        isGrounded = DetectGround();
        if (isGrounded == false)
            PostProcessingManager.instance.ActivateLowerVignette();
        else
            PostProcessingManager.instance.DisableLowerVignette();
    }
    private void FixedUpdate()
    {
        Jump();
    }
}
