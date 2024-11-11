using System;
using UnityEngine;

public class PlayerChasingState : PlayerGroundState
{
    public PlayerChasingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.RunSpeedModifier = 2f;
        base.Enter();
    }

    public override void Exit()
    {
        stateMachine.RunSpeedModifier = 1f;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        Chase();
    }

    //함수제작
    private void Chase()
    {
        RaycastHit[] hits_detect;
        RaycastHit[] hits_attack;
        Player player = stateMachine.Player;

        hits_detect = Physics.SphereCastAll(player.transform.position, player.PlayerData.GroundData.DetectDistance, Vector3.up, 0f, TargetLayerMask);

        //탐지 사정거리 확인
        if (hits_detect == null)
        {
            //사정거리안에 몬스터가 없어짐
            stateMachine.ChangeState(stateMachine.AutoMoveState);
            return;
        }
        else
        {
            //공격 사정거리 확인
            hits_attack = Physics.SphereCastAll(player.transform.position, player.Weapon.GetWeaponRange(), Vector3.up, 0f, TargetLayerMask);
            if (hits_attack != null)
            {
                //있으면 공격
                stateMachine.ChangeState(stateMachine.AttackState);
                Debug.Log("공격상태로 전환");
                return;
            }
            else
            {
                float minDistance = float.MaxValue;
                Vector3 target = Vector3.zero;

                //없으면 가장 가까운 몬스터 타겟으로 => 업데이트문에서 해도 되나?
                for(int i = 0; i < hits_attack.Length; i++)
                {
                    if((player.transform.position - hits_attack[i].transform.position).sqrMagnitude < minDistance)
                    {
                        minDistance = player.transform.position.sqrMagnitude;
                        target = hits_attack[i].transform.position;
                    }
                }

                player.Agent.SetDestination(target);
            }
        }
    }
}