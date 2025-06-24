using UnityEngine;

public enum ClientType
{
    Mean,
    Empathic,
    Demanding,
    Generous,
    Defiant,
    Proud,
    Spiteful
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
        priceChoice = priceChoice.ToLower(); // Asegura minúsculas
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
                    "low" => Random.Range(30, 41),
                    "fair" => Random.Range(50, 56) + Random.Range(10, 16), // Propina compensa
                    "high" => Random.Range(60, 71),
                    _ => 0
                };
                break;

            case ClientType.Demanding:
                baseAmount = priceChoice switch
                {
                    "low" => Random.Range(25, 36),
                    "fair" => Random.Range(95, 111),
                    "high" => Random.Range(0, 31),
                    _ => 0
                };
                break;

            case ClientType.Generous:
                baseAmount = priceChoice switch
                {
                    "low" => 0, // No acepta
                    "fair" => Random.Range(70, 81) + Random.Range(5, 11),
                    "high" => Random.Range(85, 96) + Random.Range(10, 16),
                    _ => 0
                };
                break;

            case ClientType.Defiant:
                baseAmount = priceChoice switch
                {
                    "low" => Random.Range(50, 61),
                    "fair" => 0, // Rechaza lo justo
                    "high" => Random.Range(65, 76),
                    _ => 0
                };
                break;

            case ClientType.Proud:
                baseAmount = priceChoice switch
                {
                    "low" => 0, // Ofensa
                    "fair" => Random.Range(55, 66),
                    "high" => Random.Range(80, 91),
                    _ => 0
                };
                break;

            case ClientType.Spiteful:
                baseAmount = priceChoice switch
                {
                    "low" => Random.Range(35, 41),
                    "fair" => Random.Range(65, 76),
                    "high" => 0, // No quiere que te aproveches
                    _ => 0
                };
                break;
        }

        // Modificador por reputación
        float modifier = 1f;
        if (reputation >= 60) modifier = 1.15f;
        else if (reputation >= 30) modifier = 1.10f;
        else if (reputation <= -60) modifier = 0.75f;
        else if (reputation <= -30) modifier = 0.90f;

        return Mathf.RoundToInt(baseAmount * modifier);
    }
}
