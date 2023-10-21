using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;
using UnityEngine;

public class RedCheck : MonoBehaviour
{
    Board board;

    bool isNotDisconnect;

    GameObject[] redList = new GameObject[25];
    GameObject[] resetList = new GameObject[100];
    int redcount;
    bool isRedEnd;


    void ResetRedAll()
    {
        for (int i = 0; i < board.Width; i++)
        {
            for (int j = 0; j < board.Height; j++)
            {
                var a = board.allTiles[i, j].GetComponent<Tiles>();
                if (a.isRed) 
                {
                    if (matchRed(a.gameObject))
                    {
                        a.isConnect = true;
                    }
                    if (!matchRed(a.gameObject))
                    {
                        a.isConnect = false;
                    }
                }
            }
        }
    }

    bool matchRed(GameObject obj)
    {
        for(int i = 0; i < resetList.Length; i++)
        {
            if (resetList[i] == obj)
            { return true; }
        }
        return false;
    }
    IEnumerator ResetRed()
    {
        yield return new WaitForSeconds(.1f);
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

    public void DisconnectFindEnds()
    {
        for (int i = 0; i < board.Width; i++)
        {
            for (int j = 0; j < board.Height; j++)
            {
                var a = board.allTiles[i, j].GetComponent<Tiles>();
                if (a.objectType == Tiles.ObejctType.End)
                {
                    if (a.isConnect)
                    {
                        DisconnectEnds(i, j);
                    }
                }
            }
        }
    }

    void DisconnectEnds(int col, int row)
    {
        var a = board.allTiles[col, row].GetComponent<Tiles>();

        if (row > 0 && row < board.Height - 1)
        {
            GameObject up = board.allTiles[col, row + 1];
            GameObject down = board.allTiles[col, row - 1];
            var ups = up.GetComponent<Tiles>();
            var downs = down.GetComponent<Tiles>();
            if (ups.isRed)
            {
                if (ups.isConnect)
                {
                    isNotDisconnect = true;
                }
            }
            else if (downs.isRed)
            {
                if (downs.isConnect)
                {
                    isNotDisconnect = true;
                }
            }
            if (ups.objectType == Tiles.ObejctType.Object || downs.objectType == Tiles.ObejctType.Object)
            {
                isNotDisconnect = true;
            }
        }
        else if (row == 0)
        {
            GameObject up = board.allTiles[col, row + 1];
            var ups = up.GetComponent<Tiles>();
            if (ups.isRed)
            {
                if (ups.isConnect)
                {
                    isNotDisconnect = true;
                }
            }
            else if (ups.objectType == Tiles.ObejctType.Object)
            {
                isNotDisconnect = true;
            }
        }

        else if (row == board.Height - 1)
        {
            GameObject down = board.allTiles[col, row - 1];
            var downs = down.GetComponent<Tiles>();
            if (downs.isRed)
            {
                if (downs.isConnect)
                {
                    isNotDisconnect = true;
                }
            }
            else if (downs.objectType == Tiles.ObejctType.Object)
            {
                isNotDisconnect = true;
            }
        }
        ////////////////////////////////////////////
        if (col > 0 && col < board.Width - 1)
        {
            GameObject left = board.allTiles[col - 1, row];
            GameObject right = board.allTiles[col + 1, row];
            var lefts = left.GetComponent<Tiles>();
            var rights = right.GetComponent<Tiles>();
            if (lefts.isRed)
            {
                if (lefts.isConnect)
                {
                    isNotDisconnect = true;
                }
            }
            else if (rights.isRed)
            {
                if (rights.isConnect)
                {
                    isNotDisconnect = true;
                }
            }
            if (lefts.objectType == Tiles.ObejctType.Object || rights.objectType == Tiles.ObejctType.Object)
            {
                isNotDisconnect = true;
            }
        }
        else if (col == 0)
        {
            GameObject right = board.allTiles[col + 1, row];
            var rights = right.GetComponent<Tiles>();
            if (rights.isRed)
            {
                if (rights.isConnect)
                {
                    isNotDisconnect = true;
                }
            }
            if (rights.objectType == Tiles.ObejctType.Object)
            {
                isNotDisconnect = true;
            }
        }
        else if (col == board.Width - 1)
        {
            GameObject left = board.allTiles[col - 1, row];
            var lefts = left.GetComponent<Tiles>();
            if (lefts.isRed)
            {
                if (lefts.isConnect)
                {
                    isNotDisconnect = true;
                }
            }
            else if (lefts.objectType == Tiles.ObejctType.Object)
            {
                isNotDisconnect = true;
            }
        }

        if(!isNotDisconnect)
        {
            a.isConnect = false;
            a.typeAct();
        }
    }

    public void RedCheckStart(int col, int row, int typeValue) 
    {
        StartCoroutine(ResetRed());
        RedChecking(col, row, typeValue);
    }

    void RedChecking(int col, int row, int typeValue ) // 내 타입 가져오는 거 (오브젝트에서 호출되는 거니깐 불인지 전기인지 등등
    {
        DisconnectFindEnds();
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

        //if(!isRedEnd)
        //{
        //    ResetRed();
        //}
    }


    #region DIRECTION
    void RedUp(int col, int row, int typeValue)
    {
        var a = board.allTiles[col, row + 1].GetComponent<Tiles>();
        if (a.isRed)
        {
            if (!a.isConnect)
            {
                redList[redcount] = a.gameObject;
                //resetList[redcount] = a.gameObject;
                redcount++;
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
                redList[redcount] = a.gameObject;
                //resetList[redcount] = a.gameObject;
                redcount++;
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
                redList[redcount] = a.gameObject;
                redcount++;
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
                redList[redcount] = a.gameObject;
                redcount++;
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
