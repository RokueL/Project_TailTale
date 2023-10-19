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
        Wall,
        Yet,
        WaterShot
    }

    public ObejctType objectType;

    public bool isRed;
    public bool isBlue;
    public bool isYellow;

    public bool isConnect;
    public bool isObjectConnect;

    bool isUp;
    bool isDown;
    bool isLeft;
    bool isRight = true;

    public int col, row;

    public int originCol;
    public int originRow;
    public int changeCol;
    public int changeRow;

    LineCreate lineCreate;
    Board board;
    PlayerController playerController;

    public IObjectPool<GameObject> tilePool { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        lineCreate =  FindObjectOfType<LineCreate>();
        playerController = FindObjectOfType<PlayerController>();
        if(objectType == ObejctType.Object)
        {
            isConnect = true;
            originCol = col;
            originRow = row;
            Debug.Log("Tile " + col + " , " + row);
            Debug.Log("Tile " + originCol + " , " + originRow);
        }
    }

    public void Change(int moveCol, int moveRow, int oriCol, int oriRow)
    {
            var a = board.allTiles[moveCol, moveRow];
            var b = board.allTiles[oriCol, oriRow];
            a.GetComponent<SpriteRenderer>().color = Color.green;
            a.GetComponent<Tiles>().objectType = ObejctType.Object;
            a.GetComponent<Tiles>().isBlue = false;
            a.gameObject.tag = "Battery";

            b.GetComponent<SpriteRenderer>().color = Color.blue;
            Debug.Log("b color change");
            b.GetComponent<Tiles>().objectType = ObejctType.Yet;
            b.GetComponent<Tiles>().isConnect = true;
            b.GetComponent<Tiles>().isBlue = true;
            b.gameObject.tag = "Untagged";

    }

    public void backChange(int moveCol, int moveRow, int oriCol, int oriRow)
    {
            var a = board.allTiles[moveCol, moveRow];
            var b = board.allTiles[oriCol, oriRow];
            a.GetComponent<SpriteRenderer>().color = Color.green;
            a.GetComponent<Tiles>().objectType = ObejctType.Object;
            a.GetComponent<Tiles>().isBlue = false;
            a.gameObject.tag = "Battery";

            b.GetComponent<SpriteRenderer>().color = Color.blue;
            Debug.Log("BackUP");
            b.GetComponent<Tiles>().objectType = ObejctType.None;
            b.GetComponent<Tiles>().isConnect = true;
            b.GetComponent<Tiles>().isBlue = true;
            b.gameObject.tag = "Untagged";
    }

    void LightOn()
    {
        GetComponent<SpriteRenderer>().color = Color.grey;
        Debug.Log("ConnectRed");
    }

    void LightOff()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
        Debug.Log("DisonnectRed");
    }

    void waterShot()
    {
        if (isUp)
        {
            for(int i = row + 1; i < board.Height; i++)
            {
                waterrow(i);
            }
        }
        else if (isDown)
        {
            for (int i = row - 1; i >= 0; i--)
            {
                waterrow(i);
            }
        }
        else if(isLeft)
        {
            for(int i = col - 1; i >= 0; i--)
            {
                watercol(i);
            }
        }
        else if (isRight)
        {
            for(int i = col + 1; i < board.Width; i++)
            {
                watercol(i);
            }
        }
    }

    void waterrow(int i)
    {
        var a = board.allTiles[col, i].GetComponent<Tiles>();
        if (a.objectType == ObejctType.Object)
        {
            if (a.gameObject.tag == "Fire")
            {
                a.gameObject.tag = "Untagged";
                a.objectType = ObejctType.WaterShot;
                board.allTiles[col, i].GetComponent<SpriteRenderer>().color = new Color(0, 255, 255);
                a.isConnect = false;
            }
        }
        else if (a.objectType == ObejctType.None)
        {
            if (a.isRed)
            {
                playerController.myRed++;
            }
            else if (a.isBlue)
            {
                playerController.myBlue++;
            }
            else if (a.isYellow)
            {
                playerController.myYellow++;
            }
            board.allTiles[col, i].GetComponent<SpriteRenderer>().color = new Color(0, 255, 255);
            a.objectType = ObejctType.WaterShot;
        }

    }
    void watercol(int i)
    {
        var a = board.allTiles[i, row].GetComponent<Tiles>();
        if (a.objectType == ObejctType.Object)
        {
            if (a.gameObject.tag == "Fire")
            {
                a.gameObject.tag = "Untagged";
                a.objectType = ObejctType.WaterShot;
                board.allTiles[i, row].GetComponent<SpriteRenderer>().color = new Color(0, 255, 255);
                a.isConnect = false;
            }
        }
        else if (a.objectType == ObejctType.None)
        {
            if (a.isRed)
            {
                playerController.myRed++;
            }
            else if (a.isBlue)
            {
                playerController.myBlue++;
            }
            else if (a.isYellow)
            {
                playerController.myYellow++;
            }
            board.allTiles[i,row].GetComponent<SpriteRenderer>().color = new Color(0, 255, 255);
            a.objectType = ObejctType.WaterShot;
        }
    }

    void waterStop()
    {
        if (isUp)
        {
            for (int i = row + 1; i < board.Height; i++)
            {
                var a = board.allTiles[col, i].GetComponent<Tiles>();
                if (a.objectType == ObejctType.WaterShot)
                {
                    board.allTiles[col, i].GetComponent<SpriteRenderer>().color = Color.white;
                    a.objectType = ObejctType.None;
                }
            }
        }
        else if (isDown)
        {
            for (int i = row + 1; i < board.Height; i++)
            {
                var a = board.allTiles[col, i].GetComponent<Tiles>();
                if (a.objectType == ObejctType.WaterShot)
                {
                    board.allTiles[col, i].GetComponent<SpriteRenderer>().color = Color.white;
                    a.objectType = ObejctType.None;
                }
            }
        }
        else if (isLeft)
        {
            for (int i = col - 1; i >= 0; i--)
            {
                var a = board.allTiles[i, row].GetComponent<Tiles>();
                if (a.objectType == ObejctType.WaterShot)
                {
                    board.allTiles[i, row].GetComponent<SpriteRenderer>().color = Color.white;
                    a.objectType = ObejctType.None;
                }
            }
        }
        else if (isRight)
        {
            for (int i = col + 1; i < board.Width; i++)
            {
                var a = board.allTiles[i, row].GetComponent<Tiles>();
                if (a.objectType == ObejctType.WaterShot)
                {
                    board.allTiles[i, row].GetComponent<SpriteRenderer>().color = Color.white;
                    a.objectType = ObejctType.None;
                }
            }
        }
    }

    void ObejctCheck()
    {
        if (this.gameObject.tag == "Battery")
        {
            lineCreate.PaintCheck(col, row, 0, 0);


            lineCreate.PaintCheck(col, row, 0, 1);
            lineCreate.PaintCheck(col, row, 0, 2);
        }
        else if (this.gameObject.tag == "Water")
        {
            lineCreate.PaintCheck(col, row, 1, 0);
            lineCreate.PaintCheck(col, row, 1, 1);
            lineCreate.PaintCheck(col, row, 1, 2);
        }
        else if (this.gameObject.tag == "Fire")
        {
            lineCreate.PaintCheck(col, row, 2, 0);
            lineCreate.PaintCheck(col, row, 2, 1);
            lineCreate.PaintCheck(col, row, 2, 2);
        }
    }

    public void typeAct()
    {
        switch (objectType)
        {
            case ObejctType.Object:
                ObejctCheck();
                break;
            case ObejctType.Paint:
                break;
            case ObejctType.End:
                if (isConnect)
                {
                    if (gameObject.tag == "Hose")
                    {
                        waterShot();
                    }
                    else if (gameObject.tag == "ScreenDoor")
                    {
                        LightOn();
                    }
                    break;
                }
                else if (!isConnect)
                {
                    if (gameObject.tag == "Hose")
                    {
                        waterStop();
                    }
                    else if (gameObject.tag == "ScreenDoor")
                    {
                        LightOff();
                    }
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
