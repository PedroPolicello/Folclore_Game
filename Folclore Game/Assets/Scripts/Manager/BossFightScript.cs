using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class BossFightScript : MonoBehaviour
{
    public State currentState;

    private Animator animator;

    [Header("General Variables")] 
    [SerializeField] private float minDelay;
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
    [SerializeField] private GameObject[] dragonSpawnPos;
    [SerializeField] private int[] dragonQuantities;
    private int dragonCount;

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

        dragonCount = GameObject.FindGameObjectsWithTag("dragon").Length;
    }

    void ControllPhases()
    {
        if (currentLife > 5) currentState = State.Phase1;
        else if (currentLife is <= 5 and > 0) currentState = State.Phase2;
        else currentState = State.Dead;
    }
    
    //ATTACKS & IDLE
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

    IEnumerator SpawnDragons(int dragonsQuantity)
    {
        for (int i = 0; i < dragonsQuantity; i++)
        {
            int spawnPosIndex = Random.Range(0, 2);

            Instantiate(dragonPrefab, dragonSpawnPos[spawnPosIndex].transform.position, quaternion.identity);
            yield return new WaitForSeconds(timeBTWDragons);
        }
    }
    void SpawnSpikes()
    {
        
    }
    
    //PHASES & DIE
    IEnumerator Phase1()
    {
        while (currentLife > 5 )
        {
            StartCoroutine(FireBallFalling(fireBallsQuantities[0]));
            yield return new WaitForSeconds(fireBallsQuantities[0]/2 + maxDelay);
            StartCoroutine(SpawnDragons(dragonQuantities[0]));
            yield return new WaitForSeconds(10);
            //yield return new WaitUntil(() => dragonCount <= 0);
        }
    }

    IEnumerator Phase2()
    {
        yield return null;
    }

    IEnumerator Die()
    {
        yield return null;
    }

    //DAMAGE & LIFE
    public void TakeDamage(int dmg)
    {
        currentLife -= dmg;
    }
}

public enum State
{
    Idle,
    Phase1,
    Phase2,
    Dead
}