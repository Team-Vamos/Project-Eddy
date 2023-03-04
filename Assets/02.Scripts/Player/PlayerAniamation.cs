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
    [SyncVar]
    private float moveDir;
    private void Awake() {
        EventManager.StartListening(NetManager.StartNetworkCallback, SetLocal);
    }
    private void SetLocal()
    {
        if(isLocalPlayer)
            EventManager.StartListening("Attack", AttackTrigger);
    }
    void Update()
    {
        UpdateAnimation();
    }
    private void AttackTrigger()
    {
        attackAnimator?.SetTrigger("Attack");
    }

    public void SetDir(float dir)
    {
        moveDir = dir;
    }
    private void UpdateAnimation()
    {
        playerAnimator?.SetFloat("Move", moveDir);
    }
    [Command]
    private void CmdSetDir(float dir)
    {
        SetDir(dir);
    } 
    private void OnDestroy() {
        EventManager.StopListening(NetManager.StartNetworkCallback, SetLocal);
        EventManager.StopListening("Attack", AttackTrigger);
    }
}
