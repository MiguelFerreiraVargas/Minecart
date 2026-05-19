using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<InventorySlot> inventory = new List<InventorySlot>();

    public int maxSlots = 20;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(ItemData item)
    {
        foreach (InventorySlot slot in inventory)
        {
            if (slot.item == item && item.stackable)
            {
                slot.amount++;
                InventoryUI.Instance.UpdateUI();
                return;
            }
        }

        if (inventory.Count < maxSlots)
        {
            inventory.Add(new InventorySlot(item, 1));
            InventoryUI.Instance.UpdateUI();
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemData item;
    public int amount;

    public InventorySlot(ItemData item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}