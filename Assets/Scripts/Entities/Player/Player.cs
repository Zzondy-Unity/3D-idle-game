using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IWeapon
{
    public float GetWeaponRange();
}

public class Player : MonoBehaviour
{
    [field: Header("Data")]
    [field: SerializeField] public PlayerSO PlayerData {  get; private set; }
    [field: SerializeField] public AnimationData AnimationData {  get; private set; }

    [field: Header("Control")]
    [HideInInspector] public PlayerController Input { get; private set; }
    [HideInInspector] public CharacterController controller { get; private set; }

    [field: Header("Animation")]
    [HideInInspector] public Animator animator;
    [HideInInspector] public bool _hasAnimator;

    [field: Header("NavMesh")]
    [HideInInspector] public NavMeshAgent Agent;
    [HideInInspector] public bool _hasNMAgent;

    [Space(10)]
    [SerializeField] public LayerMask TargetLayerMask;
    [SerializeField] public Transform nextStagePoint;

    public PlayerStateMachine StateMachine;

    
    public IWeapon Weapon {  get; private set; }

    public void SetWeapon(IWeapon weapon)
    {
        this.Weapon = weapon;
    }

    private void Awake()
    {
        AnimationData.InitNameToHash();

        _hasAnimator = TryGetComponent(out animator);
        _hasNMAgent = TryGetComponent(out Agent);

        Input = GetComponent<PlayerController>();
        controller = GetComponent<CharacterController>();

        StateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        StateMachine.ChangeState(StateMachine.IdleState);

        Weapon = GameObject.FindGameObjectWithTag("WeaponTest").GetComponent<IWeapon>();
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
