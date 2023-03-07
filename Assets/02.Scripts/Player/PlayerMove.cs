using UnityEngine;
using EventManagers;


public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector3 inputDir;
    private Vector3 realMove = Vector3.zero;
    private PlayerObjectControler playerObjectControler;
    private PlayerAniamation playerAniamation;
    private bool canMove = false;
    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
    private void GameStart()
    {
        canMove = true;
    }
    private void Awake()
    {
        playerAniamation = GetComponent<PlayerAniamation>();
        _rb = GetComponent<Rigidbody2D>();
        playerObjectControler = GetComponent<PlayerObjectControler>();

        EventManager.StopListening(NetManager.StartGameCallback, GameStart);
        EventManager.StartListening(NetManager.StartGameCallback, GameStart);
    }
    private void OnDestroy()
    {
        EventManager.StopListening(NetManager.StartGameCallback, GameStart);
    }
    private void Update()
    {
        if (playerObjectControler.isLocal && canMove) Move();
    }
    private void FixedUpdate() {
        if (playerObjectControler.isLocal)
            _rb.velocity = realMove;
    }
    private void Move()
    {
        inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        realMove = /* stat * */inputDir;
        float aniDir = realMove.x;
        if(aniDir == 0)
            aniDir = realMove.y;
        playerAniamation.SetDir(aniDir);
    }
}