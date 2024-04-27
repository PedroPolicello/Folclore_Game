using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;

    [SerializeField] private Transform shootPos;
    [SerializeField] private GameObject projectile;
    private void Awake()
    {
        instance = this;
    }

    public void Attack()
    {
        if (PlayerInputsControl.instance.GetIsAttacking())
        {
            Instantiate(projectile, shootPos);
            print("Você Atacou!");
        }
    }
}