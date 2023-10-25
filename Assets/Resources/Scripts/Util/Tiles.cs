using Manager;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.U2D;

public class Tiles : MonoBehaviour
{
    public enum ObejctType
    {
        None,
        Object,
        Paint,
        End,
        Block,
        Wall,
        Yet,
        WaterShot,
        Rock,
        Key,
        Door
    }
    public SpriteAtlas spAt;

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
        spAt = Resources.Load<SpriteAtlas>("Textures/spAtlas");


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

    void TileTexutreSetUp()
    {
        var obj = gameObject.GetComponent<SpriteRenderer>().sprite;
        switch (objectType)
        {
            case ObejctType.Rock:
                gameObject.GetComponent<SpriteRenderer>().sprite = spAt.GetSprite("Rock_01");
                break;
            case ObejctType.Key:
                gameObject.GetComponent<SpriteRenderer>().sprite = spAt.GetSprite("Key");
                break;
            case ObejctType.Door:
                gameObject.GetComponent<SpriteRenderer>().sprite = spAt.GetSprite("Door");
                break;
            case ObejctType.Block:
                gameObject.GetComponent<SpriteRenderer>().sprite = spAt.GetSprite("Block");
                break;
            case ObejctType.Object:
                if (gameObject.tag == "Water")
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = spAt.GetSprite("Water");
                }
                else if(gameObject.tag == "Fire")
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = spAt.GetSprite("Fire");
                }
                else if(gameObject.tag == "Battery")
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = spAt.GetSprite("Battery");
                }
                break;
            case ObejctType.End:
                gameObject.GetComponent<SpriteRenderer>().sprite = spAt.GetSprite("ScreenDoor");
                break;
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
                    if (b.objectType == ObejctType.None || b.objectType == ObejctType.Object || b.objectType == ObejctType.WaterShot)
                    {
                        if (b.gameObject.tag == "Fire" || b.gameObject.tag == "Untagged" || b.gameObject.tag == "WaterShot")
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
                            b.isConnect = false;
                        }
                    }
                    else
                    {
                        return;
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
                    if (b.objectType == ObejctType.None || b.objectType == ObejctType.Object || b.objectType == ObejctType.WaterShot)
                    {
                        if (b.gameObject.tag == "Fire" || b.gameObject.tag == "Untagged" || b.gameObject.tag == "WaterShot")
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
                            b.isConnect = false;
                        }
                    }
                    else
                    {
                        return;
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
                    if (b.objectType == ObejctType.None || b.objectType == ObejctType.Object || b.objectType == ObejctType.WaterShot)
                    {
                        if (b.gameObject.tag == "Fire" || b.gameObject.tag == "Untagged" || b.gameObject.tag == "WaterShot")
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
                            b.isConnect = false;
                        }
                    }
                    else
                    {
                        return;
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
                    if (b.objectType == ObejctType.None || b.objectType == ObejctType.Object || b.objectType == ObejctType.WaterShot)
                    {
                        if (b.gameObject.tag == "Fire" || b.gameObject.tag == "Untagged" || b.gameObject.tag == "WaterShot")
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
                            b.isConnect = false;
                        }
                    }
                    else
                    {
                        return;
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
                            if (b.isYellow)
                            {
                                b.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                            }
                            else
                            {
                                b.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                            }
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
                            if (b.isYellow)
                            {
                                b.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                            }
                            else
                            {
                                b.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                            }
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
                            if (b.isYellow)
                            {
                                b.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                            }
                            else
                            {
                                b.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                            }
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
                            if (b.isYellow)
                            {
                                b.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                            }
                            else
                            {
                                b.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                            }
                        }

                    }
                }
        }
    }

    void CannonShot()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        if (isUp)
        {

            for (int i = row + 1; i < board.Height; i++)
            {
                var b = board.allTiles[col, i].GetComponent<Tiles>();
                if (b.objectType == ObejctType.Rock)
                {
                    b.objectType = ObejctType.None;
                    b.gameObject.tag = "Untagged";
                    b.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    b.isRed = false;
                    b.isBlue = false;
                    b.isYellow = false;
                    b.isConnect = false;
                }
                else if( b.objectType == ObejctType.None)
                {

                }
                else
                {
                    break;
                }
            }

        }
        else if (isDown)
        {

            for (int i = row - 1; i >= 0; i--)
            {
                var b = board.allTiles[col, i].GetComponent<Tiles>();
                if (b.objectType == ObejctType.Rock)
                {
                    b.objectType = ObejctType.None;
                    b.gameObject.tag = "Untagged";
                    b.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    b.isRed = false;
                    b.isBlue = false;
                    b.isYellow = false;
                    b.isConnect = false;
                }
                else if (b.objectType == ObejctType.None)
                {

                }
                else
                {
                    break;
                }
            }

        }
        else if (isLeft)
        {
            for (int i = col - 1; i >= 0; i--)
            {
                var b = board.allTiles[i, row].GetComponent<Tiles>();
                if (b.objectType == ObejctType.Rock)
                {
                    b.objectType = ObejctType.None;
                    b.gameObject.tag = "Untagged";
                    b.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    b.isRed = false;
                    b.isBlue = false;
                    b.isYellow = false;
                    b.isConnect = false;
                }
                else if (b.objectType == ObejctType.None)
                {

                }
                else
                {
                    break;
                }
            }
        }
        else if (isRight)
        {
            Debug.Log(" 오른쪽 발사");
            for (int i = col; i < board.Width; i++)
            {
                var b = board.allTiles[i, row].GetComponent<Tiles>();
                if (b.objectType == ObejctType.Rock)
                {
                    Debug.Log("돌 부숨");
                    b.objectType = ObejctType.None;
                    b.gameObject.tag = "Untagged";
                    b.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    b.isRed = false;
                    b.isBlue = false;
                    b.isYellow = false;
                    b.isConnect = false;
                }
                else if (b.objectType == ObejctType.None)
                {

                }
                else
                {
                    break;
                }
            }

        }

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
            case ObejctType.Door: 
                if(isConnect)
                {
                    UIManager.Instance.GameClearEvent();
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        TileTexutreSetUp();
        if (isUp)
        {
            if(gameObject.tag == "Hose")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = spAt.GetSprite("Hose_Up");
            }
            else if(gameObject.tag == "Cannon")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = spAt.GetSprite("Cannon_Up");
            }
        }
        else if (isDown)
        {
            if (gameObject.tag == "Hose")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = spAt.GetSprite("Hose_Down");
            }
            else if (gameObject.tag == "Cannon")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = spAt.GetSprite("Cannon_Down");
            }
        }
        else if (isLeft)
        {
            if (gameObject.tag == "Hose")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = spAt.GetSprite("Hose_Left");
            }
            else if (gameObject.tag == "Cannon")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = spAt.GetSprite("Cannon_Left");
            }
        }
        else if (isRight)
        {
            if (gameObject.tag == "Hose")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = spAt.GetSprite("Hose_Right");
            }
            else if (gameObject.tag == "Cannon")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = spAt.GetSprite("Cannon_Right");
            }
        }
        else
        {

        }
    }
}
