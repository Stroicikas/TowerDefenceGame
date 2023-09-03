using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EntityManager;

public class Player : MonoBehaviour
{
    [SerializeField] private int _speed = 10;
    [SerializeField] private int _rotateSpeed = 10;
    [SerializeField] private Tower TowerPrefab;
    [SerializeField] private Tower SpikeTowerPrefab;
    [SerializeField] public int hp = 100;
    [SerializeField] private HPBar _hpbar;

    public float Rotation // in radians
    {
        get => transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        set => transform.rotation = Quaternion.Euler(0, value * Mathf.Rad2Deg, 0);
    }
    public void damage(int damage)
    {
        if (hp - damage <= 0)
        {
            hp = 0;
            //killcode
            //Debug.Log ("Wediededded");
        }
        else hp -= damage;
        _hpbar.setText(hp);
    }
    private void Start()
    {
        _hpbar.setup(hp);
    }
    // Update is called once per frame
    void Update()
    {
        DoMovement();
        if (Input.GetKeyDown(KeyCode.R))
        {
            //var obj = Instantiate(TowerPrefab); //Creates a copy of the prefab
            //obj.transform.position = transform.position;
            var tower = Entity.Create(ObjectType.BasicTower, transform.position) as Tower;

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            //var obj = Instantiate(SpikeTowerPrefab); //Creates a copy of the prefab
            //obj.transform.position = transform.position;
            var tower = Entity.Create(ObjectType.SpikeTower, transform.position) as Tower;
        }
    }
    private void DoMovement()
    {
        var rotate = 0;
        var xVelocity = 0;
        var yVelocity = 0;

        //rotate = KeyToInt(KeyCode.E) - KeyToInt(KeyCode.Q);
        xVelocity = KeyToInt(KeyCode.D) - KeyToInt(KeyCode.A);
        yVelocity = KeyToInt(KeyCode.W) - KeyToInt(KeyCode.S);

        var cameraAngle = Rotation;
        if (rotate != 0)
        {
            cameraAngle += Time.deltaTime * _rotateSpeed * rotate;
            Rotation = cameraAngle;
        }

        var direction = Vector3.zero;
        if (xVelocity != 0 || yVelocity != 0)
        {
            var moveSpeed = _speed;
            var moveVecAngle = Mathf.Atan2(yVelocity, xVelocity);
            direction.x = moveSpeed * Mathf.Cos(cameraAngle + moveVecAngle);
            direction.z = moveSpeed * Mathf.Sin(cameraAngle + moveVecAngle);
        }

        transform.position += Time.deltaTime * direction;
    }
    private int KeyToInt(KeyCode keyCode)
    {
        return Input.GetKey(keyCode) ? 1 : 0;
    }

}
