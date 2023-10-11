using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] private Transform _entitytransform;
    private Dictionary<ObjectType, Entity> _EntityPrefabs;
    private Dictionary<int, Entity> _Entities;
    public static int NextEntityID = 0;
    public EntityPool EntityPool;
    public static EntityManager Instance;

    public void AddObject(Entity entity)
    {
        _Entities[NextEntityID] = entity;
    }

    private void Awake()
    {
        _EntityPrefabs = new Dictionary<ObjectType, Entity>();
        _Entities = new Dictionary<int, Entity>();
        foreach (var entity in Resources.LoadAll<Entity>("Entities"))
        {
            _EntityPrefabs[Enum.Parse<ObjectType>(entity.name)] = entity;
        }
        EntityPool = new EntityPool(_EntityPrefabs, _entitytransform);
        Instance = this;
    }

    public Entity GetEntity(int id)
    {
        if (_Entities.TryGetValue(id, out var entity)) return entity;
        return null;
    }

    public IEnumerable<Entity> GetEntities()
    {
        return _Entities.Values;
    }

    public enum ObjectType
    {
        Projectile,
        BasicTower,
        SpikeTower,
        Enemy,
        DualTower,
        RapidTower,
    }
}
