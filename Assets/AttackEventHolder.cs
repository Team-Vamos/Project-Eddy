using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEventHolder : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private void Awake() {
        playerAttack = transform.root.GetComponent<PlayerAttack>();
    }
    protected void DoAttack()
    {
        playerAttack.DoAttack();
    }

}
