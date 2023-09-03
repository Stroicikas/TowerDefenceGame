using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : Entity
{
 
    private int _damage = 3;
    private int _speed = 15;
    private float _lifetime = 1f;
    private Vector3 _dir = Vector3.forward;

    private bool isReady = false;
    public void init(int speed, int damage, float lifetime, Vector3 dir, float angle)
    {
        _speed = speed;
        _damage = damage;
        _lifetime = lifetime;
        _dir = dir;
        //transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        Rotation = angle * Mathf.Deg2Rad;
        isReady = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isReady) 
        {
            return;
        }
        if (_lifetime <= 0)
        {
            EntityManager.Instance.EntityPool.Return(this);
            return;
        }
        else _lifetime -= Time.deltaTime;
        transform.Translate(_dir * _speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("WeHitObject" + other.name);
        if (other.gameObject.GetComponent<Enemy>() !=null) 
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            enemy.damage(_damage);
            EntityManager.Instance.EntityPool.Return(this);
        }
    }
    public override void Dispose()
    {
        base.Dispose();
        _lifetime = _speed = _damage = 0;
        isReady = false;
    }
}
