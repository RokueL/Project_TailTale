using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    int defaultCapacity = 100;
    int maxPoolSize = 150;

    public GameObject Prefabs;

    public IObjectPool<GameObject> Pool { get; private set; }


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
        //=================< Ǯ�� ����         >=====================
        Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);


        //=================< �ʱ� �̸� ����         >=====================
        for (int i = 0; i < defaultCapacity; i++)
        {

        }
    }

    //=================< ����         >=====================
    private GameObject CreatePooledItem()
    {
        GameObject poolGO = Instantiate(Prefabs);
        //poolGO.GetComponent<>().bgPool = this.Pool;

        return poolGO;
    }
    // =================< ��������         >=====================
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }


    // =================< ��ȯ         >=====================
    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.SetActive(false);
    }


    // =================< ����         >=====================
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }


}