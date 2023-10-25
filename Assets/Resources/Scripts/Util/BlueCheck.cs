using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Tiles;

public class BlueCheck : MonoBehaviour
{
    Board board;
    RedCheck redCheck;
    LineChecker lineChecker;

    Sprite def;

    int oriCol, oriRow;
    int c, r;

    GameObject[] blueList = new GameObject[25];
    int bluecount;
    bool isBlueEnd;

    public void BlueCheckStart(int col, int row, int typeValue, int oriC, int oriR)
    {
        ResetBlue();
        BlueChecking(col, row, typeValue, oriC, oriR);
    }

    public void ResetBlue()
    {
        for(int i = 0; i< blueList.Length; i++)
        {
            if (blueList[i] != null)
            {
                blueList[i].GetComponent<Tiles>().isCanMove = false;
                blueList[i] = null;
            }
        }
        bluecount = 0;
    }

    void BlueChecking(int col, int row, int typeValue, int oriC, int oriR) // 내 타입 가져오는 거 (오브젝트에서 호출되는 거니깐 불인지 전기인지 등등
    {
        if(row > 0 && row < board.Height-1)
        {
            BlueUp(col, row, typeValue, oriC, oriR);
            BlueDown(col, row, typeValue, oriC, oriR);
        }
        else if(row == 0)
        {
            BlueUp(col, row, typeValue, oriC, oriR);
        }
        else if(row == board.Height-1)
        {
            Debug.Log("Check -> Down");
            BlueDown(col, row, typeValue, oriC, oriR);
        }


        if (col > 0 && col < board.Width - 1)
        {
            BlueRight(col, row, typeValue, oriC, oriR);
            BlueLeft(col, row, typeValue, oriC, oriR);
        }
        else if(col == 0)
        {
            BlueRight(col, row, typeValue, oriC, oriR);
        }
        else if(col == board.Width - 1)
        {
            BlueLeft(col, row, typeValue, oriC, oriR);
        }

        if (bluecount == 5)
        {
            if (!isBlueEnd)
            {
                if (!board.allTiles[oriC, oriR].GetComponent<Tiles>().isConnect)
                {
                    Debug.Log("5개 이어짐");
                    isBlueEnd = true;
                    StartCoroutine(BlueChange(col, row, typeValue, oriC, oriR));
                    return;
                }
            }
        }
        if (bluecount < 5)
        {
            Debug.Log("아입니더");
            if (board.allTiles[oriC, oriR].GetComponent<Tiles>().isMoved)
            {
                if (isBlueEnd)
                {
                    Debug.Log("돌아가욧");
                    isBlueEnd = false;
                    StartCoroutine(BlueChangeBack());
                }
            }
        }

    }

    void waterClear()
    {
        for(int i = 0; i< board.Width; i++)
        {
            for(int j = 0; j < board.Height; j++)
            {
                var b = board.allTiles[i, j].GetComponent<Tiles>();
                if (b.objectType == ObejctType.WaterShot)
                {
                    if (b.gameObject.tag == "WaterShot")
                    {
                        b.objectType = ObejctType.None;
                        b.gameObject.tag = "Untagged";
                        b.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    }

                }
            }
        }
    }

    IEnumerator BlueChange(int col, int row, int typeValue, int oriC, int oriR) // 배터리 0 불 1 물 2 캐논 3 호스 4
    {
        yield return new WaitForSeconds(.1f);
        c = col;
        r = row;

        isBlueEnd = false;
        var from = board.allTiles[oriC, oriR].GetComponent<Tiles>(); //원본
        var to = board.allTiles[col, row].GetComponent<Tiles>(); //이동할 장소

        to.originCol = oriC;
        to.originRow = oriR;

        to.gameObject.tag = from.gameObject.tag;
        to.objectType = from.objectType;
        to.GetComponent<SpriteRenderer>().sprite = from.GetComponent<SpriteRenderer>().sprite;
        to.isRed = false;
        to.isBlue = false;
        to.isYellow = false;

        to.isUp = from.isUp;
        to.isDown = from.isDown;
        to.isRight = from.isRight;
        to.isLeft = from.isLeft;
        to.isIndexUp = from.isIndexUp;
        to.isIndexDown = from.isIndexDown;
        to.isIndexLeft = from.isIndexLeft;
        to.isIndexRight = from.isIndexRight;

        to.isConnect = from.isConnect;
        to.isCanMove = true;
        to.isMoved = true;
        TypeImage(typeValue, to);

        ////////////////////////////////////////////////////
        from.originCol = c;
        from.originRow = r;

        from.gameObject.tag = "Untagged";
        from.objectType = Tiles.ObejctType.Yet;
        from.isRed = false;
        from.isBlue = true;
        from.isYellow = false;

        from.isUp = false;
        from.isDown = false;
        from.isRight = false;
        from.isLeft = false;
        from.isIndexUp = false;
        from.isIndexDown = false;
        from.isIndexLeft = false;
        from.isIndexRight = false;

        from.isConnect = true;
        from.isCanMove = true;
        from.isMoved = false;
        from.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        from.gameObject.GetComponent<SpriteRenderer>().sprite = def;

        yield return new WaitForSeconds(.1f);
        lineChecker.FindObject();
        typeActRe();
    }

