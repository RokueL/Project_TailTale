using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
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
        WaterShot,
        Rock
    }
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    public ObejctType objectType;

    public bool isRed;
    public bool isBlue;
    public bool isYellow;

    public bool isConnect;
    public bool isMoved;
    public bool isCanMove;

    public bool isUp;
    public bool isDown;
    public bool isLeft;
    public bool isRight;

    public bool isIndexUp;
    public bool isIndexDown;
    public bool isIndexLeft;
    public bool isIndexRight;

    public int col, row;

    public int originCol, originRow;
    public int changeCol, changeRow;

    LineChecker lineChecker;
    Board board;
    PlayerController playerController;

    public IObjectPool<GameObject> tilePool { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        up = Resources.Load<Sprite>("Textures/up");
        down = Resources.Load<Sprite>("Textures/down");
        left = Resources.Load<Sprite>("Textures/left");
        right = Resources.Load<Sprite>("Textures/right");


        board = FindObjectOfType<Board>();
        lineChecker =  FindObjectOfType<LineChecker>();
        playerController = FindObjectOfType<PlayerController>();
        originCol = col; originRow = row;
        if(objectType == ObejctType.Object)
        {
            isConnect = true;
            isCanMove = true;
        }
        if(objectType == ObejctType.End)
        {
            if(gameObject.tag == "Cannon" || gameObject.tag == "Hose")
            {
                isCanMove = true;
                isRight = true;
                isIndexRight = true;
            }
        }

    }

    void HoseShot()
    {
        Debug.Log("Shot");
        if (isUp)
        {
            if (board.allTiles[col, row + 1] != null)
            {
                for (int i = row + 1; i < board.Height; i++)
                {
                    var b = board.allTiles[col, i].GetComponent<Tiles>();
                    if (b.objectType == ObejctType.None || b.objectType == ObejctType.Object)
                    {
                        if (b.gameObject.tag == "Fire" || b.gameObject.tag == "Untagged")
                        {
                            if (b.isRed)
                            {
                                playerController.myRed++;
                            }
                            if (b.isBlue)
                            {
                                playerController.myBlue++;
                            }
                            if (b.isYellow)
                            {
                                playerController.myYellow++;
                            }
                            b.objectType = ObejctType.WaterShot;
                            b.gameObject.tag = "WaterShot";
                            b.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 255);
                            b.isRed = false;
                            b.isBlue = false;
                            b.isYellow = false;
                            b.isConnect = false;
                        }
                    }
                    else if (b.objectType == ObejctType.Rock)
                    {
                        break;
                    }
                }
            }
        }
        if (isDown)
        {
            if (board.allTiles[col, row - 1] != null)
            {
                for (int i = row - 1; i >= 0; i--)
                {
                    var b = board.allTiles[col, i].GetComponent<Tiles>();
                    if (b.objectType == ObejctType.None || b.objectType == ObejctType.Object)
                    {
                        if (b.gameObject.tag == "Fire" || b.gameObject.tag == "Untagged")
                        {
                            if (b.isRed)
                            {
                                playerController.myRed++;
                            }
                            if (b.isBlue)
                            {
                                playerController.myBlue++;
                            }
                            if (b.isYellow)
                            {
                                playerController.myYellow++;
                            }
                            b.objectType = ObejctType.WaterShot;
                            b.gameObject.tag = "WaterShot";
                            b.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 255);
                            b.isRed = false;
                            b.isBlue = false;
                            b.isYellow = false;
                            b.isConnect = false;
                        }
                    }
                    else if (b.objectType == ObejctType.Rock)
                    {
                        break ;
                    }
                }
            }
        }
        if(isLeft)
        {
            if (board.allTiles[col - 1, row] != null)
            {
                for (int i = col - 1; i >= 0; i--)
                {
                    var b = board.allTiles[i, row].GetComponent<Tiles>();
                    if (b.objectType == ObejctType.None || b.objectType == ObejctType.Object)
                    {
                        if (b.gameObject.tag == "Fire" || b.gameObject.tag == "Untagged")
                        {
                            if (b.isRed)
                            {
                                playerController.myRed++;
                            }
                            if (b.isBlue)
                            {
                                playerController.myBlue++;
                            }
                            if (b.isYellow)
                            {
                                playerController.myYellow++;
                            }
                            b.objectType = ObejctType.WaterShot;
                            b.gameObject.tag = "WaterShot";
                            b.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 255);
                            b.isRed = false;
                            b.isBlue = false;
                            b.isYellow = false;
                            b.isConnect = false;
                        }
                    }
                    else if (b.objectType == ObejctType.Rock)
                    {
                        break;
                    }
                }
            }
        }
        if (isRight)
        {
            if (board.allTiles[col + 1, row] != null)
            {
                for (int i = col + 1; i < board.Width; i++)
                {
                    var b = board.allTiles[i, row].GetComponent<Tiles>();
                    if (b.objectType == ObejctType.None || b.objectType == ObejctType.Object)
                    {
                        if (b.gameObject.tag == "Fire" || b.gameObject.tag == "Untagged")
                        {
                            if (b.isRed)
                            {
                                playerController.myRed++;
                            }
                            if (b.isBlue)
                            {
                                playerController.myBlue++;
                            }
                            if (b.isYellow)
                            {
                                playerController.myYellow++;
                            }
                            b.objectType = ObejctType.WaterShot;
                            b.gameObject.tag = "WaterShot";
                            b.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 255);
                            b.isRed = false;
                            b.isBlue = false;
                            b.isYellow = false;
                            b.isConnect = false;
                        }
                    }
                    else if (b.objectType == ObejctType.Rock)
                    {
                        break;
                    }
                }
            }
        }
    }

    void HoseStop()
    {
        Debug.Log("stop");
        if (isUp)
        {
            if (board.allTiles[col, row + 1] != null)
                for (int i = row + 1; i < board.Height; i++)
                {
                    var b = board.allTiles[col, i].GetComponent<Tiles>();
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
        if (isDown)
        {
            if (board.allTiles[col, row - 1] != null)
                for (int i = row - 1; i >= 0; i--)
                {
                    var b = board.allTiles[col, i].GetComponent<Tiles>();
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
        if (isLeft)
        {
            if (board.allTiles[col - 1, row] != null)
                for (int i = col - 1; i >= 0; i--)
                {
                    var b = board.allTiles[i, row].GetComponent<Tiles>();
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
        if (isRight)
        {
            if (board.allTiles[col + 1, row] != null)
                for (int i = col + 1; i < board.Width; i++)
                {
                    var b = board.allTiles[i, row].GetComponent<Tiles>();
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

    void CannonShot()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

    void CannonStop()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public void typeAct()
    {
        switch (objectType)
        {
            case ObejctType.Object:

                break;
            case ObejctType.Paint:
                break;
            case ObejctType.End:
                if (isConnect)
                {
                    if (gameObject.tag == "Hose")
                    {
                        HoseShot();
                        //CannonShot();
                    }
                    else if (gameObject.tag == "ScreenDoor")
                    {

                    }
                    else if(gameObject.tag == "Cannon")
                    {
                        CannonShot();
                    }
                }
                else if (!isConnect)
                {
                    if (gameObject.tag == "Hose")
                    {
                        HoseStop();
                       //CannonStop();
                    }
                    else if (gameObject.tag == "ScreenDoor")
                    {
                    }
                    else if (gameObject.tag == "Cannon")
                    {
                        CannonStop();
                    }
                }
                break;
            case ObejctType.Wall:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {


        if(isUp)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = up;
        }
        else if (isDown)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = down;
        }
        else if (isLeft)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = left;
        }
        else if (isRight)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = right;
        }
        else
        {

        }
    }
}
