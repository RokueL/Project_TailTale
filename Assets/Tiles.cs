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
    public bool isMoved;

    public bool isUp;
    public bool isDown;
    public bool isLeft;
    public bool isRight;

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
        board = FindObjectOfType<Board>();
        lineChecker =  FindObjectOfType<LineChecker>();
        playerController = FindObjectOfType<PlayerController>();
        originCol = col; originRow = row;
        if(objectType == ObejctType.Object)
        {
            isConnect = true;
        }

    }

    void CannonShot()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    void CannonStop()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
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
    }
}
