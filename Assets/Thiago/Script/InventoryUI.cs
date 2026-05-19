using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    public GameObject slotPrefab;
    public Transform slotParent;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateUI()
    {
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        foreach (InventorySlot slot in InventoryManager.Instance.inventory)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotParent);

            Image icon = slotObj.transform.Find("Icon").GetComponent<Image>();
            TMP_Text amount = slotObj.transform.Find("Amount").GetComponent<TMP_Text>();

            icon.sprite = slot.item.icon;
            amount.text = slot.amount.ToString();
        }
    }
}