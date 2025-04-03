using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private static readonly int FIRE = Animator.StringToHash("Fire");
    private static readonly int SWAP = Animator.StringToHash("Swap");


    [Header("이동 설정")]
    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float jumpHeight = 1.2f;
    public float gravity = -9.81f;
    
    [Header("카메라 설정")]
    public Transform cameraTransform;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 80f;
    
    // 바닥 체크 관련
    [Header("Ground Check")]
    public float groundCheckDistance = 0.1f;
    public LayerMask groundMask = ~0; // 기본은 모든 레이어
    
    [Header("Animator")]
    public Animator animator;
    
    public Weapon currentWeapon;

    [SerializeField] private Weapon[] weapons;
    
    private CharacterController controller;
    private Vector3 velocity;
    private float verticalLookRotation = 0f;
    private Quaternion initialCameraRotation;

    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        initialCameraRotation = cameraTransform.localRotation;

        EquipWeapon(weapons[0]);
    }

    void SwapWeapon(int loc)
    {
        if (currentWeapon.Equals(weapons[loc]) is false)
        {
            EquipWeapon(weapons[loc]);
        }
    }

    void EquipWeapon(Weapon weapon)
    {
        currentWeapon.gameObject.SetActive(false);
        currentWeapon = weapon;
        currentWeapon.gameObject.SetActive(true);

        animator.ResetTrigger(SWAP);
        animator.SetTrigger(SWAP);
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateMovement();
        UpdateCameraRotation();
        UpdateInput();
    }

    private void UpdateMovement()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * (speed * Time.deltaTime));

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        bool isGrounded = controller.isGrounded;
        //if (isGrounded == false) isGrounded = IsGroundedBySpherecast();
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void UpdateCameraRotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        Quaternion yawRotation = Quaternion.AngleAxis(mouseX, Vector3.up);
        transform.rotation *= yawRotation;

        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -maxLookAngle, maxLookAngle);

        Quaternion pitchRotation = Quaternion.AngleAxis(verticalLookRotation, Vector3.right);
        cameraTransform.localRotation = initialCameraRotation * pitchRotation;
    }

    
    private void UpdateInput()
    {
        if (Input.GetMouseButton(0))
        {
            bool successFire = currentWeapon.Fire();

            if (successFire)
            {
                // anystate를 통해 transition
                animator.ResetTrigger(FIRE);
                animator.SetTrigger(FIRE);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwapWeapon(2);
        }
    }
    
    private bool IsGroundedBySpherecast()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f; // 약간 위에서 시작
        float rayLength = (controller.height / 2) + groundCheckDistance;

        return Physics.SphereCast(origin, controller.radius * 0.9f, Vector3.down, out _, rayLength, groundMask);
    }

    private void OnDrawGizmos()
    {
        if (controller == null) return;

        Gizmos.color = IsGroundedBySpherecast() ? Color.green : Color.red;

        Vector3 origin = transform.position + Vector3.up * 0.1f;
        float rayLength = (controller.height / 2) + groundCheckDistance;
        Gizmos.DrawWireSphere(origin + Vector3.down * rayLength, controller.radius * 0.9f);
    }
    
}
