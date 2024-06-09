using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class NetRoomPlayingDove : NetworkBehaviour
{
    [Header("Components")]
    public Animator Animator_Player;


    [Header("Movement")]
    public float _rotationSpeed = 100.0f;
    public float moveSpeed = 10f;
    private Vector2 moveInput;
    Rigidbody rd;

    [Header("Attack")]
    public KeyCode _atkKey = KeyCode.Space;

    void Start()
    {
        rd = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (CheckIsFocusedOnUpdate() == false)
        {
            return;
        }

        CheckIsLocalPlayerOnUpdate();
    }

    private bool CheckIsFocusedOnUpdate()
    {
        return Application.isFocused;
    }

    private void CheckIsLocalPlayerOnUpdate()
    {
        if (isLocalPlayer == false)
            return;

        // 이동
        MoveCharacter();

        Animator_Player.SetBool("Walk", true);

        // 공격
        if (Input.GetKeyDown(_atkKey))
        {
            CmdAttack();
        }

        RotateLocalPlayer();
    }

    [Command]
    void CmdAttack()
    {
        RpcOnAtk();
    }

    [ClientRpc]
    void RpcOnAtk()
    {
        Animator_Player.SetTrigger("Roll");
    }

    void RotateLocalPlayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    private void MoveCharacter()
    {
        Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        Vector3 movement = moveDir * moveSpeed;
        rd.velocity = movement;

        // 플레이어가 이동하는 방향을 바라보도록 회전
        if (moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    public void OnMove(InputValue inputValue)
    {
        if (inputValue != null) moveInput = inputValue.Get<Vector2>();
    }
}
