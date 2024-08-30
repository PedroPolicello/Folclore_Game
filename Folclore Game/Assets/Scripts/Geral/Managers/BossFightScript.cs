using System.Collections;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class BossFightScript : MonoBehaviour
{
    public static BossFightScript Instance;
    public State currentState;
    private Animator animator;

    [Header("---- General Variables ----")] 
    public bool isOnIdle;
    public bool isOnPhase1;
    public bool isOnPhase2;
    public bool isDead;

    [Header("---- Life Variables ----")] 
    public int maxLife;
    public int currentLife;

    [Header("---- Fire Ball Variables ----")] 
    [SerializeField] private float timeBTWFireBalls;
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private Vector2 minMaxPosX;
    [SerializeField] private int[] fireBallsQuantities;
    [SerializeField] private GameObject[] fireBallsBlockers;
    private int fireBallBlockerDisable;

    [Header("---- Spike Variables ----")] 
    [SerializeField] private float timeBTWSpikes;
    [SerializeField] private GameObject[] spikes;
    [SerializeField] private GameObject[] warnings;
    [SerializeField] private int[] spikesQuantities;

    [Header("---- Dragon Variables ----")] 
    [SerializeField] private GameObject dragonPrefab;
    [SerializeField] private float timeBTWDragons;
    [SerializeField] private GameObject[] dragonSpawnPos;
    [SerializeField] private int[] dragonQuantities;

    private void Awake()
    {
        Instance = this;
        currentLife = maxLife;
        UIManager.Instance.SetBossHealthBar();
        ControllPhases();
    }
    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                if (!isOnIdle)
                {
                    isOnIdle = true;
                }
                break;
            case State.Phase1:
                if (!isOnPhase1)
                {
                    StopAllCoroutines();
                    StartCoroutine(Phase1());
                    print("Phase01");
                    isOnPhase1 = true;
                }
                break;
            case State.Phase2:
                if (!isOnPhase2)
                {
                    StopAllCoroutines();
                    StartCoroutine(Phase2());
                    print("Phase02");
                    isOnPhase2 = true;
                }
                break;
            case State.Dead:
                if (!isDead)
                {
                    StopAllCoroutines();
                    StartCoroutine(Die());
                    isDead = true;
                }
                break;
        }
    }
    void ControllPhases()
    {
        if (currentLife > maxLife / 2)currentState = State.Phase1; 
        else if (currentLife <= maxLife / 2 && currentLife > 0)currentState = State.Phase2;
        else currentState = State.Dead;
    }

    //ATTACKS
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
            if(currentState == State.Phase1)yield return new WaitForSeconds(timeBTWFireBalls);
            else if (currentState == State.Phase2)yield return new WaitForSeconds(timeBTWFireBalls - 1);
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
    IEnumerator SpawnSpikes(bool begin, int spikesQuantity)
    {
        if (begin)
        {
            for (int i = 0; i < spikesQuantity; i++)
            {
                int spikePosIndex = Random.Range(0, spikes.Length);
                spikes[spikePosIndex].SetActive(true);
                warnings[spikePosIndex].SetActive(true);
                yield return new WaitForSeconds(1f);
                spikes[spikePosIndex].transform.DOLocalMoveY(-4,1);
            }
        }
        else
        {
            foreach (GameObject spike in spikes)
            {
                spike.transform.DOLocalMoveY(-8,1);
                yield return new WaitForSeconds(1f);
                spike.SetActive(false);
            } 
        }
    }

    //PHASES & DIE
    IEnumerator Phase1()
    {
        while (currentLife > maxLife/2)
        {
            StartCoroutine(FireBallFalling(fireBallsQuantities[0]));
            yield return new WaitForSeconds(10);
            StartCoroutine(SpawnDragons(dragonQuantities[0]));
            //Esperar os dragões morrerem
            yield return new WaitForSeconds(5);
            StartCoroutine(SpawnSpikes(true, spikesQuantities[0]));
            yield return new WaitForSeconds(5);
            StartCoroutine(SpawnSpikes(false, spikesQuantities[0]));
            yield return new WaitForSeconds(4f);
        }
    }
    IEnumerator Phase2()
    {
        print("Dialogo para Phase 2");
        yield return new WaitForSeconds(10f);
        while (currentLife <= maxLife/2 && currentLife > 0)
        {
            StartCoroutine(FireBallFalling(fireBallsQuantities[1]));
            yield return new WaitForSeconds(15);
            StartCoroutine(SpawnDragons(dragonQuantities[1]));
            //Esperar os dragões morrerem
            yield return new WaitForSeconds(10);
            StartCoroutine(SpawnSpikes(true, spikesQuantities[1]));
            yield return new WaitForSeconds(6);
            StartCoroutine(SpawnSpikes(false, spikesQuantities[1]));
            yield return new WaitForSeconds(5f);
        }
    }
    
    IEnumerator Die()
    {
        //Destruir todos os Dragões
        print("Dialogo pré-morte");
        yield return new WaitForSeconds(5f);
        SceneController.Instance.CallWinScreen();
    }

    //DAMAGE & LIFE
    public void TakeDamage(int dmg)
    {
        currentLife -= dmg;
        UIManager.Instance.UpdateBossUI();
        ControllPhases();
    }
}

public enum State
{
    Idle,
    Phase1,
    Phase2,
    Dead
}