using BattleTank.Achievement;

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
            }
            return instance;
        }
    }

    public EventController<float> OnDistanceTravelledEvent { get; private set; }

    public EventService()
    {
        OnDistanceTravelledEvent = new EventController<float>();
    }
}