using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    public class EnemyData
    {
        public readonly int Hp;
        public readonly int ToSpawn;
        public readonly int CurrentlyOnKill;

        public EnemyData(int hp, int toSpawn, int currentlyOnKill)
        {
            Hp = hp;
            ToSpawn = toSpawn;
            CurrentlyOnKill = currentlyOnKill;
        }
    }

    public class WaveData
    {
        public readonly EnemyData[] Enemies;

        public WaveData(params EnemyData[] enemies)
        {
            Enemies = enemies;
        }
    }

    public class Waves
    {
        public readonly WaveData[] WavesData = new WaveData[]
        {
            new WaveData(
                new EnemyData(5, 10, 2),
                new EnemyData(10, 5, 4),
                new EnemyData(20, 2, 10)
            ),
            // Add more waves when want
        };
    }

    private int currentWave = 0;
    private Waves wavesInstance; // Create an instance of the Waves class

    void Start()
    {
        wavesInstance = new Waves(); // Instantiate the Waves class

        // To process the current wave, you can do something like this:
        if (currentWave < wavesInstance.WavesData.Length)
        {
            foreach (EnemyData enemyData in wavesInstance.WavesData[currentWave].Enemies)
            {
                // Process enemyData
            }
            currentWave++; // Move to the next wave
        }
        else
        {
            // All waves have been completed
        }
    }

    void Update()
    {
        // Your update logic here
    }
}