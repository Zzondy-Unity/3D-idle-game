using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRadius = 20f;
    public float minsDistanceFromPlayer = 10f;
    public int maxAttempts = 10;

    private Transform Player;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 2f, 2f);
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition;
        Player = CharacterManager.Instance.Player.transform;

        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomPos = Random.insideUnitSphere * spawnRadius;

            if (NavMesh.SamplePosition(Player.position + randomPos, out NavMeshHit hit, spawnRadius, NavMesh.AllAreas))
            {
                spawnPosition = hit.position;

                if (Vector3.Distance(spawnPosition, Player.position) >= minsDistanceFromPlayer)
                {
                    int randomMonsterIndex = Random.Range(0, (int)PoolTag.MonsterCount);
                    GameObject newMonster = GameManager.Instance.ObjectPool.GetFromPool((PoolTag)randomMonsterIndex);
                    newMonster.transform.position = spawnPosition;
                    newMonster.GetComponent<HealthSystem>().ChangeHealth(newMonster.GetComponent<HealthSystem>().MaxHealth);
                    newMonster.SetActive(true);

                    NavMeshAgent agent = newMonster.GetComponent<NavMeshAgent>();
                    agent.enabled = true;
                    if (agent.isOnNavMesh) return;
                }
            }
        }
    }

    private void DifficultyByStage()
    {
        //위의 오브젝트 풀에서 꺼낸거지만
        //
    }
}
