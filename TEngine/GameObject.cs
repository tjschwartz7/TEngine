using TEngine;
using TEngine.Rendering;
using TEngine.Components;
using TEngine.Components.Behavior;
using TEngine.Components.Transform;

public abstract class GameObject
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public char Symbol { get; set; }
    
    private bool _paused;
    public Transform Transform { get; private set; } = new Transform();
    public List<Monobehavior> Monobehaviors { get; private set; } = new List<Monobehavior>();


    public GameObject(int x, int y, char symbol)
    {
        X = x;
        Y = y;
        Symbol = symbol;
        TextRenderer.Instance.RegisterGameObject(this);
        EventManager.Instance.Subscribe("PAUSE", OnPause);
    }

    public void Register(Component component)
    {
        
    }



    public bool CanMove(int newX, int newY)
    {
        return !CollisionHandler.Instance.IsColliding(newX, newY);
    }

    private void OnPause()
    {
        _paused = !_paused;
    }

   



    public abstract void Update();
}
