using TEngine.Components;
using TEngine.Components.Behavior;
using TEngine.Core;
using TEngine.Components.Transforms.Text;
using TEngine.TMath;
using TEngine.Utils;
using TEngine.Core.AssetManagement.GameObjects;

public class GameObject
{
    private static LifecycleManager LifecycleManagerInstance = LifecycleManager.Instance;

    private bool _paused = false;
    internal bool Paused => _paused;
    public string Tag { get; private set; } = "Default";
    public Layer Layer { get; set; } = new(0, "Default");
    public int ID { get; private set; } = 0; // Unique ID for the GameObject
    public string Name { get; private set; } = "Default"; // Name of the GameObject

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


    public GameObject(int id, string name = "", string tag = "")
    {
        EventManager.Instance.Subscribe("PAUSE", OnPause);
        if (Engine.GraphicSystem == GraphicSystem.Text )
            _components[typeof(TextTransform)] = new TextTransform();

        ID = id;
        Tag = tag;
        Name = name;
    }

    public T AddComponent<T>() where T : Component, new()
    {
        var component = new T();
        //If the component is not already attached to the GameObject,
        if (!HasComponent<T>())
        {
            // Set up the component
            component.Awake();
            component.Owner = this;
            component.OnAttach();
            // and add it to the components list.
            _components[typeof(T)] = component;

            //Return the newly attached component
            return component;
        }
        // If the component already exists, 
        else
        {
            //return the existing one.
            //Ignore the warning, this case only is reached if the component exists.
            return GetComponent<T>();
        }
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

    public void SetLayer(int layer) { this.Layer = Layers.GetLayer(layer); }
    public void SetLayer(string name) { this.Layer = Layers.GetLayer(name); }

    internal void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name cannot be null or empty.");
        }
        Name = name;
    }

    internal void SetTag(string tag)
    {
        if (string.IsNullOrEmpty(tag))
        {
            throw new ArgumentException("Tag cannot be null or empty.");
        }
        Tag = tag;
    }
}
