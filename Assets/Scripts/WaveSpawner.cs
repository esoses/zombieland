using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState {SPAWNING,  COUNTING}

    public TextMeshProUGUI waveDisplay;
    private bool IsFirstWave = true;

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemy = new Transform[3];
        public int[] count = new int[3];
        public float rate;
        public int timeUntilNextWave;
    }

    public Wave[] waves;
    public Transform[] SpawnPoints;
    private int nextWave = 0;
    
    private float waveCountdown;

    private SpawnState state = SpawnState.COUNTING;

    private float countMultiplayer = 1;
    private float rateMultiplayer = 1;

    private void Start()
    {
        
        waveCountdown = 10f;

        if (SpawnPoints.Length == 0)
        {
            Debug.LogError("No enemy spawnpoints set");
        }
    }

    private void Update()
    {
       
        

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));                
            }            
        }
        else
        {
            waveCountdown -= Time.deltaTime;
            if (IsFirstWave == true)
            {
                waveDisplay.text = "Time until first wave: " + waveCountdown.ToString("f1");
            }
            else
            {
                waveDisplay.text = "Time until next wave: " + waveCountdown.ToString("f1");
            }
        }
    }

    void StartNewWave()
    {

        state = SpawnState.COUNTING;
        
        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All waves completed! Reseting wave count...");
            countMultiplayer += 0.5f;
            rateMultiplayer += 0.5f;
        }
        else
        {
            nextWave++;            
        }
        if (nextWave - 1 != 0)
        {
            waveCountdown = waves[nextWave - 1].timeUntilNextWave;
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;
        Debug.Log(_wave.name);




        waveDisplay.text = "Spawning wave: " + _wave.name;

         if (_wave.enemy[0] != null || _wave.enemy[1] != null || _wave.enemy[2] != null)
         {
             for (int index0 = 0; index0 < _wave.count[0] * countMultiplayer; index0++)
             {
                 Debug.Log("Tried to spawn enemy: 0");
                 SpawnEnemy(_wave.enemy[0]);
                 yield return new WaitForSeconds(1f / _wave.rate * rateMultiplayer);
             }
             for (int index1 = 0; index1 < _wave.count[1] * countMultiplayer; index1++)
             {
                 Debug.Log("Tried to spawn enemy: 1");
                 SpawnEnemy(_wave.enemy[1]);
                 yield return new WaitForSeconds(1f / _wave.rate * rateMultiplayer);
             }
             for (int index2 = 0; index2 < _wave.count[2] * countMultiplayer; index2++)
             {
                 Debug.Log("Tried to spawn enemy: 2");
                 SpawnEnemy(_wave.enemy[2]);
                 yield return new WaitForSeconds(1f / _wave.rate * rateMultiplayer);
             }
         }
        StartNewWave();
        IsFirstWave = false;
        yield break;
    }

   /*IEnumerator Repeat(Wave wave, int enemyIndex)
    {
        if(wave.enemy[enemyIndex] != null)
        {
            for(int i = 0; i < wave.count[enemyIndex] * countMultiplayer; i++)
            {
                Debug.Log("Tried to spawn enemy: {0}, index: " + enemyIndex, wave.enemy[enemyIndex]);
                SpawnEnemy(wave.enemy[enemyIndex]);
                yield return new WaitForSeconds(1f / wave.rate * rateMultiplayer);
            }
        }

        yield break;
    }*/

    void SpawnEnemy(Transform _enemy)
    {        
        Transform spawn = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        Instantiate(_enemy, spawn.position, spawn.rotation);
    }
}
