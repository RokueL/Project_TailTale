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
        //=================< 풀링 선언         >=====================
        PaintPool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);


        //=================< 초기 미리 생성         >=====================
        for (int i = 0; i < defaultCapacity; i++)
        {
            PaintDraw paint = CreatePooledItem().GetComponent<PaintDraw>();
            paint.paintPool.Release(paint.gameObject);
        }
    }

    //=================< 생성         >=====================
    private GameObject CreatePooledItem()
    {
        GameObject poolGO = Instantiate(paintPrefabs);
        poolGO.GetComponent<PaintDraw>().paintPool = this.PaintPool;
        return poolGO;
    }
    // =================< 가져오기         >=====================
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
        poolGo.GetComponent<PaintDraw>().DestroySelf();
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