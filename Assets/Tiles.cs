using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Tiles : MonoBehaviour
{
    public enum ObejctType
    {
        None,
        Object,
        Paint,
        End,
        Wall
    }

    public ObejctType objectType;

    public bool isRed;
    public bool isBlue;
    public bool isYellow;

    public bool isConnect;
    public bool isObjectConnect;

    public int col, row;

    LineCreate lineCreate;

    public IObjectPool<GameObject> tilePool { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        lineCreate =  FindObjectOfType<LineCreate>();
        if(objectType == ObejctType.Object)
        {
            isConnect = true;
        }
    }

    void LightOn()
    {
        GetComponent<SpriteRenderer>().color = Color.grey;
        Debug.Log("ConnectRed");
    }

    public void typeAct()
    {
        switch (objectType)
        {
            case ObejctType.Object:
                lineCreate.RedCheck(col, row);
                break;
            case ObejctType.Paint:
                break;
            case ObejctType.End:
                if (isConnect)
                {
                    LightOn();
                    break;
                }
                break;
            case ObejctType.Wall:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
