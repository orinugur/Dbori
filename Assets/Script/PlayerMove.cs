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
        Cursor.lockState = CursorLockMode.Locked;  // Ŀ�� ���
        // Cursor.visible = false;  // Ŀ�� �����
        Mz = Muzzle.GetComponent<ParticleSystem>();

    }

    void Update()
    {
        // ���콺 �Է��� ���� ���� ��ȯ
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        MoveOrder();

        // �߷� ����
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float currentGravity = isGravityInverted ? -gravity : gravity;
        velocity.y += currentGravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void OnMove(InputValue inputValue) // �̵�(WASD)
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

        if (isClick == 1)   // ������ ��
        {
            GunFire();
        }
        else // �� ��
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
                // ���� �Ÿ� ������ �浹 ����
                if (hit.distance >= 2f) // ���⼭ 2f�� �ּ� �Ÿ��Դϴ�. �ʿ信 ���� �����ϼ���.
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
                // ���� �Ÿ� ������ �浹 ����
                if (hit.distance >= 2f) // ���⼭�� �ּ� �Ÿ� Ȯ��
                {
                    // �浹 ó�� ���� �߰�
                }
            }
            Debug.DrawRay(gun.transform.position, attackDirection * range, Color.blue);

            // �Ѿ� ���� �� �߻�
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

        // ���ϰ� �ִ� �߿��� ������ �Ұ���
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
