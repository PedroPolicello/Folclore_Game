using System.Collections;
using Cinemachine;
using UnityEngine;

public class BossDialog : MonoBehaviour
{
    public static BossDialog Instance;

    [SerializeField] private GameObject bossFightManager;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Camera camera;
    private void Awake()
    {
        Instance = this;
    }

    public void StartBossDialog()
    {
        virtualCamera.Follow = null;
        virtualCamera.transform.position = new Vector3(-90, -90.3f, -10);
        StartCoroutine(CucaDialog());
    }

    IEnumerator CucaDialog()
    {
        yield return new WaitForSeconds(1f);
        print("Dialogo Boss");
        yield return new WaitForSeconds(2f);
        PlayerHealth.Instance.currentHealth = PlayerHealth.Instance.maxHealth;
        UIManager.Instance.UpdateUI();
        yield return new WaitForSeconds(0.5f);
        bossFightManager.SetActive(true);
    }
}
