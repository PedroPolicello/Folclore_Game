using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class BossFightScript : MonoBehaviour
{
    public State currentState;

    private Animator animator;

    [Header("General Variables")] [SerializeField]
    private float minDelay;

    [SerializeField] private float maxDelay;
    public bool isOnIdle;
    public bool isOnPhase1;
    public bool isOnPhase2;
    public bool isDead;

    [Header("Life Variables")] 
    [SerializeField] private int maxLife;
    [SerializeField] private int currentLife;

    [Header("Fire Ball Variables")] 
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private float timeBTWFireBalls;
    [SerializeField] private Vector2 minMaxPosX;
    [SerializeField] private int[] fireBallsQuantities;
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
                if (!isOnIdle) SetIdle();
                isOnIdle = true;
                break;
            case State.Phase1:
                if (!isOnPhase1) StartCoroutine(Phase1());
                isOnPhase1 = true;
                break;
            case State.Phase2:
                if (!isOnPhase2) StartCoroutine(Phase2());
                isOnPhase2 = true;
                break;
            case State.Dead:
                if (!isDead) StartCoroutine(Die());
                isDead = true;
                break;
        }
    }

    void ControllPhases()
    {
        if (currentLife > 5) currentState = State.Phase1;
        else if (currentLife is <= 5 and > 0) currentState = State.Phase2;
        else currentState = State.Dead;
    }


    void SetIdle()
    {
        animator.SetTrigger("isIdle");
    }

    IEnumerator FireBallFalling(int fireBallsQuantity)
    {
        for (int i = 0; i < fireBallsQuantity; i++)
        {
            fireBallSpawnPos = new Vector2(Random.Range(minMaxPosX.x, minMaxPosX.y), 7);
            
            Instantiate(fireBallPrefab, fireBallSpawnPos, Quaternion.identity);
            yield return new WaitForSeconds(timeBTWFireBalls);
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
        StartCoroutine(FireBallFalling(fireBallsQuantities[0]));
        yield return new WaitForSeconds(fireBallsQuantities[0] + 1);
        StartCoroutine(FireBallFalling(fireBallsQuantities[1]));
        yield return new WaitForSeconds(fireBallsQuantities[1] + 1);
        print("Spawn Dragons");
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