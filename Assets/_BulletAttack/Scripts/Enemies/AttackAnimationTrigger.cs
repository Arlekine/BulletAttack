using System;
using UnityEngine;

public class AttackAnimationTrigger : MonoBehaviour
{
    public Action Attack;

    public void TriggerAttack()
    {
        Attack?.Invoke();
    }
}