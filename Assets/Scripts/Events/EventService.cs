using BattleTank.Achievement;
using UnityEngine;

public class EventService
{
    private static EventService instance;
    public static EventService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventService();
                Debug.Log("Event Service is created");
            }
            return instance;
        }
    }

    public EventController<float> OnDistanceTravelledEvent { get; private set; }

    public EventService()
    {
        OnDistanceTravelledEvent = new EventController<float>();
        Debug.Log("on Distance Event ");
    }
}