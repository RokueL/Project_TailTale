using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;
using UnityEngine;

public class RedCheck : MonoBehaviour
{
    Board board;


    GameObject[] redList = new GameObject[25];
    GameObject[] resetList = new GameObject[100];
    int redcount;
    bool isRedEnd;


    //public void ResetRedAll()
    //{
    //    for (int i = 0; i < board.Width; i++)
    //    {
    //        for (int j = 0; j < board.Height; j++)
    //        {
    //            var a = board.allTiles[i, j].GetComponent<Tiles>();
    //            if (a.isRed) 
    //            {
    //                if (matchRed(a.gameObject))
    //                {
    //                    Debug.Log("name : " + a + " true");
    //                    a.isConnect = true;
    //                }
    //                if (!matchRed(a.gameObject))
    //                {
    //                    Debug.Log("name : " + a + " false");
    //                    a.isConnect = false;
    //                }
    //            }
    //        }
    //    }
    //    resetlist();
    //    DisconnectFindEnds();
    //}

    //void resetlist()
    //{
    //    Debug.Log("ResetList");
    //    for(int i = 0; i< resetList.Length; i++)
    //    {
    //        if (resetList[i] != null)
    //        {
    //            resetList[i] = null;
    //        }
    //    }
    //}

    //bool matchRed(GameObject obj)
    //{
    //    for (int i = 0; i < resetList.Length; i++)
    //    {
    //        if (resetList[i] == obj)
    //        {
    //            return true; 
    //        }
    //    }
    //    return false;
    //}

    public void AllReset()
    {
        for (int i = 0; i < board.Width; i++)
        {
            for (int j = 0; j < board.Height; j++)
            {
                var a = board.allTiles[i, j].GetComponent<Tiles>();
                if (a.isRed)
                {
                    a.isConnect = false;
                }
            }
        }
    }

    public void ResetRed()
    {
        for (int i = 0; i < redList.Length; i++)
        {
            if (redList[i] != null)
            {
                redList[i].GetComponent<Tiles>().isConnect = false;
                redList[i] = null;
            }
        }
        redcount = 0;
    }

    

    public void RedCheckStart(int col, int row, int typeValue) 
    {
        //AllReset();
        //ResetRed();
        RedChecking(col, row, typeValue);
    }

    void RedChecking(int col, int row, int typeValue ) // 내 타입 가져오는 거 (오브젝트에서 호출되는 거니깐 불인지 전기인지 등등
    {
        if (row > 0 && row < board.Height-1) // 배터리 0 불 1 물 2
        {
            RedUp(col, row, typeValue);
            RedDown(col, row, typeValue);
        }
        else if (row == 0)
        {
            RedUp(col, row, typeValue);
        }
        else if (row == board.Height - 1)
        {
            RedDown(col, row, typeValue);
        }


        if (col > 0 && col < board.Width - 1)
        {
            RedRight(col, row, typeValue);
            RedLeft(col, row, typeValue);
        }
        else if (col == 0)
        {
            RedRight(col, row, typeValue);
        }
        else if (col == board.Width - 1)
        {
            RedLeft(col, row, typeValue);
        }

    }


    #region DIRECTION
    void RedUp(int col, int row, int typeValue)
    {
        var a = board.allTiles[col, row + 1].GetComponent<Tiles>();
        if (a.isRed)
        {
            if (!a.isConnect)
            {
                //redList[redcount] = a.gameObject;
                //redcount++;
                a.isConnect = true;
                RedChecking(col, row + 1, typeValue);
            }
        }
        else if(a.objectType == Tiles.ObejctType.End) // 배터리 0 불 1 물 2
        {
            isRedEnd = true;
            RedEndCheck(typeValue, a.gameObject);
        }
    }
    void RedDown(int col, int row, int typeValue)
    {
        var a = board.allTiles[col, row - 1].GetComponent<Tiles>();
        if (a.isRed)
        {
            if (!a.isConnect)
            {
                //redList[redcount] = a.gameObject;
                //redcount++;
                a.isConnect = true;
                RedChecking(col, row - 1, typeValue);
            }
        }
        else if (a.objectType == Tiles.ObejctType.End) // 배터리 0 불 1 물 2
        {
            isRedEnd = true;
            RedEndCheck(typeValue, a.gameObject);
        }
    }
    void RedRight(int col, int row, int typeValue)
    {
        var a = board.allTiles[col + 1, row].GetComponent<Tiles>();

        if (a.isRed)
        {
            if (!a.isConnect)
            {
               // redList[redcount] = a.gameObject;
                //redcount++;
                a.isConnect = true;
                RedChecking(col + 1, row, typeValue);
            }
        }
        else if (a.objectType == Tiles.ObejctType.End) // 배터리 0 불 1 물 2
        {
            isRedEnd = true;
            RedEndCheck(typeValue, a.gameObject);
        }
    }
    void RedLeft(int col, int row, int typeValue)
    {
        var a = board.allTiles[col - 1, row].GetComponent<Tiles>();
        if (a.isRed)
        {
            if (!a.isConnect)
            {
                //redList[redcount] = a.gameObject;
                //redcount++;
                a.isConnect = true;
                RedChecking(col - 1, row, typeValue);
            }
        }
        else if (a.objectType == Tiles.ObejctType.End) // 배터리 0 불 1 물 2
        {
            isRedEnd = true;
            RedEndCheck(typeValue, a.gameObject);
        }
    }
    #endregion

    void RedEndCheck(int typeValue, GameObject objects)  // 내 타입 가져와서 넣는 거
    {
        var obj = objects.GetComponent<Tiles>();
        switch (typeValue)
        {
            case 0: // 배터리 일때
                if (obj.gameObject.tag == "ScreenDoor" || obj.gameObject.tag == "Block" || obj.gameObject.tag == "Cannon")
                {
                    isRedEnd = true;
                    obj.isConnect = true;
                    obj.typeAct();
                }
                break;
            case 1: // 불 일때
                if (obj.gameObject.tag == "Cannon")
                {
                    isRedEnd = true;
                    obj.isConnect = true;
                    obj.typeAct();
                }
                break;
            case 2: // 물 일때
                if (obj.gameObject.tag == "Hose")
                {
                    isRedEnd = true;
                    obj.isConnect = true;
                    obj.typeAct();
                }
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
