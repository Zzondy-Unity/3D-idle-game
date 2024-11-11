using UnityEngine;

[CreateAssetMenu (fileName = "DefaultEnemySO", menuName = "Character/Enemy/Default")]
public class EnemySO : EntitySO
{
    [field: SerializeField][field: Range(0f, 25f)] public float WalkSpeed { get; private set; } = 5f;
    [field: SerializeField][field: Range(0f, 25f)] public float RunSpeed { get; private set; } = 2f;
    [field: SerializeField][field: Range(0.1f, 25f)] public float DetectDistance { get; private set; } = 15f;
    [field: SerializeField][field: Range(0.1f, 25f)] public float AttackDistance { get; private set; } = 3f;
    [field: SerializeField][field: Range(0.1f, 1f)] public float DetectTerm { get; private set; } = 0.5f;
    [field: SerializeField] public float Damage { get; private set; }

    public float Speed
    {
        get { return WalkSpeed * RunSpeed; }
    }

}