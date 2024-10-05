using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace vampire;

public class Entity : IEnumerable<Component>, IEnumerable
{
    public Vector2 Position = new(0, 0);
    public List<Component> components = new();
    public Scene Scene;

    public string name;
    public int tag;

    private Collider collider;
    public Collider Collider
    {
        get => collider;
        set
        {
            collider = value;
            collider.Entity = this;
        }
    }

    public Entity(Vector2 _position)
    {
        Position = _position;
    }

    public Entity() { }

    public virtual void Start()
    {
        components.ForEach(c => c.Start());
    }

    public virtual void End()
    {
        components.ForEach(c => c.End());
    }

    public virtual void Update()
    {
        components.ForEach(c => c.Update());
    }

    public virtual void Render()
    {
        components.ForEach(c => c.Render());
    }

    public virtual void DebugRender()
    {
        components.ForEach(c => c.DebugRender());
        if (Collider != null)
            Collider.DebugRender();
    }

    public void Added(Scene _scene)
    {
        Scene = _scene;
    }

    // cols
    public bool CollideCheck(Entity other)
    {
        return Collide.Check(this, other);
    }

    public bool CollideCheck(Vector2 position, Entity other)
    {
        return Collide.Check(this, position, other);
    }

    public bool CollideCheck<T>(Vector2 position)
        where T : Entity
    {
        // will it collide at this new position
        if (!Scene.Tracker.EntityMap.ContainsKey(typeof(T)))
            return false;
        return Collide.Check(this, position, Scene.Tracker.EntityMap[typeof(T)]);
    }

    public bool CollideCheck<T>()
        where T : Entity
    {
        if (!Scene.Tracker.EntityMap.ContainsKey(typeof(T)))
            return false;
        return Collide.Check(this, Scene.Tracker.EntityMap[typeof(T)]);
    }

    public T GetComponent<T>()
        where T : Component
    {
        foreach (var component in components)
            if (component.GetType() == typeof(T))
                return (T)component;
        return null;
    }

    public T AddComponent<T>()
        where T : Component, new()
    {
        T component = new T();
        components.Add(component);
        component.Added(this);
        return component;
    }

    // return types not working
    public Component AddComponent(Component component)
    {
        components.Add(component);
        component.Added(this);
        return component;
    }

    public IEnumerator<Component> GetEnumerator()
    {
        return components.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
