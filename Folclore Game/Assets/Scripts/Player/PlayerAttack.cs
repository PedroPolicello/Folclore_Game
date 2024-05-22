using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;

    [SerializeField] private Transform shootPos;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float fireRate;
    private float nextFire;
    private void Awake()
    {
        instance = this;
    }

    public void Attack()
    {
        if (PlayerInputsControl.instance.GetIsAttacking() && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(projectile, shootPos.position, shootPos.rotation);
        }
    }
}