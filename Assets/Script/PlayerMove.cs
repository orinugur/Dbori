using Mirror;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class PlayerMove : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 100.0f;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    private bool isGravityInverted = false;
    private Vector2 moveInput = Vector2.zero;
    private Vector2 moveVector = Vector2.zero;
    public Transform cameraTransform;
    public bool isFire = false;
    public bool isReload = false;
    public bool canFire = true;
    private float moveSpeed = 2f;
    public Animator animator;
    public int shell;
    public int maxShell;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;  // 커서 잠금
        // Cursor.visible = false;  // 커서 숨기기
    }

    void Update()
    {
        // 마우스 입력을 통한 시점 변환
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        MoveOrder();

        // 중력 적용
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float currentGravity = isGravityInverted ? -gravity : gravity;
        velocity.y += currentGravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void OnMove(InputValue inputValue) // 이동(WASD)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    void OnSprint(InputValue inputValue)
    {
        float value = inputValue.Get<float>();
        moveSpeed = (value * 2f) + 2f;
    }

    void OnFire(InputValue inputValue)
    {
        float isClick = inputValue.Get<float>();

        if (isClick == 1)   // 눌렀을 때
        {
            isFire = true;
        }
        else // 뗄 때
        {
            isFire = false;
        }
    }
    void GunFire()
    {
        if (canFire && isFire && shell > 0 && !isReload)
        {
            canFire= false;
            //EffectManager.Instance.FireEffectGenenate(firePos.position, firePos.rotation);
            animator.SetTrigger("Fire");
            shell--;
        }
    }
    public void FireDelay()
    {
        canFire = true;
    }

    void OnReload(InputValue inputValue)
    {
        float isClick = inputValue.Get<float>();

        // 줌하고 있는 중에는 재장전 불가능
        if (!isReload)
        {
            isReload = true;
            Reload();
        }
    }
    void Reload()
    {
            animator.SetTrigger("Reload");
            //reload.PlayOneShot(reload.clip);
            StartCoroutine(ReloadEnd());
    }
    IEnumerator ReloadEnd()
    {
        yield return new WaitForSeconds(1.4f);
        isReload = false;
        shell = maxShell;
    }

    private void MoveOrder()
    {
        moveVector = Vector2.Lerp(moveVector, moveInput * moveSpeed, Time.deltaTime * 5);

        Vector3 moveVector3 = transform.right * moveVector.x + transform.forward * moveVector.y;
        controller.Move(moveVector3 * Time.deltaTime);

        animator.SetFloat("XSpeed", moveVector.x);
        animator.SetFloat("ZSpeed", moveVector.y);
    }
}
