using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManagers;
using Mirror;
using Card;


public class PlayerAttack : NetworkBehaviour
{
    private PlayerAniamation playerAniamation;
    [SerializeField]
    private Transform center;
    [SerializeField]
    private float attackAngle;
    public float AttackAngle => attackAngle;
    [SerializeField]
    private float attackRange;
    public float AttackRange => attackRange;
    [SerializeField]
    private float attackArc;
    public float AttackArc => attackArc;

    [Header("AnimationAngleLemit")]

    [SerializeField]
    private float minAnimationAngle;
    [SerializeField]
    private float maxAnimationAngle;
    [SerializeField]
    private LayerMask enemyLayerMask;
    public LayerMask EnemyLayerMask => enemyLayerMask;
    [SerializeField]
    private int attackDamage;
    public int AttackDamage => attackDamage;

    [SerializeField]
    private WeaponCardBaseSO _defaultWeapon;

    private WeaponCardController _playerWeapon;

    // Start is called before the first frame update
    void Awake()
    {
        playerAniamation = GetComponent<PlayerAniamation>();
        EventManager.StartListening(NetManager.StartGameCallback, SetDefaultPlayerWeapon);
        
    }

    private void SetDefaultPlayerWeapon()
    {
        _playerWeapon ??= _defaultWeapon.CreateCardController(GetComponent<CardHandler>()) as WeaponCardController;
        _playerWeapon?.SetUserAttack(this);
    }
    public void DoAttack()
    {
        _playerWeapon.Attack();
    }

    // Update is called once per frame
    void Update()
    {
        LookMouse2D();
        UpdateAttact();
    }
    private void UpdateAttact()
    {
        if(!playerAniamation.isOwned) return;
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }
    public void Attack()
    {
        EventManager.TriggerEvent("AttackAni");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        GizmosExtensions.DrawWireArc(center.position, 
            Quaternion.AngleAxis(attackAngle, Vector3.down) 
            * Vector3.left, attackArc, attackRange);


        Gizmos.color = Color.blue;

        GizmosExtensions.DrawWireArc(center.position, 
            Quaternion.AngleAxis(maxAnimationAngle/2f, Vector3.down) 
            * Vector3.right, -maxAnimationAngle, 1f);


        GizmosExtensions.DrawWireArc(center.position, 
            Quaternion.AngleAxis(minAnimationAngle/2f, Vector3.down) 
            * Vector3.right, minAnimationAngle, 1f);


        //Gizmos.DrawWireCube(, attackRange, attackAngle);
    }
    public Vector3 GetHitDir(Transform enemy, float weaponHardness)
    {
        Vector3 AttackStartPos = 
            center.position+(Quaternion.AngleAxis(attackAngle 
            + (transform.localScale.x * -AttackArc/2f), Vector3.forward)) 
            * Vector3.left * (Vector2.Distance(enemy.position, center.position) * weaponHardness);

        Vector3 EnemyPos = enemy.position;
        AttackStartPos.z = 0;
        EnemyPos.z = 0;
            
        Debug.DrawLine(AttackStartPos, EnemyPos, Color.red, 1f);
        

        return (AttackStartPos - EnemyPos).normalized;
    }

    public static IDamageTaker[] GetArcTargetsAll(Vector3 position, float anglesRange, float radius, float attackAngle, LayerMask layerMask)
    {
        List<IDamageTaker> targets = new List<IDamageTaker>();
        RaycastHit2D[] raycastHit2Ds;

        var angle = attackAngle - anglesRange / 2;
        var rad = Mathf.Deg2Rad * angle;
        var initialPos = new Vector3(position.x - radius * Mathf.Cos(rad), position.y - radius * Mathf.Sin(rad), 0);

        raycastHit2Ds = Physics2D.LinecastAll(position, initialPos, layerMask);
        foreach (var raycastHit2D in raycastHit2Ds)
        {
            IDamageTaker iDamageTaker = raycastHit2D.collider.gameObject.GetComponent<IDamageTaker>();
            if(iDamageTaker != null && !targets.Contains(iDamageTaker))
                targets.Add(iDamageTaker);
        }

        angle = attackAngle + anglesRange / 2;
        rad = Mathf.Deg2Rad * angle;
        initialPos = new Vector3(position.x - radius * Mathf.Cos(rad), position.y - radius * Mathf.Sin(rad), 0);
        raycastHit2Ds = Physics2D.LinecastAll(position, initialPos, layerMask);
        foreach (var raycastHit2D in raycastHit2Ds)
        {
            IDamageTaker iDamageTaker = raycastHit2D.collider.gameObject.GetComponent<IDamageTaker>();
            if(iDamageTaker != null && !targets.Contains(iDamageTaker))
                targets.Add(iDamageTaker);
        }

        Collider2D[] collider2Ds =
        Physics2D.OverlapCircleAll(position, radius, layerMask);
        foreach (var collider in collider2Ds)
        {
            float finalAngle = GetAngle(position, collider.transform.position) + 180f;
            //Debug.Log("angle"+finalAngle);
            Debug.DrawLine(position, collider.transform.position, Color.red, 1f);
            if (finalAngle >= attackAngle - anglesRange / 2f && finalAngle <= attackAngle + anglesRange / 2f)
            {
                
                IDamageTaker iDamageTaker = collider.gameObject.GetComponent<IDamageTaker>();
                if(iDamageTaker != null && !targets.Contains(iDamageTaker))
                    targets.Add(iDamageTaker);
            }
        }
        return targets.ToArray();
    }

    private void LookMouse2D()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = 10f;
        Vector3 lookPos;
        
        if(playerAniamation.isOwned){
            lookPos = Camera.main.ScreenToWorldPoint(mousePos)-center.position;
            playerAniamation.SetMousePos(lookPos);
        }
        else{
            lookPos = playerAniamation.GetMousePos();
        }
        float angle = GetAngle(Vector2.zero, lookPos);

        attackAngle = angle + 180f;

        if(Mathf.Abs(angle) < 90f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            angle *= -1f;
        }
        else
        {
            angle += 180f;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if(angle>=270f)
        {
            angle -= 360f;
        }
        angle = Mathf.Clamp(angle, -maxAnimationAngle, -minAnimationAngle);

        center.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
    public static float GetAnglesFromDir(Vector3 position, Vector3 dir)
    {
        var forwardLimitPos = position + dir;
        var srcAngles = Mathf.Rad2Deg * Mathf.Atan2(forwardLimitPos.z - position.z, forwardLimitPos.x - position.x);

        return srcAngles;
    }
    public static float GetAngle (Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;
 
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
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
        foreach (var target in targets)
        {

            // get target by id
            // take damage
            // if dead, remove from targets

            _playerWeapon?.Attack();
            Debug.Log("DoAttack: " + target + " " + damage);
        }
    }

}