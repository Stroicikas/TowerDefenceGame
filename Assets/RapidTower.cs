using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;
using static EntityManager;

public class RapidTower : Entity
{
    [SerializeField] public Projectile ProjectilePrefab;
    [SerializeField] public float range = 10f;
    [SerializeField] public float cooldown = 0.1f;
    [SerializeField] public int proj_speed = 15; 
    [SerializeField] public int proj_damage = 20;
    [SerializeField] public int proj_amount = 1;
    [SerializeField] public float proj_lifetime = 1f;
    [SerializeField] public float proj_angleinc = 45f;
    private void shoot1()
    {
        cooldown = 0.1f;
        for (int i = 0; i < proj_amount; i++)
        {
            //var projectile = Instantiate(ProjectilePrefab, transform);
            var projectile = Entity.Create(ObjectType.Projectile, transform.position) as Projectile;
            projectile.init(proj_speed, proj_damage, proj_lifetime, Vector3.forward, i * proj_angleinc);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            shoot1();
            FindAndShootNearestEnemy();
        }
        else cooldown -= Time.deltaTime;
    }
    private void FindAndShootNearestEnemy()
    {
        float closestDistance = float.MaxValue;
        Entity nearestEnemy = null;

        foreach (var entity in EntityManager.Instance.GetEntities())
        {
            if (entity.Type == ObjectType.Enemy)
            {
                float distance = Vector3.Distance(transform.position, entity.transform.position);
                if (distance <= range && distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestEnemy = entity;
                }
            }
        }

        if (nearestEnemy != null)
        {
            // Aim at the nearest enemy and shoot
            Vector3 direction = (nearestEnemy.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);
            shoot1();
        }
    }
}
