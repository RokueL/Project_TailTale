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
        Yet
    }

    public ObejctType objectType;

    public bool isRed;
    public bool isBlue;
    public bool isYellow;

    public bool isConnect;
    public bool isObjectConnect;

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

    void ObejctCheck()
    {
        if(this.gameObject.tag == "Battery")
        {
            lineCreate.PaintCheck(col, row, 0, 0);


            lineCreate.PaintCheck(col, row, 0, 1);
            lineCreate.PaintCheck(col, row, 0, 2);
        }
        else if(this.gameObject.tag == "Water")
        {
            lineCreate.PaintCheck(col, row, 1, 0);
            lineCreate.PaintCheck(col, row, 1, 1);
            lineCreate.PaintCheck(col, row, 1, 2);
        }
        else if(this.gameObject.tag == "Fire")
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
                    LightOn();
                    break;
                }
                else if (!isConnect)
                {
                    LightOff(); 
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
