using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    public float delay;
    void Start()
    {
        Destroy(gameObject, delay);
    }
}
