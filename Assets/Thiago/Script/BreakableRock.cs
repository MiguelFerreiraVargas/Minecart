using UnityEngine;

public class BreakableRock : MonoBehaviour
{
    [Header("Vida da Pedra")]
    public int maxHealth = 3;

    [Header("Efeito")]
    public GameObject breakEffect;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnMouseDown()
    {
        TakeDamage(1);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Pedra levou dano! Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            BreakRock();
        }
    }

    void BreakRock()
    {
        // cria efeito
        if (breakEffect != null)
        {
            Instantiate(
                breakEffect,
                transform.position,
                Quaternion.identity
            );
        }

        // destrói a pedra
        Destroy(gameObject);
    }
}