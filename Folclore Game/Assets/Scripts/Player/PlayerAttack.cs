using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;

    [SerializeField] private Transform shootPos;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float fireRate;
    private float nextFire;
    private Animator animator;
    private bool canAttack = true;
    
    private void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    public void SetCanAttack(bool canAttack)
    {
        if (canAttack)
        {
            this.canAttack = true;
        }
        else
        {
            this.canAttack = false;
        }
    }

    public void Attack()
    {
        if (PlayerInputsControl.Instance.GetIsAttacking() && Time.time > nextFire && canAttack)
        {
            nextFire = Time.time + fireRate;
            Instantiate(projectile, shootPos.position, shootPos.rotation);
            animator.SetTrigger("isAttacking");
        }
    }
}