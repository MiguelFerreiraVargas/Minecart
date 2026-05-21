using UnityEngine;

public class Ore : MonoBehaviour
{
    public ItemData itemData;

    public int health = 3;

    private int currentHealth;

    [HideInInspector]
    public OreSpawnPoint spawnPoint;

    void Start()
    {
        currentHealth = health;
    }

    public void HitOre()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            BreakOre();
        }
    }

    void BreakOre()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.AddItem(itemData);
        }

        if (spawnPoint != null)
        {
            spawnPoint.RespawnOre();
        }

        Destroy(gameObject);
    }
}