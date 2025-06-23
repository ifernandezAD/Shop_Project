using UnityEngine;
using TMPro;
using System.Collections;

public class ClientManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI clientText;
    public TextMeshProUGUI feedbackText;

    [Header("Dependencies")]
    public Resources resources;
    public GameCalendar calendar;

    private Client currentClient;

    private void Start()
    {
        GenerateNewClient();
    }

    public void GenerateNewClient()
    {
        ClientType type = (ClientType)Random.Range(0, 3);
        currentClient = new Client(type);

        clientText.text = $"Client type: <b>{type}</b>";


        Debug.Log($"[NEW CLIENT GENERATED] Type: {currentClient.Type}");
    }

    public void SellLow() => Sell("low");
    public void SellFair() => Sell("fair");
    public void SellHigh() => Sell("high");

    private void Sell(string price)
    {
        Debug.Log($"[SELL] Called with price: {price}, Client type: {currentClient?.Type}");

        if (currentClient == null) return;

        int rep = resources.GetReputation();
        int payout = currentClient.CalculatePayout(price, rep);

        if (payout > 0)
        {
            resources.ChangeMoney(payout);

            if (price.ToLower() == "fair") resources.ChangeReputation(+2);
            else resources.ChangeReputation(+1);

            feedbackText.text = $"Sold for {payout}!";
        }
        else
        {
            resources.ChangeReputation(-5);
            feedbackText.text = "Sale failed!";
        }

        StartCoroutine(ClearFeedbackAfterDelay(2f)); 

        calendar.AdvanceDay();
        GenerateNewClient();
    }

    private IEnumerator ClearFeedbackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        feedbackText.text = "";
    }
}
