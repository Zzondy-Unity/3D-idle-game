using UnityEngine;

[CreateAssetMenu(fileName = "Entity", menuName = ("Character"))]
public class EntitySO : ScriptableObject
{
    [field: SerializeField] public float MaxHP { get; private set; } = 100f;
    [field: SerializeField] public float MaxMP { get; private set; } = 100f;
}