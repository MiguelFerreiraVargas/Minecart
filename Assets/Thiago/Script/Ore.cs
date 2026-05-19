using UnityEngine;

public class Ore : MonoBehaviour
{
    public ItemData itemData;

    public int health = 3;

    private int currentHealth;

    public OreSpawnPoint spawnPoint;

    void OnEnable()
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
        InventoryManager.Instance.AddItem(itemData);

        gameObject.SetActive(false);

        if (spawnPoint != null)
        {
            spawnPoint.OreDestroyed();
        }
    }
}