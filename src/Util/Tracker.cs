using System;
using System.Collections.Generic;

namespace vampire;

public class Tracker
{
    public Dictionary<Type, List<Entity>> EntityMap { get; set; }

    public Tracker()
    {
        EntityMap = new Dictionary<Type, List<Entity>>();
    }
}
