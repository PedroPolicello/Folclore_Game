using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class BossFightScript : MonoBehaviour
{
    public State currentState;

    private Animator animator;

    [Header("----General Variables----")] [SerializeField]
    private float minDelay;

    [SerializeField] private float maxDelay;
    public bool isOnIdle;
    public bool isOnPhase1;
    public bool isOnPhase2;
    public bool isDead;

    [Header("---- Life Variables ----")] [SerializeField]
    private int maxLife;

    [SerializeField] private int currentLife;

    [Header("---- Fire Ball Variables ----")] [SerializeField]
    private GameObject fireBallPrefab;

    [SerializeField] private float timeBTWFireBalls;
    [SerializeField] private Vector2 minMaxPosX;
    [SerializeField] private int[] fireBallsQuantities;
    [SerializeField] private GameObject[] fireBallsBlockers;
    private int fireBallBlockerDisable;

    [Header("---- Spike Variables ----")] [SerializeField]
    private GameObject spikesPrefab;

    [SerializeField] private float timeBTWSpikes;

    [Header("---- Dragon Variables ----")] [SerializeField]
    private GameObject dragonPrefab;

    [SerializeField] private float timeBTWDragons;
    [SerializeField] private GameObject[] dragonSpawnPos;
    [SerializeField] private int[] dragonQuantities;

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

    //ATTACKS & IDLE
    void SetIdle()
    {
        animator.SetTrigger("isIdle");
    }

    IEnumerator FireBallFalling(int fireBallQuantity)
    {
        while (fireBallQuantity > 0)
        {
            //Ativa um dos Bloqueadores
            fireBallBlockerDisable = Random.Range(0, fireBallsBlockers.Length);
            fireBallsBlockers[fireBallBlockerDisable].SetActive(true);
            
            //Spawna as bolas de fogo
            for (float i = minMaxPosX.x; i < minMaxPosX.y + 1; i += 1.5f)
            {
                Instantiate(fireBallPrefab, new Vector2(i, -81), Quaternion.identity);
            }

            fireBallQuantity--;
            yield return new WaitForSeconds(timeBTWFireBalls);
            foreach (var t in fireBallsBlockers) t.SetActive(false);
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
        while (currentLife > 5)
        {
            yield return new WaitForSeconds(maxDelay);
            StartCoroutine(FireBallFalling(fireBallsQuantities[0]));
            yield return new WaitForSeconds(timeBTWFireBalls * fireBallsQuantities[0]);
            StartCoroutine(SpawnDragons(dragonQuantities[0]));
            yield return new WaitForSeconds(dragonQuantities[0] * 3);
            //yield return new WaitUntil(() => dragonCount <= 0);
        }
        SetIdle();
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