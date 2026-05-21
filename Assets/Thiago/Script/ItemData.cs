using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    [Header("Informações")]
    public string itemName;

    [TextArea]
    public string description;

    [Header("Visual")]
    public Sprite icon;

    public Color itemColor = Color.white;

    [Header("Stack")]
    public bool stackable = true;

    public int maxStack = 99;

    [Header("Economia")]
    public int value;

    [Header("Raridade")]
    public ItemRarity rarity;
}

public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}