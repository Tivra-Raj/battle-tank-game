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

    public EventController OnPlayerFiredBulletEvent { get; private set; }
    public EventController OnPlayerKilledEnemiesEvent { get; private set; }
    public EventController OnPlayerTravelledDistanceEvent { get; private set; }
    
    public EventService()
    {
        OnPlayerFiredBulletEvent = new EventController();
        OnPlayerKilledEnemiesEvent = new EventController();
        OnPlayerTravelledDistanceEvent = new EventController();
    }
}