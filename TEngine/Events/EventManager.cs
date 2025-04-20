public class EventManager
{
    private Dictionary<string, Action<object>> oneArgeventDictionary = new Dictionary<string, Action<object>>();
    private Dictionary<string, Action> noArgeventDictionary = new Dictionary<string, Action>();

    public static EventManager Instance { get; private set; } = new EventManager();

    // Subscribe to events
    public void Subscribe(string eventName, Action<object> listener)
    {
        if (!oneArgeventDictionary.ContainsKey(eventName))
        {
            oneArgeventDictionary[eventName] = listener;
        }
        else
        {
            oneArgeventDictionary[eventName] += listener;
        }
    }

    // Unsubscribe from events
    public void Unsubscribe(string eventName, Action<object> listener)
    {
        if (oneArgeventDictionary.ContainsKey(eventName))
        {
            oneArgeventDictionary[eventName] -= listener;
            if (oneArgeventDictionary[eventName] == null)
            {
                oneArgeventDictionary.Remove(eventName);
            }
        }
    }


    // Subscribe to events
    public void Subscribe(string eventName, Action listener)
    {
        if (!noArgeventDictionary.ContainsKey(eventName))
        {
            noArgeventDictionary[eventName] = listener;
        }
        else
        {
            noArgeventDictionary[eventName] += listener;
        }
    }

    // Unsubscribe from events
    public void Unsubscribe(string eventName, Action listener)
    {
        if (noArgeventDictionary.ContainsKey(eventName))
        {
            noArgeventDictionary[eventName] -= listener;
            if (noArgeventDictionary[eventName] == null)
            {
                noArgeventDictionary.Remove(eventName);
            }
        }
    }

    // Trigger an event with arguments (supports passing null)
    public void Trigger(string eventName, object args)
    {
        if (oneArgeventDictionary.ContainsKey(eventName))
        {
            oneArgeventDictionary[eventName]?.Invoke(args);
        }
    }

    // Overload the Trigger method to trigger without arguments
    public void Trigger(string eventName)
    {
        if (noArgeventDictionary.ContainsKey(eventName))
        {
            noArgeventDictionary[eventName]?.Invoke();
        }
    }
}
