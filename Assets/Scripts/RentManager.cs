using UnityEngine;
using TMPro;

public class RentManager : MonoBehaviour
{
    public Resources resources;
    public TextMeshProUGUI rentText;
    public GameCalendar calendar;
    public GameObject gameOverPanel;
    public GameObject winPanel;

    private int[] paymentDays = { 15, 30, 45, 60, 75, 90 };
    private int[] paymentAmounts = { 500, 700, 900, 1100, 1300, 1500 };
    private int currentPaymentIndex = 0;

    public void CheckForRent(int currentDay)
    {
        if (currentPaymentIndex >= paymentDays.Length) return;

        if (currentDay == paymentDays[currentPaymentIndex])
        {
            int amountDue = paymentAmounts[currentPaymentIndex];
            int currentMoney = resources.GetMoney();

            if (currentMoney >= amountDue)
            {
                resources.ChangeMoney(-amountDue);
                rentText.text = $"Rent paid: {amountDue}";
                Debug.Log($"[RENT PAID] Day {currentDay}, Paid {amountDue}");
                currentPaymentIndex++;

                if (currentDay == 90)
                {
                    winPanel.SetActive(true);
                    Debug.Log(" You win!");
                }
            }
            else
            {
                rentText.text = $"Rent unpaid: Needed {amountDue}, had {currentMoney}";
                gameOverPanel.SetActive(true);
                Debug.Log(" Game Over: Not enough money for rent");
            }
        }
    }
}
