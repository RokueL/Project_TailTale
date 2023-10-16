using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    int defaultCapacity = 100;
    int maxPoolSize = 150;

    public GameObject paintPrefabs;

    public IObjectPool<GameObject> PaintPool { get; private set; }


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
        PaintPool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);


        //=================< �ʱ� �̸� ����         >=====================
        for (int i = 0; i < defaultCapacity; i++)
        {
            PaintDraw paint = CreatePooledItem().GetComponent<PaintDraw>();
            paint.paintPool.Release(paint.gameObject);
        }
    }

    //=================< ����         >=====================
    private GameObject CreatePooledItem()
    {
        GameObject poolGO = Instantiate(paintPrefabs);
        poolGO.GetComponent<PaintDraw>().paintPool = this.PaintPool;
        return poolGO;
    }
    // =================< ��������         >=====================
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
        poolGo.GetComponent<PaintDraw>().DestroySelf();
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