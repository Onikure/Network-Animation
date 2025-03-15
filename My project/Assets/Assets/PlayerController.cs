using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    private Animator animator;
    private CharacterController controller;
    private Vector3 moveDirection;
    public float speed = 5f;
    public float jumpForce = 8f;
    public float gravity = 9.81f;

    void Start()
    {
        if (!IsOwner) return;

        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!IsOwner) return;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ) * speed;
        controller.Move(moveDirection * Time.deltaTime);

        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpForce;
                animator.SetTrigger("Jump");
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                animator.SetBool("Crouch", true);
            }
            else
            {
                animator.SetBool("Crouch", false);
            }

            if (moveX != 0 || moveZ != 0)
            {
                animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
    }
}
