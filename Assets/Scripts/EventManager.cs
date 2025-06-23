using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum EventEffect
{
    None,
    HideFeedback,
    MirrorClient,
    RandomMood,
    ReputationFragile,
    HighTension,
    NoSound,
    AllDemanding,
    ReputationSpike,
    DistortedText,
    DoubleClients,
    HighPriceSafe
}

public class GameEvent
{
    public string eventName;
    public string description;
    public EventEffect effect;

    public GameEvent(string name, string desc, EventEffect eff)
    {
        eventName = name;
        description = desc;
        effect = eff;
    }
}

public class EventManager : MonoBehaviour
{
    private List<GameEvent> remainingEvents = new List<GameEvent>();
    private GameEvent currentEvent;

    [Header("UI References")]
    public TextMeshProUGUI eventTitleText;
    public TextMeshProUGUI eventEffectText;


    private void Start()
    {
        remainingEvents = new List<GameEvent>
        {
            new GameEvent("Voces en el Intercomunicador", "El sistema murmura fragmentos que no recuerdas haber dicho.", EventEffect.HideFeedback),
            new GameEvent("Cliente Espejo", "Un cliente igual a ti entra y te observa en silencio.", EventEffect.MirrorClient),
            new GameEvent("Desfase Emocional", "Los humores son caóticos.", EventEffect.RandomMood),
            new GameEvent("Tormenta Psíquica", "Tus errores pesan más.", EventEffect.ReputationFragile),
            new GameEvent("Víspera de Algo Peor", "Los clientes están tensos y desesperados.", EventEffect.HighTension),
            new GameEvent("Disonancia Auditiva", "El sonido está distorsionado.", EventEffect.NoSound),
            new GameEvent("Anomalía de Luz", "Todos se comportan como exigentes.", EventEffect.AllDemanding),
            new GameEvent("Visita del Pasado", "Un cliente te recuerda algo olvidado.", EventEffect.ReputationSpike),
            new GameEvent("Silencio Total", "Sin sonido alguno. Como un vacío.", EventEffect.NoSound),
            new GameEvent("Interferencia del Sistema", "Los nombres están distorsionados.", EventEffect.DistortedText),
            new GameEvent("Tiempo Doblado", "Hoy atenderás a dos clientes.", EventEffect.DoubleClients),
            new GameEvent("Oferta Irresistible", "Nadie se queja del precio alto.", EventEffect.HighPriceSafe)
        };

        ClearEventUI();
    }

    public void TriggerWeeklyEvent(int weekNumber)
    {
        if (weekNumber == 1)
        {
            Debug.Log("No event this week (week 1).");
            currentEvent = null;
            return;
        }

        if (remainingEvents.Count == 0)
        {
            Debug.Log("No more events left to trigger.");
            currentEvent = null;
            return;
        }

        int index = Random.Range(0, remainingEvents.Count);
        currentEvent = remainingEvents[index];
        remainingEvents.RemoveAt(index);

        ApplyEventEffect(currentEvent.effect);

        if (eventTitleText != null)
            eventTitleText.text = currentEvent.eventName;

        if (eventEffectText != null)
            eventEffectText.text = currentEvent.description;
    }

    private void ApplyEventEffect(EventEffect effect)
    {
        switch (effect)
        {
            case EventEffect.HideFeedback:
                Debug.Log("Effect Applied: Hide client feedback.");
                break;
            case EventEffect.MirrorClient:
                Debug.Log("Effect Applied: Mirror client appears.");
                break;
            case EventEffect.RandomMood:
                Debug.Log("Effect Applied: Client moods son impredecibles.");
                break;
            case EventEffect.ReputationFragile:
                Debug.Log("Effect Applied: Reputation loss doubled.");
                break;
            case EventEffect.HighTension:
                Debug.Log("Effect Applied: +25% money, -50% reputation gain.");
                break;
            case EventEffect.NoSound:
                Debug.Log("Effect Applied: Sound disabled.");
                break;
            case EventEffect.AllDemanding:
                Debug.Log("Effect Applied: All clients are demanding.");
                break;
            case EventEffect.ReputationSpike:
                Debug.Log("Effect Applied: Free reputation bonus.");
                break;
            case EventEffect.DistortedText:
                Debug.Log("Effect Applied: Names or UI distorted.");
                break;
            case EventEffect.DoubleClients:
                Debug.Log("Effect Applied: Two clients today.");
                break;
            case EventEffect.HighPriceSafe:
                Debug.Log("Effect Applied: High price accepted.");
                break;
            default:
                Debug.Log("No special effect.");
                break;
        }
    }

    public EventEffect GetCurrentEffect()
    {
        return currentEvent != null ? currentEvent.effect : EventEffect.None;
    }

    public string GetCurrentEventName()
    {
        return currentEvent != null ? currentEvent.eventName : "None";
    }

    public string GetCurrentEventDescription()
    {
        return currentEvent != null ? currentEvent.description : "";
    }



    private void ClearEventUI()
    {
        if (eventTitleText != null)
            eventTitleText.text = "";

        if (eventEffectText != null)
            eventEffectText.text = "";
    }
}
