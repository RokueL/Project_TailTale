using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    int defaultCapacity = 100;
    int maxPoolSize = 150;

    public GameObject tilePrefabs;

    public IObjectPool<GameObject> tilePool { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        Init();
    }

    void Init()
    {
        //=================< 풀링 선언         >=====================
        tilePool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);


        //=================< 초기 미리 생성         >=====================
        for (int i = 0; i < defaultCapacity; i++)
        {
            Tiles tile = CreatePooledItem().GetComponent<Tiles>();
            tile.tilePool.Release(tile.gameObject);
        }
    }

    //=================< 생성         >=====================
    private GameObject CreatePooledItem()
    {
        GameObject poolGO = Instantiate(tilePrefabs);
        poolGO.GetComponent<Tiles>().tilePool = this.tilePool;
        return poolGO;
    }
    // =================< 가져오기         >=====================
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }


    // =================< 반환         >=====================
    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.SetActive(false);
    }


    // =================< 삭제         >=====================
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }


}