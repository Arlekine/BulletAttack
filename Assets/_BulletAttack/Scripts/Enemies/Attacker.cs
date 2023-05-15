using UnityEngine;

public abstract class Attacker : MonoBehaviour
{
    public abstract void SetAttackTarget(Health target);
}