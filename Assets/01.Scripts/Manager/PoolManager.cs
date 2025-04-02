using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;

    [SerializeField] private Queue<GameObject>[] pools;

    int defaultSpawnCount = 20; // 초기 생성값

    private void Awake()
    {
        pools = new Queue<GameObject>[prefabs.Length];

        for(int i = 0; i < pools.Length; i++)
        {
            pools[i] = new Queue<GameObject>();
            for(int j = 0; j < defaultSpawnCount; j ++)
            {
                GameObject go = Instantiate(prefabs[i]);
                go.SetActive(false);

                pools[i].Enqueue(go);
            }
        }
    }
    
    public GameObject Get(int index)
    {
        GameObject select = null;

        if (pools[index].Count > 0)
        {
            select = pools[index].Dequeue();
            select.SetActive(true);
        }
        else
        {
            select = Instantiate(prefabs[index]);
            pools[index].Enqueue(select);
        }

        return select;
    }

    public void BulletReturn(int index, GameObject bullet)
    {
        bullet.SetActive(false);   
        pools[index].Enqueue(bullet);
    }
}
