using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnimationData
{
    [SerializeField] private string speedParameterName = "Speed";
    [SerializeField] private string moveParameterName = "Move";
    [SerializeField] private string jumpParameterName = "Jump";
    [SerializeField] private string interactionParameterName = "Interaction";
    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string comboAttackParameterName = "ComboAttack";

    public int SpeedParameterHash { get; private set; }
    public int MoveParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public int InteractionParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int ComboAttackParameterHash { get; private set; }

    public void InitNameToHash()
    {
        SpeedParameterHash = Animator.StringToHash(speedParameterName);
        MoveParameterHash = Animator.StringToHash(moveParameterName);
        JumpParameterHash = Animator.StringToHash(jumpParameterName);
        InteractionParameterHash = Animator.StringToHash(interactionParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        ComboAttackParameterHash = Animator.StringToHash(comboAttackParameterName);
    }
}
