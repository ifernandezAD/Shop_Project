using UnityEngine;
using TMPro;

public class GameCalendar : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool debugAdvanceDay;

    public TextMeshProUGUI calendarText;
    public RentManager rentManager;
    public EventManager eventManager;

    private int currentDay = 1;
    private int currentWeek = 1;

    private readonly int totalDays = 90;
    private readonly int daysPerWeek = 7;

    private string[] monthNames = { "Veltrum", "Noxar", "Syrr" };


    private void OnValidate()
    {
        if (debugAdvanceDay)
        {
            AdvanceDay();
            debugAdvanceDay = false;
        }
    }

    void Start()
    {
        UpdateCalendarText();
        CheckEvents();
    }

    public void AdvanceDay()
    {
        if (currentDay >= totalDays)
        {
            Debug.Log("Game Over: reached final day.");
            return;
        }

        currentDay++;
        currentWeek = ((currentDay - 1) / daysPerWeek) + 1;

        UpdateCalendarText();
        CheckEvents();
    }

    private void CheckEvents()
    {
        // Weekly event
        if ((currentDay - 1) % daysPerWeek == 0)
        {
            Debug.Log($"Week {currentWeek} begins — Weekly event triggered.");
            if (eventManager != null)
            {
                eventManager.TriggerWeeklyEvent(currentWeek);
            }
            else
            {
                Debug.LogWarning("EventManager reference missing!");
            }
        }

        // Rent day
        if (currentDay % 15 == 0)
        {
            Debug.Log($"Day {currentDay} — The landlord has arrived to collect rent.");
            if (rentManager != null)
            {
                rentManager.CheckForRent(currentDay);
            }
            else
            {
                Debug.LogWarning("RentManager reference missing!");
            }
        }
    }

    private void UpdateCalendarText()
    {
        if (calendarText != null)
        {
            string month = GetCurrentMonth();
            int dayInMonth = GetDayInMonth();

            calendarText.text = $" Day {dayInMonth} – {month}";
        }
    }

    private string GetCurrentMonth()
    {
        if (currentDay <= 30) return monthNames[0];   
        else if (currentDay <= 60) return monthNames[1];  
        else return monthNames[2];  
    }

    private int GetDayInMonth()
    {
        if (currentDay <= 30) return currentDay;
        else if (currentDay <= 60) return currentDay - 30;
        else return currentDay - 60;
    }
}
