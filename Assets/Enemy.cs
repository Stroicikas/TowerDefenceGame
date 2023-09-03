using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private int _speed = 10;
    [SerializeField] private int _hp = 10;
    private int currentNode = 0;
    [SerializeField] private GameObject[] nodeList;
    private Player _player;
    private bool _isReady = false;
    public void init(Player player,GameObject[] Nodes, int hp, int speed)
    {
        nodeList = Nodes;
        _hp = hp;
        _speed = speed;
        _player = player;
        _isReady = true;
    }
    public override void Dispose()
    {
        _player = null;
        nodeList = null;
        base.Dispose();
        _isReady = false;
        currentNode = 0;
    }
    public void damage(int damage)
    {
        if (_hp - damage <= 0)
        {
            EntityManager.Instance.EntityPool.Return(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isReady || nodeList == null || _player == null || nodeList.Length == 0)
            return;
        if (Vector3.Distance(transform.position, nodeList[currentNode].transform.position) < 0.1)
        {
            if(currentNode + 1 >= nodeList.Length) {
                //lowerplayerhealth
                _player.damage(1);
                EntityManager.Instance.EntityPool.Return(this);
                return;
            }
            currentNode++; //increases node counter
        }
        transform.LookAt(nodeList[currentNode].transform); //will look at current node
        //transform.position += Vector3.forward * _speed * Time.deltaTime; //moves forwards towards where we look at
        transform.Translate(Vector3.forward * _speed * Time.deltaTime); 
    }
}
