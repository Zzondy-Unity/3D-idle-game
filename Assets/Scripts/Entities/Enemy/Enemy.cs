using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [field: Header("Data")]
    [field: SerializeField] public EnemySO EnemyData { get; private set; }
    [field: SerializeField] public AnimationData AnimationData { get; private set; }
    public HealthSystem HealthSystem { get; private set; }
    private int playerLv;

    [field: Header("Animation")]
    [HideInInspector] public Animator animator { get; private set; }

    [field: Header("NavMesh")]
    [HideInInspector] public NavMeshAgent Agent;
    [HideInInspector] public bool _hasNMAgent;

    [Space(10)]
    [SerializeField] public LayerMask TargetLayerMask;
    public EnemyStateMachine StateMachine;

    [field: Header("Attack")]
    public IWeapon Weapon { get; private set; }
    public GameObject WeaponObject;

    private void Awake()
    {
        _hasNMAgent = TryGetComponent(out Agent);
        animator = GetComponentInChildren<Animator>();

        HealthSystem = GetComponent<HealthSystem>();
        HealthSystem.data = EnemyData;

        AnimationData.InitNameToHash();

        StateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {
        HealthSystem.OnDeath += OnDie;

        StateMachine.ChangeState(StateMachine.ChasingState);
        Weapon = WeaponObject.GetComponent<IWeapon>();
        
        EventBus.Subscribe(EventType.LevelUp, OnPlayerLevelUp);
    }

    private void Update()
    {
        if (gameObject.activeSelf == false) return;
        StateMachine?.Update();
    }

    private void FixedUpdate()
    {
        if (gameObject.activeSelf == false) return;
        StateMachine?.PhysicsUpdate();
    }

    private void OnPlayerLevelUp()
    {
        playerLv++;
    }

    private void OnDie()
    {
        animator.SetTrigger(AnimationData.OnDieParameterHash);
        EventBus.Publish(EventType.EnemyDead);
        Debug.Log("Àû Á×À½");

        Invoke("Destroy", 2.5f);
    }

    private void Destroy()
    {
        Agent.enabled = false;

        float numOfCoin = Random.Range(1, 1 + playerLv * 5);
        for(int i = 0; i < numOfCoin; i++)
        {
            GameObject coin = GameManager.Instance.ObjectPool.GetFromPool(PoolTag.Coin);
            coin.SetActive(true);
            coin.transform.position = transform.position;
            gameObject.SetActive(false);
        }
    }
}
