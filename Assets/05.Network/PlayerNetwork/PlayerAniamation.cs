using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManagers;
using Mirror;

public class PlayerAniamation : NetworkBehaviour
{
    public bool isLocal {get{if(NetworkServer.active) return isLocalPlayer; else return false;}}
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private Animator attackAnimator;
    private PlayerObjectControler playerObjectControler;
    private void Awake() {
        EventManager.StartListening(NetManager.StartNetworkCallback, SetLocal);
    }
    private void SetLocal()
    {
        Debug.Log("PlayerAniamation SetLocal");
        if(isLocal)
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
    private void UpdateAnimation()
    {
        playerAnimator?.SetFloat("Move", moveDir * transform.localScale.x);
    }
    private void OnDestroy() {
        EventManager.StopListening(NetManager.StartNetworkCallback, SetLocal);
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
    // ------------------ SyncVar End ------------------
    
}
