using System.Collections;
using System.Collections.Generic;

namespace vampire;

public class Scene : IEnumerable<Entity>, IEnumerable
{
    public List<Entity> entities;
    public Tracker Tracker;

    public Scene()
    {
        entities = new List<Entity>();
        Tracker = new Tracker();
    }

    public virtual void Start()
    {
        entities.ForEach(e => e.Start());
    }

    public virtual void End()
    {
        entities.ForEach(e => e.End());
    }

    public virtual void Update()
    {
        entities.ForEach(e => e.Update());
    }

    public virtual void Render()
    {
        entities.ForEach(e => e.Render());
    }

    public void Destroy() { }

    public void AddEntity(Entity entity)
    {
        entities.Add(entity);
        entity.Added(this);
        if (!Tracker.EntityMap.ContainsKey(entity.GetType()))
            Tracker.EntityMap.Add(entity.GetType(), new List<Entity>());
        Tracker.EntityMap[entity.GetType()].Add(entity);
    }

    public void RemoveEntity(Entity entity)
    {
        entities.Remove(entity);
        Tracker.EntityMap[entity.GetType()].Remove(entity);
        // GC?
    }

    // return first instance
    public T GetEntity<T>()
        where T : Entity
    {
        foreach (var entity in entities)
        {
            if (entity.GetType() == typeof(T))
                return (T)entity;
        }
        return null;
    }

    public IEnumerator<Entity> GetEnumerator()
    {
        return entities.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
/*
hashmap with all alive tags and values, integrate with entity list. Simliar to the browser history problem from Bloomberg interview

Scene should have:

Dictionary typeTracker =
{
    [type]: [instances, ...]

};
 */
