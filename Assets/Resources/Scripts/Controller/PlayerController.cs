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
            var boards = board.allTiles[column, row].GetComponent<SpriteRenderer>();
            boards.color = tailC;

            switch (tailvalue)
            {
                case 0:
                    var a = board.allTiles[column, row].GetComponent<Tiles>();
                    a.isRed = true;
                    a.isBlue = false;
                    a.isYellow = false;
                    break;
                case 1:
                    var b = board.allTiles[column, row].GetComponent<Tiles>();
                    b.isRed = false;
                    b.isBlue = true;
                    b.isYellow = false;
                    break;
                case 2:
                    var c = board.allTiles[column, row].GetComponent<Tiles>();
                    c.isRed = false;
                    c.isBlue = false;
                    c.isYellow = true;
                    break;
            }
            board.objects.GetComponent<Tiles>().typeAct();

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
                DrawPaint();
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
                DrawPaint();
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
                DrawPaint();
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
                DrawPaint();
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
            Debug.Log("space down");
            isSpacePress = true;
            DrawPaint();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("space up");
            isSpacePress = false;
        }
    }
    #endregion
}
