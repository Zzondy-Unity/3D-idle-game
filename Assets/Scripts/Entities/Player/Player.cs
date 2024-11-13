using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IWeapon
{
    public void Attack();
    public float GetWeaponRange();
    public void ToggleWeaponCollider(bool state);
    public WeaponSO WeaponData { get; }
}

public class Player : MonoBehaviour
{
    [field: Header("Data")]
    [field: SerializeField] public PlayerSO PlayerData {  get; private set; }
    [field: SerializeField] public AnimationData AnimationData {  get; private set; }
    public HealthSystem HealthSystem { get; private set; }
    public PlayerCondition condition { get; private set; }

    [field: Header("Control")]
    [HideInInspector] public PlayerController Input { get; private set; }
    [HideInInspector] public CharacterController controller { get; private set; }

    [field: Header("Animation")]
    [HideInInspector] public Animator animator { get; private set; }

    [field: Header("NavMesh")]
    [HideInInspector] public NavMeshAgent Agent;
    [HideInInspector] public bool _hasNMAgent;

    [Space(10)]
    [SerializeField] public LayerMask TargetLayerMask;
    [SerializeField] public Transform nextStagePoint;

    public PlayerStateMachine StateMachine;

    [Header("Item")]
    public  Action<ItemSO> AddItem;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        _hasNMAgent = TryGetComponent(out Agent);
        animator = GetComponentInChildren<Animator>();
        condition = GetComponent<PlayerCondition>();

        Input = GetComponent<PlayerController>();
        controller = GetComponent<CharacterController>();
        HealthSystem = GetComponent<HealthSystem>();
        HealthSystem.data = PlayerData;

        AnimationData.InitNameToHash();

        StateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        HealthSystem.OnDeath += OnDie;
        StateMachine.ChangeState(StateMachine.IdleState);
    }

    private void OnDie()
    {
        HealthSystem.ChangeHealth(HealthSystem.MaxHealth);
        transform.position = Vector3.zero;
        Debug.Log("PlayerDead");
    }

    private void Update()
    {
        StateMachine?.HandleInput();
        StateMachine?.Update();
    }

    private void FixedUpdate()
    {
        StateMachine?.PhysicsUpdate();
    }
}
