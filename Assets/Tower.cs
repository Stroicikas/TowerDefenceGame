using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;
using static EntityManager;

public class Tower : Entity
{
    [SerializeField] private Projectile ProjectilePrefab;
    [SerializeField] private float range = 5f;
    [SerializeField] private float cooldown = 0.5f;
    [SerializeField] private int proj_speed = 15; 
    [SerializeField] private int proj_damage = 20;
    [SerializeField] private int proj_amount = 1;
    [SerializeField] private float proj_lifetime = 1f;
    [SerializeField] private float proj_angleinc = 45f;

    private void shoot()
    {
        cooldown = 0.5f;
        float arcgap = proj_amount > 1 ? proj_angleinc : 0; // Calculate arcgap
        for (int i = 0; i < proj_amount; i++)
        {
            float calculatedAngle = transform.eulerAngles.y - (arcgap * (proj_amount - 1)) / 2 + i * arcgap;

            var projectile = Entity.Create(ObjectType.Projectile, transform.position) as Projectile;
            projectile.init(proj_speed, proj_damage, proj_lifetime, Vector3.forward, calculatedAngle);
        }
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
            shoot();
        }
    }
    // Start is called before the first frame updateS
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown <= 0) 
        {
            shoot();
            FindAndShootNearestEnemy();
        }
        else cooldown -= Time.deltaTime;
    }
}
