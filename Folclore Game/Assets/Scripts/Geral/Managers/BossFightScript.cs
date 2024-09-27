using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BossFightScript : MonoBehaviour
{
    public static BossFightScript Instance;
    public State currentState;
    private Animator animator;
    private AudioSource audioSource;
    
    [Header("---- Dialogue Variables ----")] 
    public float duration;
    [TextArea(3,10)] public string[] texts;
    private GameObject textBox;

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
    int spikePosIndex;

    [Header("---- Dragon Variables ----")] 
    [SerializeField] private GameObject dragonPrefab;
    [SerializeField] private float timeBTWDragons;
    [SerializeField] private GameObject[] dragonSpawnPos;
    [SerializeField] private int[] dragonQuantities;
    [SerializeField] private GameObject[] dragonsInGame;

    private void Awake()
    {
        Instance = this;
        currentLife = maxLife;
        UIManager.Instance.SetBossHealthBar();
        SetReferences();
        ControllPhases();
        SoundManager.Instance.PlayPauseMusic(false);
        SoundManager.Instance.SetMusic(SoundManager.Instance.bossMusic);
        SoundManager.Instance.PlayPauseMusic(true);
        audioSource = GetComponent<AudioSource>();
    }
    void SetReferences()
    {
        textBox = GameObject.FindGameObjectWithTag("backgroundDialogue");
        textBox.GetComponent<Image>().enabled = false;
    }
    private void Update()
    {
        dragonsInGame = GameObject.FindGameObjectsWithTag("dragon");
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
                    isOnPhase1 = true;
                }
                break;
            case State.Phase2:
                if (!isOnPhase2)
                {
                    StopAllCoroutines();
                    StartCoroutine(Phase2());
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
            
            audioSource.volume = SoundManager.Instance.sFXVolume.value/50;
            audioSource.PlayOneShot(SoundManager.Instance.fireBall);

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
                ChooseSpike();
                warnings[spikePosIndex].SetActive(true);
                spikes[spikePosIndex].SetActive(true);
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

    void ChooseSpike()
    {
        spikePosIndex = Random.Range(0, spikes.Length);
        if (spikes[spikePosIndex].activeSelf == false) return;
        else ChooseSpike();
    }

    //PHASES & DIE
    IEnumerator Phase1()
    {
        while (currentLife > maxLife/2)
        {
            StartCoroutine(FireBallFalling(fireBallsQuantities[0]));
            yield return new WaitForSeconds(10);
            StartCoroutine(SpawnDragons(dragonQuantities[0]));
            yield return new WaitForSeconds(5);
            yield return new WaitUntil(DragonsInGame); //Esperar os dragões morrerem
            yield return new WaitForSeconds(1);
            StartCoroutine(SpawnSpikes(true, spikesQuantities[0]));
            yield return new WaitForSeconds(5);
            StartCoroutine(SpawnSpikes(false, spikesQuantities[0]));
            yield return new WaitForSeconds(4f);
        }
    }
    IEnumerator Phase2()
    {
        SetDialogue(true, 0);
        yield return new WaitForSeconds(duration);
        SetDialogue(false, 0);
        
        while (currentLife <= maxLife/2 && currentLife > 0)
        {
            StartCoroutine(FireBallFalling(fireBallsQuantities[1]));
            yield return new WaitForSeconds(15);
            StartCoroutine(SpawnDragons(dragonQuantities[1]));
            yield return new WaitForSeconds(5);
            yield return new WaitUntil(DragonsInGame); //Esperar os dragões morrerem
            yield return new WaitForSeconds(1);
            StartCoroutine(SpawnSpikes(true, spikesQuantities[1]));
            yield return new WaitForSeconds(6);
            StartCoroutine(SpawnSpikes(false, spikesQuantities[1]));
            yield return new WaitForSeconds(5f);
        }
    }

    bool DragonsInGame()
    {
        return dragonsInGame.Length == 0;
    }
    
    IEnumerator Die()
    {
        KillAllDragons();
        audioSource.volume = SoundManager.Instance.sFXVolume.value/50;
        audioSource.PlayOneShot(SoundManager.Instance.bossDie);

        SetDialogue(true, 1);
        yield return new WaitForSeconds(duration);
        SetDialogue(false, 1);
        
        SceneController.Instance.CallWinScreen();
    }

    public void KillAllDragons()
    {
        foreach (GameObject dragon in dragonsInGame) Destroy(dragon);
    }

    //DAMAGE & LIFE
    public void TakeDamage(int dmg)
    {
        audioSource.volume = SoundManager.Instance.sFXVolume.value/50;
        audioSource.PlayOneShot(SoundManager.Instance.bossHit);
        
        currentLife -= dmg;
        UIManager.Instance.UpdateBossUI();
        ControllPhases();
    }
    
    void SetDialogue(bool active, int textIndex)
    {
        if (active)
        {
            textBox.GetComponent<Image>().enabled = true;
            textBox.GetComponentInChildren<TextMeshProUGUI>().text = texts[textIndex];
        }
        else
        {
            textBox.GetComponentInChildren<TextMeshProUGUI>().text = "";
            textBox.GetComponent<Image>().enabled = false;
        }
    }
}

public enum State
{
    Idle,
    Phase1,
    Phase2,
    Dead
}