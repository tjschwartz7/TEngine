using TEngine.Components;
using TEngine.Components.Behavior;
using TEngine.Components.Colliders;
using TEngine.Components.Inputs;
using TEngine.Components.Physics;
using TEngine.Components.Transforms.Text;
using TEngine.TMath;
using TEngine.Utils;

public abstract class GameObject
{
    private bool _paused;
    private List<MonoBehavior> _monobehaviors { get; set; } = new();

    private Dictionary<Type, Component> _components = new();
    public Tag ObjectTag { get; set; } = new("Default");
    public Layer ObjectLayer { get; set; } = new(0, "Default");

    // Optional reference to a parent object
    public GameObject? Parent { get; private set; } = null;
    public List<GameObject> Children { get; private set; } = new();
    public bool IsActive { get; private set; } = true;
    public bool isActiveAndEnabled { get; private set; } = true;


    public GameObject()
    {
        EventManager.Instance.Subscribe("PAUSE", OnPause);
        _components[typeof(TextTransform)] = new TextTransform();
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
    }

    public bool IsPaused()
    {
        return _paused;
    }

    // Move the GameObject by updating its Transform position
    public void MoveTo(Vector2Int newPosition)
    {
        if(HasComponent<TextTransform>())
            GetComponent<TextTransform>()?.MoveTo(newPosition);
    }

    public void SetParent(GameObject newParent)
    {
        // Remove from current parent
        Parent?.Children.Remove(this);

        // Set new parent
        Parent = newParent;

        // Add to new parent's children list
        Parent?.Children.Add(this);
    }

    public void AddChild(GameObject child)
    {
        child.SetParent(this);
    }

    public void RemoveChild(GameObject child)
    {
        if (Children.Contains(child))
        {
            child.Parent = null;
            Children.Remove(child);
        }
    }

    public IEnumerable<GameObject> GetHierarchy()
    {
        yield return this;
        foreach (var child in Children)
        {
            foreach (var desc in child.GetHierarchy())
                yield return desc;
        }
    }

    public void Update()
    {
        
        isActiveAndEnabled = IsActive;
        if (Parent != null) { isActiveAndEnabled = isActiveAndEnabled && !Parent.IsPaused(); }

        if (_paused || !IsActive) return;

        foreach (var behavior in _monobehaviors)
        {
            if (behavior.isActiveAndEnabled)
            {
                behavior.Update();
            }
        }

        foreach (var component in _components.Values)
        {
            if (component.enabled)
            {
                component.Update();
            }
        }

        foreach (var child in Children)
        {
            child.Update();
        }
    }

    // Optional: expose children/parent getters if needed
    public IReadOnlyList<GameObject> GetChildren() => Children.AsReadOnly();

}
