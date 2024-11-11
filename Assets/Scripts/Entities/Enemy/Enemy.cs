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
    public PlayerStateMachine StateMachine;

    private void Awake()
    {
        _hasNMAgent = TryGetComponent(out Agent);
        animator = GetComponentInChildren<Animator>();

        HealthSystem = GetComponent<HealthSystem>();
        HealthSystem.data = EnemyData;

        AnimationData.InitNameToHash();

        //StateMachine = new PlayerStateMachine(this);
    }

}
