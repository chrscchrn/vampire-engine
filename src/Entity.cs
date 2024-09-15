using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace vampire;

public class Entity : IEnumerable<Component>, IEnumerable
{
    private Vector2 _position = new(0, 0);
    public Vector2 Position
    {
        get => _position;
        set => _position = value;
    }

    public List<Component> components = new();
    public Scene Scene;

    public string name;
    public int tag;

    public List<Collider> ColliderTracker = new();

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
    }

    public void Added(Scene _scene)
    {
        Scene = _scene;
    }

    // cols
    public bool CollideCheck(Entity other)
    {
        foreach (Collider collider in ColliderTracker)
            if (collider.Check(Position, other))
                return true;
        return false;
    }

    public bool CollideCheck<T>(Vector2 position)
        where T : Entity
    {
        // will it collide at this new position
        if (!Scene.Tracker.EntityMap.ContainsKey(typeof(T)))
            return false;
        foreach (Collider collider in ColliderTracker)
            if (collider.Check(position, Scene.Tracker.EntityMap[typeof(T)]))
                return true;
        return false;
    }

    // public bool CollideCheck<T>()
    //     where T : Entity
    // {
    //     // is it currently colliding at this position
    //     return false;
    // }

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
        T component = new();
        components.Add(component);
        component.Added(this);
        if (typeof(Collider) == typeof(T))
            ColliderTracker.Add((Collider)(object)component);
        return component;
    }

    // return types not working
    public virtual Component AddComponent(Component component)
    {
        components.Add(component);
        component.Added(this);
        if (component is Collider collider)
            ColliderTracker.Add(collider);
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
