using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PoolTag
{
    Skeleton_Minion,
    Skeleton_Mage,
    Coin
}

[System.Serializable]
public class Pool
{
    public GameObject Prefab;
    public int size;
    public PoolTag tag;
}

public class ObjectPool : MonoBehaviour
{
    public List<Pool> Pools;
    public Dictionary<PoolTag, Queue<GameObject>> PoolDictionary;

    private void Awake()
    {
        GameManager.Instance.ObjectPool = this;
        Init();
    }

    public void Init()
    {
        PoolDictionary = new Dictionary<PoolTag, Queue<GameObject>>();
        foreach (var pool in Pools)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.Prefab, this.transform);
                obj.SetActive(false);
                objPool.Enqueue(obj);
            }
            PoolDictionary.Add(pool.tag, objPool);
        }
    }

    public GameObject GetFromPool(PoolTag tag)
    {
        if (!PoolDictionary.ContainsKey(tag)) return null;

        if (PoolDictionary[tag].All(x => x.activeSelf == true))
        {
            Pool pool = Pools.Find(x => x.tag == tag);
            GameObject newObj = Instantiate(pool.Prefab, this.transform);
            newObj.SetActive(false);
            PoolDictionary[tag].Enqueue(newObj);
            return newObj;
        }
        if (PoolDictionary[tag].TryDequeue(out GameObject obj))
        {
            obj.SetActive(true);
            PoolDictionary[tag].Enqueue(obj);
            return obj;
        }

        return null;
    }
}
