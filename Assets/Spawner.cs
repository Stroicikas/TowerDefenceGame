using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;
using static EntityManager;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Player player;
    [SerializeField] private NodeController nodeController;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        if(player.hp  <= 0)
        {
            return;
        }
            //var obj = Instantiate(enemyPrefab, transform); //Spawns copy of the variable(enemyPrefab) into the game
            //obj.transform.localPosition = new Vector3(i * 1,0,0);
           //obj.init(player,nodeController.GetNodeArray(),20, 20);
        var entity=Entity.Create(ObjectType.Enemy, transform.position) as Enemy;
        entity.init(player, nodeController.GetNodeArray(), 20, 20);
        
        
    }
}
