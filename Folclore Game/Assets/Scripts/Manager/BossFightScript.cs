using System.Collections;
using UnityEngine;



public class BossFightScript : MonoBehaviour
{
    public State currentState;

    private Animator animator;

    [Header("Life Variables")]
    [SerializeField] private int maxLife;
    [SerializeField] private int currentLife;

    [Header("Fire Ball Variables")]
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private float timeBTWFireBalls;
    [SerializeField] private int[] fireBallsQuantities;
    [SerializeField] private int minPosX, maxPosX;
    private Vector2 fireBallSpawnPos;

    [Header("Spike Variables")]
    [SerializeField] private GameObject spikesPrefab;
    [SerializeField] private float timeBTWSpikes;

    [Header("Dragon Variables")]
    [SerializeField] private GameObject dragonPrefab;
    [SerializeField] private float timeBTWDragons;

    private void Awake()
    {
        currentState = State.Idle;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ControllPhases();

        switch (currentState)
        {
            case State.Idle:
                SetIdle();
                break;
            case State.Phase1:
                StartCoroutine(Phase1());
                break;
            case State.Phase2:
                StartCoroutine(Phase2());
                break;
            case State.Dead:
                StartCoroutine(Die());
                break;
        }
    }

    void ControllPhases()
    {
        if (currentLife > 5) currentState = State.Phase1;
        else if (currentLife <= 5 && currentLife > 0) currentState = State.Phase2;
        else currentState = State.Dead;
    }


    void SetIdle()
    {
        animator.SetTrigger("isIdle");
    }

    void FireBallFalling(int fireBallsQuantity) //USAR ARRAY fireBallsQuantities
    {
        for (int i = 0; i < fireBallsQuantity; i++)
        {
            fireBallSpawnPos = new Vector2(Random.Range(minPosX, maxPosX), 10);

            Instantiate(fireBallPrefab, fireBallSpawnPos, Quaternion.identity);
        }
    }



    void SpawnSpikes()
    {

    }

    void SpawnDragons()
    {

    }

    IEnumerator Phase1()
    {
        yield return null;
    }

    IEnumerator Phase2()
    {
        yield return null;
    }

    IEnumerator Die()
    {
        yield return null;
    }
}

public enum State
{
    Idle,
    Phase1,
    Phase2,
    Dead
}