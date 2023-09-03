using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using static EntityManager;

public class Entity : MonoBehaviour
{
    public float Rotation // in radians
    {
        get => transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        set => transform.rotation = Quaternion.Euler(0, value * Mathf.Rad2Deg, 0);
    }
    public ObjectType Type;

    public int Id;

    public virtual void Dispose()
    {
        transform.localScale = Vector3.zero;
    }
    public virtual void Init (ObjectType type, int id)
    {
        Type = type;
        Id = id;
        transform.localScale = Vector3.one;
    }
    public static Entity Create (ObjectType type, Vector3 position)
    {
        var entity = EntityManager.Instance.EntityPool.get (type);
        entity.transform.position = position;
        entity.Init(type, EntityManager.NextEntityID++);
        EntityManager.Instance.AddObject(entity);
        return entity;
    }
}
