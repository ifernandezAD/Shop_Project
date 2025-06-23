using UnityEngine;

public enum ClientType
{
    Mean,      
    Empathic,   
    Demanding   
}

public class Client
{
    public ClientType Type { get; private set; }

    public Client(ClientType type)
    {
        Type = type;
    }

    public int CalculatePayout(string priceChoice, int reputation)
    {
        int baseAmount = 0;

        switch (Type)
        {
            case ClientType.Mean:
                baseAmount = priceChoice switch
                {
                    "low" => Random.Range(45, 56),
                    "fair" => Random.Range(65, 76),
                    "high" => 0, // Rechaza
                    _ => 0
                };
                break;

            case ClientType.Empathic:
                baseAmount = priceChoice switch
                {
                    "low" => Random.Range(50, 61),
                    "fair" => Random.Range(85, 96) + Random.Range(5, 11), // +propina
                    "high" => Random.Range(40, 61), // A veces acepta
                    _ => 0
                };
                break;

            case ClientType.Demanding:
                baseAmount = priceChoice switch
                {
                    "low" => Random.Range(40, 61),
                    "fair" => Random.Range(95, 111),
                    "high" => Random.Range(0, 101), // Aleatorio
                    _ => 0
                };
                break;
        }

        // Modificadores por reputaci�n
        float modifier = 1f;

        if (reputation >= 60) modifier = 1.15f;
        else if (reputation >= 30) modifier = 1.10f;
        else if (reputation <= -60) modifier = 0.75f;
        else if (reputation <= -30) modifier = 0.90f;

        return Mathf.RoundToInt(baseAmount * modifier);
    }
}
