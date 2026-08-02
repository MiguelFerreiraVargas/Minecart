using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;

    [Header("UI")]
    public Transform itemsParent;
    public GameObject itemPrefab;

    [Header("Dialogue")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    private bool dialogueShown = false;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        if (!dialogueShown)
        {
            dialogueShown = true;

            dialoguePanel.SetActive(true);

            dialogueText.text =
                "Olá, jovem minerador! Precisamos de carvão e minério para expandir nossas ferrovias e construir locomotivas cada vez melhores.\n\n" +
                "Traga os recursos que encontrar na mina, e eu os comprarei por um bom preço. Com esse material, poderemos ligar cidades e transportar pessoas mais rápido do que nunca!\n\n" +
                "Agora, mostre-me o que você coletou.";

            return;
        }

        UpdateShop();
    }

    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
        UpdateShop();
    }

    public void UpdateShop()
    {
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (InventorySlot slot in InventoryManager.Instance.inventory)
        {
            GameObject obj = Instantiate(itemPrefab, itemsParent);

            Image icon = obj.transform.Find("Icon").GetComponent<Image>();
            TMP_Text nameText = obj.transform.Find("NameText").GetComponent<TMP_Text>();
            TMP_Text amountText = obj.transform.Find("AmountText").GetComponent<TMP_Text>();
            TMP_Text priceText = obj.transform.Find("PriceText").GetComponent<TMP_Text>();

            icon.sprite = slot.item.icon;
            nameText.text = slot.item.itemName;
            amountText.text = "x" + slot.amount;

            int totalPrice = slot.item.value * slot.amount;
            priceText.text = "$" + totalPrice;
        }
    }

    public void SellAll()
    {
        int totalMoney = 0;

        foreach (InventorySlot slot in InventoryManager.Instance.inventory)
        {
            totalMoney += slot.item.value * slot.amount;
        }

        MoneyManager.Instance.AddMoney(totalMoney);

        InventoryManager.Instance.inventory.Clear();

        InventoryUI.Instance.UpdateUI();

        UpdateShop();
    }
}