    void TypeImage(int typeValue, Tiles to) 
    {
        var obj = to.gameObject.GetComponent<SpriteRenderer>();
        switch(typeValue)
        {
            case 0:  // 배터리
                obj.color = Color.green;
                break;
            case 1:  // 불
                obj.color = Color.red;
                break;
            case 2: // 물
                obj.color = new Color(0,255,255);
                break;
            case 3: // 캐논
                obj.color = Color.gray;
                break;
            case 4: // 호스
                obj.color = Color.gray;
                break;
            case 5: // 차단기
                obj.color = Color.green;
                break;

        }
    }

    void typeActRe()
    {
        for (int i = 0; i < board.Width; i++)
        {
            for (int j = 0; j < board.Height; j++)
            {
                var a = board.allTiles[i, j].GetComponent<Tiles>();
                if (a.objectType == Tiles.ObejctType.End)
                {
                    a.typeAct();
                }
            }
        }
    }

    public void blueChangeBack()
    {
        StartCoroutine(BlueChangeBack());
    }

    IEnumerator BlueChangeBack()
    {
        yield return new WaitForSeconds(.1f);
        for(int i = 0; i< board.Width; i++)
        {
            for(int j = 0; j< board.Height; j++)
            {
                var a = board.allTiles[i, j].GetComponent<Tiles>();
                if (a.objectType == Tiles.ObejctType.Yet)
                {
                    var movedObject = board.allTiles[a.originCol, a.originRow].GetComponent<Tiles>(); // 이동한 그 물이든 배터리든 가져오기

                    a.gameObject.tag = movedObject.gameObject.tag;
                    a.objectType = movedObject.objectType;
                    a.gameObject.GetComponent<SpriteRenderer>().sprite = movedObject.gameObject.GetComponent<SpriteRenderer>().sprite;
                    a.isRed = false;
                    a.isBlue = false;
                    a.isYellow = false;

                    a.isUp = movedObject.isUp;
                    a.isDown = movedObject.isDown;
                    a.isRight = movedObject.isRight;
                    a.isLeft = movedObject.isLeft;

                    a.isIndexUp = movedObject.isIndexUp;
                    a.isIndexDown = movedObject.isIndexDown;
                    a.isIndexRight = movedObject.isIndexRight;
                    a.isIndexLeft = movedObject.isIndexLeft;

                    a.isConnect = movedObject.isConnect;
                    a.isCanMove = true;
                    a.isMoved = false;
                    a.gameObject.GetComponent<SpriteRenderer>().color = movedObject.GetComponent<SpriteRenderer>().color;

                    movedObject.originCol = movedObject.col;
                    movedObject.originRow = movedObject.row;

                    movedObject.gameObject.tag = "Untagged";
                    movedObject.objectType = Tiles.ObejctType.None;
                    movedObject.isRed = false;
                    movedObject.isBlue = true;
                    movedObject.isYellow = false;

                    movedObject.isUp = false;
                    movedObject.isDown = false;
                    movedObject.isRight = false;
                    movedObject.isLeft = false;

                    movedObject.isIndexUp = false;
                    movedObject.isIndexDown = false;
                    movedObject.isIndexRight = false;
                    movedObject.isIndexLeft = false;

                    movedObject.isConnect = false;
                    movedObject.isCanMove = false;
                    movedObject.isMoved = false;
                    movedObject.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    movedObject.gameObject.GetComponent<SpriteRenderer>().sprite = def;

                    a.originCol = a.col;
                    a.originRow = a.row;
                }
            }
        }
        waterClear();
        typeActRe();
    }
    #region DIRECTION
    void BlueUp(int col, int row, int typeValue, int oriC, int oriR)
    {
        var a = board.allTiles[col, row + 1].GetComponent<Tiles>();
        if (a.isBlue)
        {
            if (!a.isCanMove)
            {
                blueList[bluecount] = a.gameObject;
                bluecount++;
                a.isCanMove = true;
                BlueChecking(col, row + 1, typeValue, oriC, oriR);
            }
        }
    }
    void BlueDown(int col, int row, int typeValue, int oriC, int oriR)
    {
        var a = board.allTiles[col, row - 1].GetComponent<Tiles>();
        if (a.isBlue)
        {
            if (!a.isCanMove)
            {
                blueList[bluecount] = a.gameObject;
                bluecount++;
                a.isCanMove = true;
                BlueChecking(col, row - 1, typeValue, oriC, oriR);
            }
        }
    }
    void BlueRight(int col, int row, int typeValue, int oriC, int oriR)
    {
        var a = board.allTiles[col + 1, row].GetComponent<Tiles>();
        if (a.isBlue)
        {
            if (!a.isCanMove)
            {
                blueList[bluecount] = a.gameObject;
                bluecount++;
                a.isCanMove = true;
                BlueChecking(col + 1, row, typeValue, oriC, oriR);
            }
        }
    }
    void BlueLeft(int col, int row, int typeValue, int oriC, int oriR)
    {
        var a = board.allTiles[col - 1, row].GetComponent<Tiles>();
        if (a.isBlue)
        {
            if (!a.isCanMove)
            {
                blueList[bluecount] = a.gameObject;
                bluecount++;
                a.isCanMove = true;
                BlueChecking(col - 1, row, typeValue, oriC, oriR);
            }
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        def = Resources.Load<Sprite>("Textures/ETC/TableTile");
        board = FindObjectOfType<Board>();
        redCheck = FindObjectOfType<RedCheck>();
        lineChecker = FindObjectOfType<LineChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
