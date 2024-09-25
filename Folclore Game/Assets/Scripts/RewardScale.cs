using System.Collections;
using DG.Tweening;
using UnityEngine;

public class RewardScale : MonoBehaviour
{
    [SerializeField] private float duration;

    void OnEnable()
    {
        StartCoroutine(Scale());
    }

    IEnumerator Scale()
    {
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), duration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(duration);
        transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), duration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(duration);
        StartCoroutine(Scale());
    }
}
