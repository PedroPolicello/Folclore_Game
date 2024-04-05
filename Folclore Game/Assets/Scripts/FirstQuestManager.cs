using UnityEngine;

public class FirstQuestManager : MonoBehaviour
{
    private bool ingredient01;
    private bool ingredient02;
    private bool ingredient03;
    private bool hasAllIngredients;

    void Update()
    {
        if (ingredient01 && ingredient02 && ingredient03)
        {
            hasAllIngredients = true;
        }
    }

    void DeliverQuest()
    {
        if (hasAllIngredients)
        {
            //Instancia Carta Nº1
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ingredient01"))
        {
            ingredient01 = true;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Ingredient02"))
        {
            ingredient02 = true;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Ingredient03"))
        {
            ingredient03 = true;
            Destroy(collision.gameObject);
        }
    }
}
