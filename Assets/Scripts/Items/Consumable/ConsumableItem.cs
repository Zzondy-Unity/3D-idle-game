using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumableItem : MonoBehaviour
{
    public PotionSO potionData;
    public abstract void Consume();
}
