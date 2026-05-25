using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    public int money;

    public TMP_Text moneyText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateMoneyUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;

        UpdateMoneyUI();
    }

    public bool SpendMoney(int amount)
    {
        if (money < amount)
            return false;

        money -= amount;

        UpdateMoneyUI();

        return true;
    }

    void UpdateMoneyUI()
    {
        moneyText.text = "$ " + money;
    }
}