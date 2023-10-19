using Manager;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    Board board;
    LineCreate lineCreate;

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
    int myRed;
    public int myBlue;
    int myYellow;

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
            if (obj.objectType == Tiles.ObejctType.None)
            {
                var boards = board.allTiles[column, row].GetComponent<SpriteRenderer>();
                boards.color = tailC;
                lineCreate.findBlue = 0;
                switch (tailvalue)
                {
                    case 0:
                        var a = board.allTiles[column, row].GetComponent<Tiles>();
                        if (a.isBlue)
                        {
                            myBlue++;
                            myRed--;
                            lineCreate.isBlueEnd = false;
                            if (myBlue != 0)
                            {
                                if (lineCreate.isCanChange)
                                {
                                    lineCreate.BlueBack();
                                }
                            }
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
                        else
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
                            lineCreate.isBlueEnd = false;
                            myYellow--; 
                            if (myBlue != 0)
                            {
                                if (lineCreate.isCanChange)
                                {
                                    lineCreate.BlueBack();
                                }
                            }
                        }
                        else if (c.isYellow)
                        {

                        }
                        else
                        {
                            myYellow--;
                        }
                        c.isRed = false;
                        c.isBlue = false;
                        c.isYellow = true;
                        c.isConnect = false;
                        break;
                }
                lineCreate.isEnd = false;
                lineCreate.ResetRed();
                lineCreate.ResetBlue();
                lineCreate.FindStartTypeAct();
                lineCreate.disconnect();
            }
        }
    }


    void OnKeyboard()
    {
        //이동만이에용
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            if ( row < board.Height-1)
            {
                row++;
            }
            if (isSpacePress)
            {
                switch (tailvalue)
                {
                    case 0:
                        if (myRed > 0)
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
        }
        else if (Input.GetKeyDown(KeyCode.S)) 
        {
            if (row > 0)
            {
                row--;
            }
            if (isSpacePress)
            {
                switch (tailvalue)
                {
                    case 0:
                        if (myRed > 0)
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
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (column < board.Width - 1)
            {
                column++;
            }
            if (isSpacePress)
            {
                switch (tailvalue)
                {
                    case 0:
                        if (myRed > 0)
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
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (column > 0)
            {
                column--;
            }
            if (isSpacePress)
            {
                switch (tailvalue)
                {
                    case 0:
                        if (myRed > 0)
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
        lineCreate = FindObjectOfType<LineCreate>();
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
            Debug.Log("space up");
            isSpacePress = false;
        }
    }
    #endregion
}
