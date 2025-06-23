using UnityEngine;
using TMPro;

public class Resources : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI reputationText;

    [Header("Values")]
    private int money = 0;
    private int reputation = 0; 

    private void Start()
    {
        UpdateUI();
    }

    public int GetMoney() => money;
    public int GetReputation() => reputation;
    public void ChangeMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    public void ChangeReputation(int amount)
    {
        reputation += amount;
        reputation = Mathf.Clamp(reputation, -100, 100);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = $"{money}";

        if (reputationText != null)
            reputationText.text = $"{reputation}";
    }
}