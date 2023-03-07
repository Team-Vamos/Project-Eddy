using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManagers;
using Mirror;

public class PlayerAniamation : NetworkBehaviour
{
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private Animator attackAnimator;
    private PlayerObjectControler playerObjectControler;
    private void Awake() {
        EventManager.StartListening(NetManager.StartGameCallback, GameStart);
    }
    private void GameStart()
    {
        if(isOwned){
            EventManager.StartListening("Attack", AttackTrigger);
        }
    }

    void Update()
    {
        UpdateAnimation();
    }
    private void AttackTrigger()
    {
        attackAnimator.SetTrigger("Attack");
        SendAttack();
        SendAttackHit( new int[]{1,2,3}, 10);
    }
    private void UpdateAnimation()
    {
        playerAnimator?.SetFloat("Move", moveDir * transform.localScale.x);
    }
    private void OnDestroy() {
        EventManager.StopListening("Attack", AttackTrigger);
    }

    // ------------------ moveDir SyncVar ------------------
    [SyncVar]
    private float moveDir;
    public void SetDir(float dir)
    {
        moveDir = dir;
        if(!isServer) CmdSetDir(moveDir);
    }
    [Command]
    private void CmdSetDir(float dir)
    {
        SetDir(dir);
    }
    // ------------------ mousePos SyncVar ------------------
    [SyncVar]
    private Vector2 mousePos;
    public void SetMousePos(Vector2 pos)
    {
        mousePos = pos;
        if(!isServer) CmdMousePos(mousePos);
    }
    [Command]
    private void CmdMousePos(Vector2 pos)
    {
        SetMousePos(pos);
    }
    public Vector2 GetMousePos()
    {
        return mousePos;
    }
    // ------------------ AttackHit SyncVar ------------------
    public void SendAttackHit(int[] targets, float damage)
    {
        if(!isServer) CmdAttackHit(targets, damage);
        else RpcAttackHit(targets, damage);
    }
    [Command]
    private void CmdAttackHit(int[] targets, float damage)
    {
        if(!isOwned)
            DoAttack(targets, damage);
        RpcAttackHit(targets, damage);
    }
    [ClientRpc]
    private void RpcAttackHit(int[] targets, float damage)
    {
        if(!isOwned)
            DoAttack(targets, damage);
    }
    private void DoAttack(int[] targets, float damage)
    {
        attackAnimator.SetTrigger("Attack");

        foreach (var target in targets)
        {
            // get target by id
            // take damage
            // if dead, remove from targets
            Debug.Log("DoAttack: " + target + " " + damage);
        }

    }
    // ------------------ AttackAni SyncVar ------------------
    public void SendAttack()
    {
        if(!isServer) CmdAttack();
        else RpcAttack();
    }
    [Command]
    private void CmdAttack()
    {
        if(!isOwned)
            attackAnimator.SetTrigger("Attack");
        RpcAttack();
    }
    [ClientRpc]
    private void RpcAttack()
    {
        if(!isOwned)
            attackAnimator.SetTrigger("Attack");
    }
}