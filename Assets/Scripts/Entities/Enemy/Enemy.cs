using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public class Enemy : MonoBehaviour
{
    [field: Header("Data")]
    [field: SerializeField] public EnemySO EnemyData { get; private set; }
    [field: SerializeField] public AnimationData AnimationData { get; private set; }
    public HealthSystem HealthSystem { get; private set; }

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
    }

    private void Update()
    {
        StateMachine?.Update();
    }

    private void FixedUpdate()
    {
        StateMachine?.PhysicsUpdate();
    }

    private void OnDie()
    {
        animator.SetTrigger(AnimationData.OnDieParameterHash);
        Debug.Log("적 죽음");
        Invoke("Destroy", 2.5f);
    }

    private void Destroy()
    {
        //오브젝트 풀로 이동 + 재화뿌림
        Destroy(gameObject);
    }
}
