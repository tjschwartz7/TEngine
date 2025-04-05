using TEngine.Components;
using TEngine.Components.Behavior;
using TEngine.Components.Colliders;
using TEngine.Components.Inputs;
using TEngine.Components.Physics;
using TEngine.TMath;
using TEngine.Utils;

public abstract class GameObject
{
    private bool _paused;
    private List<MonoBehavior> _monobehaviors { get; set; } = new();

    private Dictionary<Type, Component> _components = new();
    public Tag ObjectTag { get; set; } = new("Default");
    public Layer ObjectLayer { get; set; } = new(0, "Default");


    public GameObject()
    {
        EventManager.Instance.Subscribe("PAUSE", OnPause);
        _components[typeof(Transform)] = new Transform();
    }

    public T AddComponent<T>() where T : Component, new()
    {
        var component = new T();
        component.Owner = this;
        _components[typeof(T)] = component;
        return component;
    }

    // Add a MonoBehavior component
    public T AddMonoBehavior<T>() where T : MonoBehavior, new()
    {
        var behavior = new T();
        behavior.Owner = this;
        _monobehaviors.Add(behavior);
        return behavior;
    }

    // Check for MonoBehavior (doesn't mix with regular components)
    public T? GetMonoBehavior<T>() where T : MonoBehavior
    {
        return _monobehaviors.OfType<T>().FirstOrDefault();
    }

    public T? GetComponent<T>() where T : Component
    {
        return _components.TryGetValue(typeof(T), out var comp) ? comp as T : null;
    }

    public bool HasComponent<T>() where T : Component
    {
        return _components.ContainsKey(typeof(T));
    }

    /// <summary>
    /// This function allows you to add a MonoBehavior to the game object.
    /// </summary>
    /// <param name="behavior">The new MonoBehavior to be added.</param>
    public void AddBehavior(MonoBehavior behavior)
    {
        behavior.Owner = this;
        _monobehaviors.Add(behavior);
        
    }


    /// <summary>
    /// This function is used locally to manage game pausing. 
    /// It should be internally managed by the main game loop.
    /// </summary>
    private void OnPause()
    {
        _paused = !_paused;
        for(int i = 0; i < _monobehaviors.Count; i++)
        {
            _monobehaviors[i].isActiveAndEnabled = _monobehaviors[i].enabled && _paused;
        }
    }

    // Move the GameObject by updating its Transform position
    public void MoveTo(Vector2Int newPosition)
    {
        if(HasComponent<Transform>())
            GetComponent<Transform>()?.MoveTo(newPosition);
    }

}
