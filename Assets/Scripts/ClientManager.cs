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
        ClientType type = (ClientType)Random.Range(0, System.Enum.GetValues(typeof(ClientType)).Length);
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

            if (price == "fair")
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

        yield return new WaitForSeconds(1.5f);

        feedbackText.text = "";

        calendar.AdvanceDay();
        GenerateNewClient();
    }

    private string GetClientHint(ClientType type)
    {
        return type switch
        {
            ClientType.Mean => "Hint: This client dislikes high prices.",
            ClientType.Empathic => "Hint: Appreciates fairness. May leave a tip.",
            ClientType.Demanding => "Hint: Wants exact value. Careful with price.",
            ClientType.Generous => "Hint: Will pay well, especially if you're bold.",
            ClientType.Defiant => "Hint: Hates normality. Be extreme.",
            ClientType.Proud => "Hint: Hates being underestimated.",
            ClientType.Spiteful => "Hint: Overpricing offends them deeply.",
            _ => ""
        };
    }

    private string GetSaleOutcomeMessage(ClientType type, string price, int payout, bool success)
    {
        if (!success) return "The client left unhappy and without paying.";

        switch (type)
        {
            case ClientType.Mean:
                return price switch
                {
                    "low" => $"Sold for {payout}. Client is cautiously satisfied.",
                    "fair" => $"Sold for {payout}. Client is pleased.",
                    _ => "Client rejected the price."
                };

            case ClientType.Empathic:
                return price switch
                {
                    "low" => $"Sold for {payout}. Client appreciates the deal.",
                    "fair" => $"Sold for {payout}. Client is very happy and tips you!",
                    "high" => $"Sold for {payout}. Client accepted, but seems hesitant.",
                    _ => ""
                };

            case ClientType.Demanding:
                return price switch
                {
                    "low" => $"Sold for {payout}. Client is disappointed but buys.",
                    "fair" => $"Sold for {payout}. Client is satisfied.",
                    "high" => $"Sold for {payout}. Client is surprised but pays.",
                    _ => ""
                };

            case ClientType.Generous:
                return price switch
                {
                    "low" => "Client rejected the offer. Too low!",
                    "fair" => $"Sold for {payout}. Client is happy and tips you.",
                    "high" => $"Sold for {payout}. Client is thrilled and rewards you generously!",
                    _ => ""
                };

            case ClientType.Defiant:
                return price switch
                {
                    "low" => $"Sold for {payout}. Client smirks approvingly.",
                    "fair" => "Client rejected the fair price. Too boring.",
                    "high" => $"Sold for {payout}. Client respects your boldness.",
                    _ => ""
                };

            case ClientType.Proud:
                return price switch
                {
                    "low" => "Client rejected the offer. They felt insulted.",
                    "fair" => $"Sold for {payout}. Client nods with quiet pride.",
                    "high" => $"Sold for {payout}. Client accepts, impressed by your confidence.",
                    _ => ""
                };

            case ClientType.Spiteful:
                return price switch
                {
                    "low" => $"Sold for {payout}. Client grudgingly accepts.",
                    "fair" => $"Sold for {payout}. Client is calm and pays.",
                    "high" => "Client rejected your greed and walked away.",
                    _ => ""
                };
        }

        return $"Sold for {payout}.";
    }
}
