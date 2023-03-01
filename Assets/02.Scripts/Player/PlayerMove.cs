using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManagers;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector3 inputDir;
    private Vector3 realMove = Vector3.zero;
    [SerializeField]
    private bool canMove = false;
    private PlayerObjectControler playerObjectControler;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        playerObjectControler = GetComponent<PlayerObjectControler>();
        Debug.Log("Start");
        EventManager.StartListening(NetManager.StartNetworkCallback, SetNetwork);
    }
    private void SetNetwork()
    {
        Debug.Log("SetNetwork");
        if (playerObjectControler.isLocal)
        {
            canMove = true;
        }
    }
    private void Update()
    {
        if (canMove) Move();
    }
    private void FixedUpdate() {
        if (canMove)
            _rb.velocity = realMove;
    }
    private void Move()
    {
        inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        realMove = /* stat * */inputDir.normalized;
    }
}