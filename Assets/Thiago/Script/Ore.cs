using UnityEngine;

public class Ore : MonoBehaviour
{
    [Header("Item do inventário")]
    public ItemData itemData;

    [Header("Vida")]
    public int health = 3;

    private int currentHealth;

    [HideInInspector]
    public OreSpawnPoint spawnPoint;

    void Awake()
    {
        Renderer[] rends = GetComponentsInChildren<Renderer>(true);

        foreach (Renderer r in rends)
        {
            r.enabled = true;
        }

        Collider[] cols = GetComponentsInChildren<Collider>(true);

        foreach (Collider c in cols)
        {
            c.enabled = true;
        }
    }

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