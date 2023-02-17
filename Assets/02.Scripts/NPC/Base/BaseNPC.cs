using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseNPC : MonoBehaviour
{
    [SerializeField]
    private NPCSO _so;

    [SerializeField]
    private LayerMask _hitLayer = 1 << 3;

    private Rigidbody2D _rb;

    private NavMeshAgent _agent;
    private Vector3 _destination;

    private Transform _sprite;

    private const string speed = "Spd";
    private const string runSpeed = "RunSpd";
    protected virtual void Awake()
    {
        _so.statController.Init();

        _agent = GetComponent<NavMeshAgent>();


        _sprite = transform.Find("Sprite");
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.updatePosition = false;
        _agent.speed = _so.statController.GetStat(speed).value;
    }

    public void Move(Vector3 pos)
    {
        _agent.Move(pos - transform.position);
    }

    public void SetDestination(Vector3 destination)
    {
        _destination = destination;
        _agent.SetDestination(_destination);
    }

    private void Update()
    {
        if (_agent.remainingDistance > _agent.stoppingDistance)
        {
            _agent.Move(transform.position - _agent.nextPosition);
            if (!_rb.bodyType.HasFlag(RigidbodyType2D.Static))
                _rb.velocity = _agent.velocity;
        }
        else
        {
            if (!_rb.bodyType.HasFlag(RigidbodyType2D.Static))
                _rb.velocity = Vector2.zero;
        }

        if (_rb.velocity.x >= 0)
        {
            // 오른쪽
            Vector3 scale = _sprite.transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            _sprite.transform.localScale = scale;
        }
        else
        {
            // 왼쪽
            Vector3 scale = _sprite.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * -1f;
            _sprite.transform.localScale = scale;
        }

    }


    protected virtual void OnDaddyHit(Collision2D other)
    {
        if (((1 << other.gameObject.layer) & _hitLayer) > 0)
        {
            Hit();
        }
    }

    protected void StopNPC()
    {
        _agent.isStopped = true;
        _rb.bodyType = RigidbodyType2D.Static;
    }

    protected void EndActionNPC()
    {
        _agent.isStopped = false;
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _agent.speed = _so.statController.GetStat(runSpeed).value;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnDaddyHit(other);
    }
    public abstract void Hit();

}
