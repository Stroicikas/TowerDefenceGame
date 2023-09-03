using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static EntityManager;

public class EntityPool 
{
    private readonly Dictionary<ObjectType, Queue<Entity>> _EntityPool;
    private readonly Dictionary<ObjectType, Entity> _EntityPrefabs;
    private readonly Transform _WrapperParent;
    
    public EntityPool(Dictionary<ObjectType, Entity> prefabs, Transform parent)
    {
        _EntityPool = new Dictionary<ObjectType, Queue<Entity>>();
        _EntityPrefabs = prefabs;
        _WrapperParent = parent;
    }
    public Entity get(ObjectType type)
    {
        if (! _EntityPrefabs.ContainsKey(type)) 
        {
            Debug.LogWarning($"PreFab Not Implemented {type}");
            return null;
        }
        if (! _EntityPool.TryGetValue(type, out var Queue))
        {
            _EntityPool[type] = Queue = new Queue<Entity>();
        }
        if (Queue.Count > 0)
        {
            return Queue.Dequeue();
        }
        var Entity = Object.Instantiate(_EntityPrefabs[type],_WrapperParent);
        return Entity;
    }
    public void Return(Entity entity)
    {
        if (entity == null) 
        {
            Debug.LogWarning("Cannot Return null wrapper");
            return;
        }
        entity.Dispose();
        _EntityPool[entity.Type].Enqueue(entity);
    }
}
