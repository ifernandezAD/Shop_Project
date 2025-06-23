using UnityEngine;
using TMPro;

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

        clientText.text = $" Client type: <b>{type}</b>";
        feedbackText.text = "";
    }

    public void Sell(string price)
    {
        if (currentClient == null) return;

        int rep = resources.GetReputation();
        int payout = currentClient.CalculatePayout(price.ToLower(), rep);

        if (payout > 0)
        {
            resources.ChangeMoney(payout);

            if (price == "fair") resources.ChangeReputation(+2);
            else resources.ChangeReputation(+1);

            feedbackText.text = $" Sold for {payout}!";
        }
        else
        {
            resources.ChangeReputation(-5);
            feedbackText.text = $" Sale failed!";
        }

        calendar.AdvanceDay();
        GenerateNewClient();
    }
}

