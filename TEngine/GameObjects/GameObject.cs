using TEngine;
using TEngine.Rendering;
using TEngine.Components;
using TEngine.Components.Behavior;
using TEngine.Components.Transform;
using TEngine.Components.Physics;
using TEngine.Components.Colliders;

public abstract class GameObject
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public char Symbol { get; set; }
    
    private bool _paused;
    public Transform transform { get; private set; } = new Transform();
    public Rigidbody? rigidbody { get; set; }
    public Collider? collider { get; set; }
    public List<MonoBehavior> monobehaviors { get; private set; } = new List<MonoBehavior>();


    public GameObject(int x, int y, char symbol)
    {
        X = x;
        Y = y;
        Symbol = symbol;
        EventManager.Instance.Subscribe("PAUSE", OnPause);
    }

    public void AddComponent<T>() where T : Component, new()
    {
        if (typeof(T) == typeof(Rigidbody)) { rigidbody = new Rigidbody(); }
        else if (typeof(T) == typeof(Collider)) { collider = new Collider(); }
    }

    public void AddComponent(Component component)
    {
        if (component is Rigidbody) { rigidbody = (Rigidbody)component; }
        else if (component is Collider) { collider = (Collider)component; }
        else if (component is MonoBehavior) { monobehaviors.Add((MonoBehavior)component); }
    }


    private void OnPause()
    {
        _paused = !_paused;
        for(int i = 0; i < monobehaviors.Count; i++)
        {
            monobehaviors[i].isActiveAndEnabled = monobehaviors[i].enabled && _paused;
        }
    }

   



    public abstract void Update();
}
