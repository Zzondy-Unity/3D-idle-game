using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu (fileName = "PlayerSO", menuName = "Character/Player")]
public class PlayerSO : EntitySO
{
    [field: SerializeField] public PlayerGroundData GroundData { get; private set; }
    [field: SerializeField] public PlayerAttackData AttackData { get; private set; }
}

[Serializable]
public class PlayerGroundData
{
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; } = 1f;
    [field: SerializeField][field: Range(0f, 2f)] public float WalkSpeedModifier { get; private set; } = 0.225f;
    [field: SerializeField][field: Range(0f, 2f)] public float RunSpeedModifier { get; private set; } = 1f;
    [field: SerializeField][field: Range(0f, 25f)] public float JumpForce { get; private set; } = 5f;
    [field: SerializeField][field: Range(0.1f, 25f)] public float DetectDistance { get; private set; } = 20f;
    [field: SerializeField][field: Range(0.1f, 1f)] public float DetectTerm { get; private set; } = 0.5f;

    public float Speed
    {
        get { return BaseSpeed * WalkSpeedModifier * RunSpeedModifier; }
    }

}

[Serializable]
public class PlayerAttackData
{
    [field: SerializeField] public string AttackName { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; }

}
