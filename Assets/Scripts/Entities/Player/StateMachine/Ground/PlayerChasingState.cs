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

    //�Լ�����
    private void Chase()
    {
        RaycastHit[] hits_detect;
        RaycastHit[] hits_attack;
        Player player = stateMachine.Player;

        hits_detect = Physics.SphereCastAll(player.transform.position, player.PlayerData.GroundData.DetectDistance, Vector3.up, 0f, TargetLayerMask);

        //Ž�� �����Ÿ� Ȯ��
        if (hits_detect == null)
        {
            //�����Ÿ��ȿ� ���Ͱ� ������
            stateMachine.ChangeState(stateMachine.AutoMoveState);
            return;
        }
        else
        {
            //���� �����Ÿ� Ȯ��
            hits_attack = Physics.SphereCastAll(player.transform.position, player.Weapon.GetWeaponRange(), Vector3.up, 0f, TargetLayerMask);
            if (hits_attack != null)
            {
                //������ ����
                stateMachine.ChangeState(stateMachine.AttackState);
                Debug.Log("���ݻ��·� ��ȯ");
                return;
            }
            else
            {
                float minDistance = float.MaxValue;
                Vector3 target = Vector3.zero;

                //������ ���� ����� ���� Ÿ������ => ������Ʈ������ �ص� �ǳ�?
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