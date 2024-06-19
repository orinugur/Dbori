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
    public int maxShell = 6;
    public Camera mainCamera;
    public GameObject bulletSell;
    public GameObject Muzzle;
    public ParticleSystem Mz;

    Ray ray;
    RaycastHit hit;
    Vector3 targetPoint = Vector3.zero;
    public GameObject gun;
    public float range=100f;
    public GameObject Bullet;
    public float bulletSpeed = 2000f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;  // 커서 잠금
        // Cursor.visible = false;  // 커서 숨기기
        Mz = Muzzle.GetComponent<ParticleSystem>();

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
            GunFire();
        }
        else // 뗄 때
        {
            isFire = false;
        }
    }
    void GunFire()
    {
        if (canFire && !isFire && shell > 0 && !isReload)
        {
            animator.SetTrigger("Fire");
            shell--;
            Mz.Play();
            Debug.Log("fire");

            ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f);
            if (Physics.Raycast(ray, out hit, range))
            {
                // 일정 거리 이하의 충돌 무시
                if (hit.distance >= 2f) // 여기서 2f는 최소 거리입니다. 필요에 따라 조정하세요.
                {
                    targetPoint = hit.point;
                }
                else
                {
                    targetPoint = ray.origin + ray.direction * range;
                }
            }
            else
            {
                targetPoint = ray.origin + ray.direction * range;
            }
            Debug.DrawRay(ray.origin, ray.direction * range, Color.red);
            Vector3 attackDirection = (targetPoint - gun.transform.position).normalized;
            if (Physics.Raycast(gun.transform.position, attackDirection, out hit, range))
            {
                // 일정 거리 이하의 충돌 무시
                if (hit.distance >= 2f) // 여기서도 최소 거리 확인
                {
                    // 충돌 처리 로직 추가
                }
            }
            Debug.DrawRay(gun.transform.position, attackDirection * range, Color.blue);

            // 총알 생성 및 발사
            GameObject bullet = Instantiate(Bullet, gun.transform.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(attackDirection * bulletSpeed, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("cant fire");
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
