using UnityEngine;
using EventManagers;


public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector3 inputDir;
    private Vector3 realMove = Vector3.zero;
    private PlayerObjectControler playerObjectControler;
    private PlayerAniamation playerAniamation;
    private void Awake()
    {
        playerAniamation = GetComponent<PlayerAniamation>();
        _rb = GetComponent<Rigidbody2D>();
        playerObjectControler = GetComponent<PlayerObjectControler>();

    }
    private void Update()
    {
        if (playerObjectControler.isLocal) Move();
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