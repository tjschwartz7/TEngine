using System.ComponentModel.Composition;
using TEngine.Components;
using TEngine.Components.Behavior;
using TEngine.Components.Colliders;
using TEngine.Components.Inputs;
using TEngine.Components.Physics;
using TEngine.Components.Transforms.Text;
using TEngine.GameObjects;
using TEngine.TMath;
using TEngine.Utils;
using TEngine.EngineManagement;

public class GameObject
{
    private static LifecycleManager LifecycleManagerInstance = LifecycleManager.Instance;

    private bool _paused = false;
    internal bool Paused => _paused;
    public Tag ObjectTag { get; set; } = new("Default");
    public Layer ObjectLayer { get; set; } = new(0, "Default");

    private List<MonoBehavior> _monobehaviors = new List<MonoBehavior>();
    private Dictionary<Type, Component> _components = new Dictionary<Type, Component>();
    private List<GameObject> _children = new List<GameObject>();

    internal List<MonoBehavior> MonoBehaviors => _monobehaviors;
    internal Dictionary<Type, Component> Components => _components;
    internal List<GameObject> Children => _children;

    public GameObject? Parent { get; set; } = null;



    public bool _isActive = true; // Default to active
    public bool IsActive
    {
        get => _isActive;
        set
        {
            if (_isActive != value)
            {
                _isActive = value;
                PropagateActiveStatus(_isActive);
            }
        }
    }


    public GameObject()
    {
        EventManager.Instance.Subscribe("PAUSE", OnPause);
        if (Engine.GraphicSystem == GraphicSystem.Text )
            _components[typeof(TextTransform)] = new TextTransform();
    }

    public T AddComponent<T>() where T : Component, new()
    {
        var component = new T();
        component.Awake();  
        component.Owner = this;
        component.OnAttach();
        _components[typeof(T)] = component;
        return component;
    }

    // Add a MonoBehavior component
    public T AddMonoBehavior<T>() where T : MonoBehavior, new()
    {
        var behavior = new T();
        behavior.Awake();
        behavior.Owner = this;
        behavior.OnAttach();
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

    // Adding GetComponents<T> to your GameObject class
    public IEnumerable<T> GetComponents<T>() where T : Component
    {
        // Loop through all components and yield those of type T
        foreach (var component in _components.Values)
        {
            if (component is T tComponent)
            {
                yield return tComponent;
            }
        }
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

    // Methods to interact with the Lifecycle Manager
    public void StartLifecycle() => LifecycleManagerInstance.StartGameObjectLifecycle(this);
    public void UpdateLifecycle() => LifecycleManagerInstance.UpdateGameObjectLifecycle(this);
    public void FixedUpdateLifecycle() => LifecycleManagerInstance.FixedUpdateLifecycle(this);
    public void LateUpdateLifecycle() => LifecycleManagerInstance.LateUpdateLifecycle(this);
    public void OnGuiLifecycle() => LifecycleManagerInstance.GUILifecycle(this);

    public void PropagateActiveStatus(bool isActive) => LifecycleManagerInstance.PropagateActiveStatusToChildren(this, _isActive);


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
    public void MoveTo(Vector3 newPosition)
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

    // Optional: expose children/parent getters if needed
    public IReadOnlyList<GameObject> GetChildren() => Children.AsReadOnly();

    public static GameObject Create()
    {
        return new GameObject();
    }

}
