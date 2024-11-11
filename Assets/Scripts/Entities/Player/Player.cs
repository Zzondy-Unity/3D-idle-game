using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private bool _hasAnimator;

    [field: Header("NavMesh")]
    [HideInInspector] public NavMeshAgent Agent;
    private bool _hasNMAgent;
    [Space(10)]
    [SerializeField] public LayerMask TargetLayerMask;
    [SerializeField] public Transform nextStagePoint;

    public PlayerStateMachine StateMachine;

    private void Awake()
    {
        AnimationData.InitNameToHash();

        _hasAnimator = TryGetComponent(out animator);
        _hasNMAgent = TryGetComponent(out Agent);

        Input = GetComponent<PlayerController>();
        controller = GetComponent<CharacterController>();

        StateMachine = new PlayerStateMachine(this);
    }
}
