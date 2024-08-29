namespace vampire;

public class Component
{
    public Entity Entity { get; private set; }

    public bool isActive = true;

    public Component() { }

    public Component(bool isActive)
    {
        this.isActive = isActive;
    }

    //public virtual void EntityAwake()
    //{

    //}

    public virtual void Start() { }

    public virtual void Update() { }

    public virtual void End() { }

    public virtual void Render() { }

    public void Added(Entity entity)
    {
        Entity = entity;
    }
}
