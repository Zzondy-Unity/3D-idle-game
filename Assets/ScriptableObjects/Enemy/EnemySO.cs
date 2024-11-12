using UnityEngine;

[CreateAssetMenu (fileName = "DefaultEnemySO", menuName = "Character/Enemy/Default")]
public class EnemySO : EntitySO
{
    [field: SerializeField][field: Range(0f, 25f)] public float WalkSpeed { get; private set; } = 5f;
    [field: SerializeField][field: Range(0f, 25f)] public float RunSpeed { get; private set; } = 2f;
    [field: SerializeField][field: Range(0.1f, 25f)] public float AttackDistance { get; private set; } = 3f;
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float AttackDelay { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; }


    public float Speed
    {
        get { return WalkSpeed * RunSpeed; }
    }

}