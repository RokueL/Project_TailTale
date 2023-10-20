using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class LineCreate : MonoBehaviour
{
    public int oriCol;
    public int oriRow;

    int chaCol, chaRow;
    int origCol, origRow;

    int redcount;
    public int bluecount;
    int yellowcount;

    public int findBlue;

    bool isRed;
    bool isBlue;
    bool isYellow;
    public bool isCanChange;

    public bool isRedEnd;
    public bool isBlueEnd;

    Board board;
    YellowCheck yellowCheck;

    GameObject bluestack;

    List<GameObject> RedPaints = new List<GameObject>();
    List<GameObject> BluePaints = new List<GameObject>();
    List<GameObject> YellowPaints = new List<GameObject>();

    GameObject[] redlist = new GameObject[25];
    GameObject[] bluelist = new GameObject[25];
    GameObject[] yellowlist = new GameObject[25];

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        yellowCheck = FindObjectOfType<YellowCheck>();
    }

   public void disconnect()
    {
        for(int i = 0; i<board.Width; i++)
        {
            for(int j = 0; j < board.Height; j++)
            {
                if (board.allTiles[i, j].GetComponent<Tiles>().objectType == Tiles.ObejctType.End)
                {
                    disconnectCheck(i, j);
                }
            }
        }
    }

    void disconnectCheck(int col, int row)
    {
        #region LEFTRIGHT
        var obj = board.allTiles[col, row].GetComponent<Tiles>();
        if (col > 0 && col < board.Width - 1) 
        { 
            GameObject left = board.allTiles[col - 1, row];
            GameObject right = board.allTiles[col + 1 , row];
            var lefts = left.GetComponent<Tiles>();
            var rights = right.GetComponent<Tiles>();
            if (lefts.isRed || lefts.isBlue || lefts.isYellow)
            {
                if (!lefts.isConnect)
                {
                    obj.isConnect = false;
                    obj.typeAct();
                }
            }
            else if (rights.isRed || rights.isBlue || rights.isYellow)
            {
                if (!rights.isConnect)
                {
                    obj.isConnect = false;
                    obj.typeAct();
                }
            }
            else if(lefts.objectType == Tiles.ObejctType.Object || rights.objectType == Tiles.ObejctType.Object)
            {
                obj.isConnect =true;
                obj.typeAct();
            }
        }
        else if (col == 0)
        {
            GameObject right = board.allTiles[col + 1, row];
            var rights = right.GetComponent<Tiles>();
            if (rights.isRed || rights.isBlue || rights.isYellow)
            {
                if (!rights.isConnect)
                {
                    obj.isConnect = false;
                    obj.typeAct();
                }
            }
            else if (rights.objectType == Tiles.ObejctType.Object)
            {
                obj.isConnect = true;
                obj.typeAct();
            }
        }
        else if(col == board.Width - 1)
        {
            GameObject left = board.allTiles[col - 1, row];
            var lefts = left.GetComponent<Tiles>();
            if (lefts.isRed || lefts.isBlue || lefts.isYellow)
            {
                if (!lefts.isConnect)
                {
                    obj.isConnect = false;
                    obj.typeAct();
                }
            }
            else if (lefts.objectType == Tiles.ObejctType.Object)
            {
                obj.isConnect = true;
                obj.typeAct();
            }
        }
        #endregion
        if (row > 0 && row < board.Height - 1)
        {
            GameObject up = board.allTiles[col, row + 1];
            GameObject down = board.allTiles[col, row - 1];
            var ups = up.GetComponent<Tiles>();
            var downs = down.GetComponent<Tiles>();
            if (ups.isRed || ups.isBlue || ups.isYellow)
            {
                if (!ups.isConnect)
                {
                    obj.isConnect = false;
                    obj.typeAct();
                }
            }
            else if (downs.isRed || downs.isBlue || downs.isYellow)
            {
                if (!downs.isConnect)
                {
                    obj.isConnect = false;
                    obj.typeAct();
                }
            }
            else if (ups.objectType == Tiles.ObejctType.Object || downs.objectType == Tiles.ObejctType.Object)
            {
                obj.isConnect = true;
                obj.typeAct();
            }
        }
        else if (row == 0)
        {
            GameObject up = board.allTiles[col, row + 1];
            var ups = up.GetComponent<Tiles>();
            if (ups.isRed || ups.isBlue || ups.isYellow)
            {
                if (!ups.isConnect)
                {
                    obj.isConnect = false;
                    obj.typeAct();
                }
            }
            else if (ups.objectType == Tiles.ObejctType.Object)
            {
                obj.isConnect = true;
                obj.typeAct();
            }
        }
        else if (row == board.Height - 1)
        {
            GameObject down = board.allTiles[col, row - 1];
            var downs = down.GetComponent<Tiles>();
            if (downs.isRed || downs.isBlue || downs.isYellow)
            {
                if (!downs.isConnect)
                {
                    obj.isConnect = false;
                    obj.typeAct();
                }
            }
            else if (downs.objectType == Tiles.ObejctType.Object)
            {
                obj.isConnect = true;
                obj.typeAct();
            }
        }
    }


    public void BlueBack()
    {
        isBlueEnd = false;
        isCanChange = false;
        for (int i = 0; i < board.Width; i++)
        {
            for (int j = 0; j < board.Height; j++)
            {
                if (board.allTiles[i, j].GetComponent<Tiles>().objectType == Tiles.ObejctType.Object)
                {
                    if (board.allTiles[i, j].gameObject.tag == "Water")
                    {
                        chaCol = i;
                        chaRow = j;
                    }
                }
                if(board.allTiles[i, j].GetComponent<Tiles>().objectType == Tiles.ObejctType.Yet)
                {
                    origCol = i; 
                    origRow = j;
                }
            }
        }
        Debug.Log("ori " + origCol + " , " + origRow);

        Debug.Log("cha " + chaCol + " , " + chaRow);
        disconnect();
    }



    public void FindStartTypeAct()
    {
        for (int i = 0; i < board.Width; i++)
        {
            for (int j = 0; j < board.Height; j++)
            {
                if (board.allTiles[i, j].GetComponent<Tiles>().objectType == Tiles.ObejctType.Object)
                {
                        board.allTiles[i, j].GetComponent<Tiles>().typeAct();
                }
                if (board.allTiles[i,j].GetComponent<Tiles>().objectType == Tiles.ObejctType.End)
                {
                    if (board.allTiles[i, j].gameObject.tag == "Cannon" || board.allTiles[i, j].gameObject.tag == "Hose")
                    {
                       // yellowCheck.YellowRotation(i, j);
                    }
                }
            }
        }
    }

    #region RESET
    public void ResetYellow()
    {
        for (int i = 0; i < yellowlist.Length; i++)
        {
            if (yellowlist[i] != null)
            {
                yellowlist[i].GetComponent<Tiles>().isConnect = false;
                yellowlist[i] = null;
            }
        }
        yellowcount = 0;
    }


    public void ResetBlue()
    {
        for (int i = 0; i < bluelist.Length; i++)
        {
            if (bluelist[i] != null)
            {
                bluelist[i].GetComponent<Tiles>().isConnect = false;
                bluelist[i] = null;
            }
        }
        bluecount = 0;
    }


    public void ResetRed()
    {
        for(int i = 0; i< redlist.Length; i++)
        {
            if (redlist[i] != null)
            {
                redlist[i].GetComponent<Tiles>().isConnect = false;
                redlist[i] = null;
            }
        }
        redcount = 0;
    }
    #endregion

       
    public void PaintCheck(int col, int row, int typeValue, int colorValue) //typeValue 는 Object가 불인지 물인지 전기인지     colorValue는 페인트 색깔로 분류해둔것;
    {
        Debug.Log("blueCount : " +bluecount);
        if (board.allTiles[col,row].GetComponent<Tiles>().objectType == Tiles.ObejctType.Object)
        {
            oriCol = col;
            oriRow = row;
        }
        #region UPDOWN
        if (row > 0 && row < board.Height - 1)
        {
            PaintUP(col, row, typeValue, colorValue);
            PaintDown(col, row, typeValue, colorValue);

        }
        else if (row == board.Height - 1)
        {
            PaintDown(col, row, typeValue, colorValue);
        }
        else if (row == 0)
        {
            PaintUP(col, row, typeValue, colorValue);
        }
        #endregion

        #region LEFTRIGHT
        if (col > 0 && col < board.Width - 1)
        {
            PaintLeft(col, row, typeValue, colorValue);
            PaintRight(col, row, typeValue, colorValue);

        }
        else if (col == board.Width - 1)
        {
            PaintLeft(col, row, typeValue, colorValue);
        }
        else if (col == 0)
        {
            PaintRight(col, row, typeValue, colorValue);
        }

        #endregion
        if (!isRedEnd)
        {
            disconnect();
        }
        if (bluecount >= 5)
        {
            isCanChange = true;
            if (!isBlueEnd)
            {
                BlueEndCheck(col, row);
            }
        }
        else if(bluecount < 5)
        {
            if (isCanChange)
            {
                if (isBlueEnd)
                {
                    BlueBack();
                }
            }
        }
        else
        {
            disconnect();
            isCanChange = false;
            findBlue = bluecount;
        }
    }

    #region DIRECTION
    void PaintLeft(int col, int row, int typeValue, int colorValue)
    {
        GameObject left = board.allTiles[col - 1, row];
        var lefts = left.GetComponent<Tiles>();

            switch (colorValue)
            {
                case 0:
                    if (lefts.isRed)
                    {
                        if (lefts.isConnect == false)
                        {
                            redlist[redcount] = left;
                            redcount++;
                            lefts.isConnect = true;
                            PaintCheck(col - 1, row, typeValue, colorValue);
                        }
                    }
                    RedEndCheck(typeValue, left);
                    break;
                case 1:
                    if (lefts.isBlue)
                    {
                        if (lefts.isConnect == false)
                        {
                            bluelist[bluecount] = left;
                            bluecount++;
                            lefts.isConnect = true;
                            PaintCheck(col - 1, row, typeValue, colorValue);
                        }
                    }
                    break;
            }
        
    }

    void PaintRight(int col, int row, int typeValue, int colorValue)
    {
        GameObject right = board.allTiles[col + 1, row];
        var rights = right.GetComponent<Tiles>();
            switch (colorValue)
            {
                case 0:
                    if (rights.isRed)
                    {
                        if (rights.isConnect == false)
                        {
                            redlist[redcount] = right;
                            redcount++;
                            rights.isConnect = true;
                            PaintCheck(col + 1, row, typeValue, colorValue);
                        }
                    }
                    RedEndCheck(typeValue, right);
                    break;
                case 1:
                    if (rights.isBlue)
                    {
                        if (rights.isConnect == false)
                        {
                            bluelist[bluecount] = right;
                            bluecount++;
                            rights.isConnect = true;
                            PaintCheck(col + 1, row, typeValue, colorValue);
                        }
                    }
                    break;
            }
        
    }

    void PaintUP(int col, int row, int typeValue, int colorValue)
    {
        GameObject up = board.allTiles[col, row + 1];
        var ups = up.GetComponent<Tiles>();
            switch (colorValue)
            {
                case 0:
                    if (ups.isRed)
                    {
                        if (ups.isConnect == false)
                        {
                            redlist[redcount] = up;
                            redcount++;
                            ups.isConnect = true;
                            PaintCheck(col, row + 1, typeValue, colorValue);
                        }
                    }
                    RedEndCheck(typeValue, up);
                    break;
                case 1:
                    if (ups.isBlue )
                    {
                        if (ups.isConnect == false)
                        {
                            bluelist[bluecount] = up;
                            bluecount++;
                            ups.isConnect = true;
                            PaintCheck(col, row + 1, typeValue, colorValue);
                        }
                    }

                    break;
            }
        
    }

    void PaintDown(int col, int row, int typeValue, int colorValue)
    {
        GameObject down = board.allTiles[col, row - 1];
        var downs = down.GetComponent<Tiles>();

            switch (colorValue)
            {
                case 0:
                    if (downs.isRed)
                    {
                        if (downs.isConnect == false)
                        {
                            redlist[redcount] = down;
                            redcount++;
                            downs.isConnect = true;
                            PaintCheck(col, row - 1, typeValue, colorValue);
                        }
                    }
                    RedEndCheck(typeValue, down);
                    break;
                case 1:
                    if (downs.isBlue)
                    {
                        if (downs.isConnect == false)
                        {
                            bluelist[bluecount] = down;
                            bluecount++;
                            downs.isConnect = true;
                            PaintCheck(col, row - 1, typeValue, colorValue);
                        }
                    }
                    break;

            }
        
    }
    #endregion

    #region ENDCHECK
    void RedEndCheck(int value, GameObject objects)
    {
        var obj = objects.GetComponent<Tiles>();
        switch (value)
        {
            case 0: // 배터리 일때
                if (obj.objectType == Tiles.ObejctType.End)
                {
                    if (obj.gameObject.tag == "ScreenDoor" || obj.gameObject.tag == "Block" || obj.gameObject.tag == "Cannon")
                    {
                        isRedEnd = true;
                        obj.isConnect = true;
                        obj.typeAct();
                    }
                }
                break;
            case 1: // 물 일때
                if (obj.objectType == Tiles.ObejctType.End)
                {
                    if (obj.gameObject.tag == "Hose")
                    {
                        isRedEnd = true;
                        obj.isConnect = true;
                        obj.typeAct();
                    }
                }
                break;
            case 2: // 불 일때
                if (obj.objectType == Tiles.ObejctType.End)
                {
                    if (obj.gameObject.tag == "Cannon")
                    {
                        isRedEnd = true;
                        obj.isConnect = true;
                        obj.typeAct();
                    }
                }
                break;
        }
    }

    void BlueEndCheck(int col, int row)
    {
        isBlueEnd = true;
        var obj = board.allTiles[col, row].GetComponent<Tiles>();

        Debug.Log("ori " + oriCol);
        Debug.Log("ori " + oriRow);
    }


    #endregion


    // Update is called once per frame
    void Update()
    {
    }
}
