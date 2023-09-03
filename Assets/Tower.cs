using System.Collections;
using System.Collections.Generic;
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
        for (int i = 0; i < proj_amount; i++)
        {
            //var projectile = Instantiate(ProjectilePrefab, transform);
            var projectile = Entity.Create(ObjectType.Projectile, transform.position) as Projectile;
            projectile.init(15, 20, 1f, Vector3.forward, i * proj_angleinc);

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown <= 0) 
        {
            shoot();
        }
        else cooldown -= Time.deltaTime;
    }
}
