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
    [SerializeField] private AudioSource audioSource;

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
            animator.SetTrigger("isAttacking");
            Instantiate(projectile, shootPos.position, shootPos.rotation);

            audioSource.volume = SoundManager.Instance.sFXVolume.value/100;
            audioSource.PlayOneShot(SoundManager.Instance.attack);
        }
    }
}