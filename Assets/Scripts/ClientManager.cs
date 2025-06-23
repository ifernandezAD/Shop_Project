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
        feedbackText.text = GetClientHint(type);
    }

    public void SellLow() => StartCoroutine(Sell("low"));
    public void SellFair() => StartCoroutine(Sell("fair"));
    public void SellHigh() => StartCoroutine(Sell("high"));

    private IEnumerator Sell(string price)
    {
        if (currentClient == null) yield break;

        int rep = resources.GetReputation();
        int payout = currentClient.CalculatePayout(price, rep);

        if (payout > 0)
        {
            resources.ChangeMoney(payout);

            if (price.ToLower() == "fair")
                resources.ChangeReputation(+2);
            else
                resources.ChangeReputation(+1);

            feedbackText.text = GetSaleOutcomeMessage(currentClient.Type, price, payout, true);
        }
        else
        {
            resources.ChangeReputation(-5);
            feedbackText.text = GetSaleOutcomeMessage(currentClient.Type, price, payout, false);
        }

        yield return new WaitForSeconds(3f);

        feedbackText.text = "";

        calendar.AdvanceDay();
        GenerateNewClient();
    }

    private string GetClientHint(ClientType type)
    {
        switch (type)
        {
            case ClientType.Mean:
                return "Hint: This client dislikes high prices.";
            case ClientType.Empathic:
                return "Hint: This client appreciates fairness and may tip.";
            case ClientType.Demanding:
                return "Hint: This client is picky, exact pricing matters.";
            default:
                return "";
        }
    }

    private string GetSaleOutcomeMessage(ClientType type, string price, int payout, bool success)
    {
        if (!success)
        {
            return "The client left unhappy and without paying.";
        }

        switch (type)
        {
            case ClientType.Mean:
                if (price == "low") return $"Sold for {payout}. Client is cautiously satisfied.";
                if (price == "fair") return $"Sold for {payout}. Client is pleased.";
                if (price == "high") return "Client rejected the price.";
                break;

            case ClientType.Empathic:
                if (price == "low") return $"Sold for {payout}. Client appreciates the deal.";
                if (price == "fair") return $"Sold for {payout}. Client is very happy and tips you!";
                if (price == "high") return $"Sold for {payout}. Client accepted, but seems hesitant.";
                break;

            case ClientType.Demanding:
                if (price == "low") return $"Sold for {payout}. Client is disappointed but buys.";
                if (price == "fair") return $"Sold for {payout}. Client is satisfied.";
                if (price == "high") return $"Sold for {payout}. Client is surprised but pays.";
                break;
        }

        return $"Sold for {payout}.";
    }
}
