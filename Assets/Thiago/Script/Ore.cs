using UnityEngine;

public class Ore : MonoBehaviour
{
    [Header("Item")]
    public ItemData itemData;

    [Header("Vida")]
    public int health = 3;

    public void HitOre()
    {
        health--;

        if (health <= 0)
        {
            BreakOre();
        }
    }

    void BreakOre()
    {
        InventoryManager.Instance.AddItem(itemData);

        Destroy(gameObject);
    }
}