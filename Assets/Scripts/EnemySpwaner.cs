﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{
    public enum spwanState { Spwaning,Waiting,Counting};

    [System.Serializable]
    public class Wave
    {
        public string Name;
        public Transform[] enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextwave = 0;
    private int waveCount = 0;
    private int releaseCount = 1;

    public Transform[] spwanPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountDown;
    private float searchCountDown = 1f;

    int maxEnemy = 1;

    private spwanState state = spwanState.Counting;
    // Start is called before the first frame update
    void Start()
    {
        waveCountDown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == spwanState.Waiting)
        {
            if(!isEnemyAlive())
            {
                waveCompleted();
            }
            else
            {
                return;
            }
        }
        if(waveCountDown <= 0)
        {
            if (state != spwanState.Spwaning)
            {
                StartCoroutine(spwanWave(waves[nextwave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }

    }

    bool isEnemyAlive()
    {
        searchCountDown -= Time.deltaTime;
        if(searchCountDown <= 0.0f)
        {
            searchCountDown = 1f;
            if(GameObject.FindGameObjectWithTag("Enemy")==null)
            {
                return false;
            }
        }
        return true;
    }

    void waveCompleted()
    {
        Debug.Log("Wave complete");
        state = spwanState.Counting;
        waveCountDown = timeBetweenWaves;
        if(maxEnemy < 3)
        {
            maxEnemy++;
        }

        if(nextwave + 1 > waves.Length -1)
        {
            nextwave = -1;
            Debug.Log("All WAVES COMPLETED");
            for (int i = 0; i < waves.Length; i++)
            {
                waves[i].count++;
            }
        }

        nextwave++;
        waveCount++;
    }

    IEnumerator spwanWave(Wave _wave)
    {
        state = spwanState.Spwaning;
        for(int i = 0;i < _wave.count;i++)
        {
            spwanEnemy(_wave.enemy[Random.Range(0,maxEnemy)]);
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        state = spwanState.Waiting;

        yield break;
    }

    void spwanEnemy(Transform _enemy)
    {
        Transform _sp = spwanPoints[Random.Range(0, spwanPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }

    public bool releaseFireBalls()
    {
        if(waveCount == releaseCount)
        {
            releaseCount++;
            return true;
        }
        else
        {
            return false;
        }
    }
}
