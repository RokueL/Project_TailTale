using Manager;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    Board board;
    LineChecker lineChecker;

    RedCheck redCheck;
    BlueCheck blueCheck;
    YellowCheck yellowCheck;

    GameObject myTail;

    float swipeSpeed = 0.2f;

    int tailvalue;
    int targetX;
    int targetY;
    public int column;
    public int row;

    int maxRed = 25;
    int maxBlue = 5;
    int maxYellow = 5;
    public int myRed;
    public int myBlue;
    public int myYellow;

    public bool isSpacePress;

    Color tailC;

    Vector2 tempPosition;

    #region TRIGGER
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Paint")
        {

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Paint")
        {

        }
    }
    #endregion

    void DrawPaint()
    {
        if (isSpacePress)
        {
            var obj = board.allTiles[column, row].GetComponent<Tiles>();
            if (obj.objectType == Tiles.ObejctType.None) //맨바닥에서만 작동하도록
            {
                var boards = board.allTiles[column, row].GetComponent<SpriteRenderer>();
                boards.color = tailC;
                switch (tailvalue)  //꼬리 값 즉 꼬리색에 따라 바닥에 칠하는 거
                {
                    case 0:
                        var a = board.allTiles[column, row].GetComponent<Tiles>(); //지금 밑바닥
                        if (a.isBlue) //파란색이면 지우고 반납
                        {
                            myBlue++;
                            myRed--;
                            blueCheck.ResetBlue();
                            blueCheck.blueChangeBack();
                        }
                        else if (a.isYellow)
                        {
                            myYellow++;
                            myRed--;

                        }
                        else if (a.isRed)
                        {

                        }
                        else
                        {
                            myRed--;
                        }
                        a.isRed = true;
                        a.isBlue = false;
                        a.isYellow = false;
                        a.isConnect = false;
                        break;
                    case 1:
                        var b = board.allTiles[column, row].GetComponent<Tiles>();
                        if (b.isRed)
                        {
                            myRed++;
                            myBlue--;
                        }
                        else if (b.isYellow)
                        {
                            myYellow++;
                            myBlue--;
                        }
                        else if (b.isBlue)
                        {

                        }
                        else //맨바닥임
                        {
                            myBlue--;
                        }
                        b.isRed = false;
                        b.isBlue = true;
                        b.isYellow = false;
                        b.isConnect = false;
                        break;
                    case 2:
                        var c = board.allTiles[column, row].GetComponent<Tiles>();
                        if (c.isRed)
                        {
                            myRed++;
                            myYellow--;
                        }
                        else if (c.isBlue)
                        {
                            myBlue++;
                            myYellow--;
                            blueCheck.ResetBlue();
                            blueCheck.blueChangeBack();
                        }
                        else if (c.isYellow)
                        {

                        }
                        c.isRed = false;
                        c.isBlue = false;
                        c.isYellow = true;
                        c.isConnect = false;
                        break;
                }
                lineChecker.FindObject();
            }
        }
    }


    void OnKeyboard()
    {
        //이동만이에용
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (row < board.Height - 1)
            {
                var a = board.allTiles[column, row + 1].GetComponent<Tiles>().objectType;
                if (a == Tiles.ObejctType.None)
                {
                    row++;
                }
                else if (a == Tiles.ObejctType.Object)
                {
                    if (board.allTiles[column, row + 1].gameObject.tag == "Water")
                    {
                        row++;
                    }
                }
                if (isSpacePress)
                {
                    switch (tailvalue)
                    {
                        case 0:
                            if (myRed > 0 && a == Tiles.ObejctType.None)
                            {
                                isSpacePress = true;
                                DrawPaint();
                            }
                            break;
                        case 1:
                            if (myBlue > 0 && a == Tiles.ObejctType.None)
                            {
                                isSpacePress = true;
                                DrawPaint();
                            }
                            break;
                        case 2:
                            if (myYellow > 0 && a == Tiles.ObejctType.None)
                            {
                                isSpacePress = true;
                                DrawPaint();
                            }
                            break;
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (row > 0)
            {
                var a = board.allTiles[column, row - 1].GetComponent<Tiles>().objectType;
                if (a == Tiles.ObejctType.None)
                {
                    row--;
                }
                else if (a == Tiles.ObejctType.Object)
                {
                    if (board.allTiles[column, row - 1].gameObject.tag == "Water")
                    {
                        row--;
                    }
                }
                if (isSpacePress)
                {
                    switch (tailvalue)
                    {
                        case 0:
                            if (myRed > 0 && a == Tiles.ObejctType.None)
                            {
                                isSpacePress = true;
                                DrawPaint();
                            }
                            break;
                        case 1:
                            if (myBlue > 0 && a == Tiles.ObejctType.None)
                            {
                                isSpacePress = true;
                                DrawPaint();
                            }
                            break;
                        case 2:
                            if (myYellow > 0 && a == Tiles.ObejctType.None)
                            {
                                isSpacePress = true;
                                DrawPaint();
                            }
                            break;
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (column < board.Width - 1)
            {
                var a = board.allTiles[column + 1, row].GetComponent<Tiles>().objectType;
                if (a == Tiles.ObejctType.None)
                {
                    column++;
                }
                else if (a == Tiles.ObejctType.Object)
                {
                    if (board.allTiles[column + 1, row].gameObject.tag == "Water")
                    {
                        column++;
                    }
                }
                if (isSpacePress)
                {
                    switch (tailvalue)
                    {
                        case 0:
                            if (myRed > 0 && a == Tiles.ObejctType.None)
                            {
                                isSpacePress = true;
                                DrawPaint();
                            }
                            break;
                        case 1:
                            if (myBlue > 0 && a == Tiles.ObejctType.None)
                            {
                                isSpacePress = true;
                                DrawPaint();
                            }
                            break;
                        case 2:
                            if (myYellow > 0 && a == Tiles.ObejctType.None)
                            {
                                isSpacePress = true;
                                DrawPaint();
                            }
                            break;
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (column > 0)
            {
                var a = board.allTiles[column - 1, row].GetComponent<Tiles>().objectType;
                if (a == Tiles.ObejctType.None)
                {
                    column--;
                }
                else if (a == Tiles.ObejctType.Object)
                {
                    if (board.allTiles[column -1, row].gameObject.tag == "Water")
                    {
                        column--;
                    }
                }
                if (isSpacePress)
                {
                    switch (tailvalue)
                    {
                        case 0:
                            if (myRed > 0 && a == Tiles.ObejctType.None)
                            {
                                isSpacePress = true;
                                DrawPaint();
                            }
                            break;
                        case 1:
                            if (myBlue > 0 && a == Tiles.ObejctType.None)
                            {
                                isSpacePress = true;
                                DrawPaint();
                            }
                            break;
                        case 2:
                            if (myYellow > 0 && a == Tiles.ObejctType.None)
                            {
                                isSpacePress = true;
                                DrawPaint();
                            }
                            break;
                    }
                }
            }
            
        }
        //색 변경
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(tailvalue == 2)
            {
                tailvalue = 0;
            }
            else
            {
                tailvalue += 1;
            }
        }
        // 페인트 칠
    }



    #region UNITYDEFAULT

    private void Awake()
    {
        column = 0;
        row = 0;
        myRed = maxRed;
        myBlue = maxBlue;
        myYellow = maxYellow;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        board = FindObjectOfType<Board>();
        lineChecker = FindObjectOfType<LineChecker>();

        redCheck = FindObjectOfType<RedCheck>();
        blueCheck = FindObjectOfType<BlueCheck>();
        yellowCheck = FindObjectOfType<YellowCheck>();

        myTail = GameObject.Find("Tail");
        GameManager.Inputs.KeyAction -= OnKeyboard;
        GameManager.Inputs.KeyAction += OnKeyboard;
    }

    void Move()
    {
        float x = targetX - transform.position.x;
        float y = targetY - transform.position.y;
        if (x < 0)
        {
            x = x * -1;
        }
        if (y < 0)
        {
            y = y * -1;
        }

        if (x > 0.1f)
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, swipeSpeed);
        }
        else
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
        }
        if (y > 0.1f)
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, swipeSpeed);
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
        }
    }

    void tailColor()
    {
        var tail = myTail.GetComponent<SpriteRenderer>();
        switch (tailvalue)
        {
            case 0:
                tailC = Color.red;
                tail.color = tailC;
                break;
            case 1:
                tailC = Color.blue;
                tail.color = tailC;
                break;
            case 2:
                tailC = Color.yellow;
                tail.color = tailC;
                break;
        }

    }

    void Update()
    {
        targetX = column;
        targetY = row;
        Move();
        tailColor();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (tailvalue)
            {
                case 0:
                    if(myRed > 0)
                    {
                        isSpacePress = true;
                        DrawPaint();
                    }
                    break;
                case 1:
                    if (myBlue > 0)
                    {
                        isSpacePress = true;
                        DrawPaint();
                    }
                    break;
                case 2:
                    if (myYellow > 0)
                    {
                        isSpacePress = true;
                        DrawPaint();
                    }
                    break;
            }

        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isSpacePress = false;
        }
    }
    #endregion
}
