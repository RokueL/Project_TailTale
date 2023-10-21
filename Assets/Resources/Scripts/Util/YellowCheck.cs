using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCheck : MonoBehaviour
{
    Board board;
    PlayerController playerController;

    bool isTurnLeft;
    bool isTurnRight;

    int yellowCount;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        PlayerController playerController = FindObjectOfType<PlayerController>();
    }



    public void YellowRotation(int col, int row)
    {
        yellowCount = 0;
        StartCoroutine(yellowRotation(col, row));
    }

    IEnumerator yellowRotation(int col, int row)
    {
        yield return new WaitForSeconds(.1f);
        yellowCount = 0;
        var obj = board.allTiles[col, row].GetComponent<Tiles>();

        if (obj.isIndexUp)
        {
            YellowUp(col, row + 1);
            if (!obj.isConnect)
            {
                if (yellowCount == 3)
                {
                    if (isTurnLeft)
                    {
                        obj.isUp = false;
                        obj.isDown = false;
                        obj.isLeft = true;
                        obj.isRight = false;
                    }
                    else if (isTurnRight)
                    {
                        obj.isUp = false;
                        obj.isDown = false;
                        obj.isLeft = false;
                        obj.isRight = true;
                    }
                }
                else if (yellowCount == 5)
                {
                    obj.isUp = false;
                    obj.isDown = true;
                    obj.isLeft = false;
                    obj.isRight = false;
                }
                else if (yellowCount < 3)
                {
                    {
                        obj.isUp = true;
                        obj.isDown = false;
                        obj.isLeft = false;
                        obj.isRight = false;
                        isTurnLeft = false;
                        isTurnRight = false;
                    }
                }
            }
        }

        else if (obj.isIndexDown)
        {
            YellowDown(col, row - 1);

            if (!obj.isConnect)
            {
                if (yellowCount == 3)
                {
                    if (isTurnLeft)
                    {
                        obj.isUp = false;
                        obj.isDown = false;
                        obj.isLeft = true;
                        obj.isRight = false;
                    }
                    else if (isTurnRight)
                    {
                        obj.isUp = false;
                        obj.isDown = false;
                        obj.isLeft = false;
                        obj.isRight = true;
                    }
                }
                else if (yellowCount == 5)
                {
                    obj.isDown = false;
                    obj.isUp = true;
                    obj.isLeft = false;
                    obj.isRight = false;
                }
                else if (yellowCount < 3)
                {
                    obj.isUp = false;
                    obj.isDown = true;
                    obj.isLeft = false;
                    obj.isRight = false;
                    isTurnLeft = false;
                    isTurnRight = false;
                }
            }
        }
        else if (obj.isIndexLeft)
        {
            YellowLeft(col - 1, row);
            if (!obj.isConnect)
            {
                if (yellowCount == 3)
                {
                    if (isTurnLeft)
                    {
                        obj.isUp = false;
                        obj.isDown = true;
                        obj.isLeft = false;
                        obj.isRight = false;
                    }
                    else if (isTurnRight)
                    {
                        obj.isUp = true;
                        obj.isDown = false;
                        obj.isLeft = false;
                        obj.isRight = false;
                    }
                }
                else if (yellowCount == 5)
                {
                    obj.isLeft = false;
                    obj.isRight = true;
                    obj.isUp = false;
                    obj.isDown = false;
                }
                else if (yellowCount < 3)
                {
                    obj.isUp = false;
                    obj.isDown = false;
                    obj.isLeft = true;
                    obj.isRight = false;
                    isTurnLeft = false;
                    isTurnRight = false;
                }
            }
        }
        else if (obj.isIndexRight)
        {
            YellowRight(col + 1, row);

            if (!obj.isConnect)
            {
                if (yellowCount == 3)
                {
                    if (isTurnLeft)
                    {
                        obj.isUp = true;
                        obj.isDown = false;
                        obj.isLeft = false;
                        obj.isRight = false;
                    }
                    else if (isTurnRight)
                    {
                        obj.isUp = false;
                        obj.isDown = true;
                        obj.isLeft = false;
                        obj.isRight = false;
                    }
                }
                else if (yellowCount == 5)
                {
                    obj.isRight = false;
                    obj.isLeft = true;
                    obj.isUp = false;
                    obj.isDown = false;
                }
                else if (yellowCount < 3)
                {
                    obj.isUp = false;
                    obj.isDown = false;
                    obj.isLeft = false;
                    obj.isRight = true;
                    isTurnLeft = false;
                    isTurnRight = false;
                }
            }
        }
    }

    void YellowUp(int col, int row)
    {
        var obj = board.allTiles[col, row];
        Debug.Log(obj.name);
        if (obj != null && board.allTiles[col, row].GetComponent<Tiles>().isYellow) // ¿ß
        {
            yellowCount++;
            if (board.allTiles[col - 1, row] != null && board.allTiles[col - 1, row].GetComponent<Tiles>().isYellow) //¿ßøﬁ
            {
                isTurnLeft = true;
                yellowCount++;
                if (board.allTiles[col - 1, row -1 ] != null && board.allTiles[col - 1, row - 1].GetComponent<Tiles>().isYellow)//¿ßøﬁæ∆
                {
                    yellowCount++;
                    if (board.allTiles[col - 1, row - 2] != null && board.allTiles[col - 1, row - 2].GetComponent<Tiles>().isYellow) // ¿ßøﬁæ∆æ∆
                    {
                        yellowCount++;
                        if(board.allTiles[col, row - 2] != null && board.allTiles[col, row - 2].GetComponent<Tiles>().isYellow) // ¿ßøﬁæ∆æ∆ø¿
                        {
                            yellowCount++;
                        }
                    }
                }
            }
            else if (board.allTiles[col + 1, row] != null && board.allTiles[col + 1, row].GetComponent<Tiles>().isYellow)//¿ßø¿
            {
                isTurnRight = true;
                yellowCount++;
                Debug.Log(yellowCount);
                if (board.allTiles[col + 1, row - 1] != null && board.allTiles[col + 1, row - 1].GetComponent<Tiles>().isYellow)//¿ßø¿æ∆
                {
                    yellowCount++;

                    Debug.Log(yellowCount);
                    if (board.allTiles[col + 1, row - 2] != null && board.allTiles[col + 1, row - 2].GetComponent<Tiles>().isYellow) // ¿ßø¿æ∆æ∆
                    {
                        yellowCount++;

                        Debug.Log(yellowCount);
                        if (board.allTiles[col, row - 2] != null && board.allTiles[col, row - 2].GetComponent<Tiles>().isYellow) // ¿ßøﬁæ∆æ∆øﬁ
                        {
                            yellowCount++;

                            Debug.Log(yellowCount);
                        }
                    }
                }
            }
        }
    }

    void YellowDown(int col, int row) //¥ŸøÓ ∞™¿ª ∞°¡Æø»
    {
        var obj = board.allTiles[col, row];
        Debug.Log(obj.name);
        if (obj != null && board.allTiles[col, row].GetComponent<Tiles>().isYellow) // æ∆∑°
        {
            yellowCount++;
            if (board.allTiles[col - 1, row] != null && board.allTiles[col - 1, row].GetComponent<Tiles>().isYellow) //æ∆øﬁ
            {
                isTurnLeft = true;
                yellowCount++;
                if (board.allTiles[col - 1, row + 1] != null && board.allTiles[col - 1, row + 1].GetComponent<Tiles>().isYellow)//æ∆øﬁ¿ß
                {
                    yellowCount++;
                    if (board.allTiles[col - 1, row + 2] != null && board.allTiles[col - 1, row + 2].GetComponent<Tiles>().isYellow) // æ∆øﬁ¿ß¿ß
                    {
                        yellowCount++;
                        if (board.allTiles[col, row + 2] != null && board.allTiles[col, row + 2].GetComponent<Tiles>().isYellow) // æ∆øﬁ¿ß¿ßø¿
                        {
                            yellowCount++;
                        }
                    }
                }
            }
            else if (board.allTiles[col + 1, row] != null && board.allTiles[col + 1, row].GetComponent<Tiles>().isYellow)//æ∆ø¿
            {
                isTurnRight = true;
                yellowCount++;
                if (board.allTiles[col + 1, row + 1] != null && board.allTiles[col + 1, row + 1].GetComponent<Tiles>().isYellow)//æ∆ø¿¿ß
                {
                    yellowCount++;
                    if (board.allTiles[col + 1, row + 2] != null && board.allTiles[col + 1, row + 2].GetComponent<Tiles>().isYellow) // æ∆ø¿¿ß¿ß
                    {
                        yellowCount++;
                        if (board.allTiles[col, row + 2] != null && board.allTiles[col, row + 2].GetComponent<Tiles>().isYellow) // æ∆ø¿¿ß¿ßøﬁ
                        {
                            yellowCount++;
                        }
                    }
                }
            }
        }
    }

    void YellowLeft(int col, int row)
    {
        var obj = board.allTiles[col, row];
        Debug.Log(obj.name);
        if (obj != null && board.allTiles[col, row].GetComponent<Tiles>().isYellow) // øﬁ
        {
            yellowCount++;
            if (board.allTiles[col, row + 1] != null && board.allTiles[col, row + 1].GetComponent<Tiles>().isYellow) //øﬁ¿ß
            {
                isTurnRight = true;
                yellowCount++;
                if (board.allTiles[col + 1, row + 1] != null && board.allTiles[col + 1, row + 1].GetComponent<Tiles>().isYellow)//øﬁ¿ßø¿
                {
                    yellowCount++;
                    if (board.allTiles[col + 2, row + 1] != null && board.allTiles[col + 2, row + 1].GetComponent<Tiles>().isYellow) // øﬁ¿ßø¿ø¿
                    {
                        yellowCount++;
                        if (board.allTiles[col + 2, row] != null && board.allTiles[col + 2, row].GetComponent<Tiles>().isYellow) // øﬁ¿ßø¿ø¿æ∆
                        {
                            yellowCount++;
                        }
                    }
                }
            }
            else if (board.allTiles[col, row - 1] != null && board.allTiles[col, row - 1].GetComponent<Tiles>().isYellow)//øﬁæ∆
            {
                isTurnLeft = true;
                yellowCount++;
                if (board.allTiles[col + 1, row - 1] != null && board.allTiles[col + 1, row - 1].GetComponent<Tiles>().isYellow)//øﬁæ∆ø¿
                {
                    yellowCount++;
                    if (board.allTiles[col + 2, row - 1] != null && board.allTiles[col + 2, row - 1].GetComponent<Tiles>().isYellow) // øﬁæ∆ø¿ø¿
                    {
                        yellowCount++;
                        if (board.allTiles[col + 2, row] != null && board.allTiles[col + 2, row].GetComponent<Tiles>().isYellow) // øﬁæ∆ø¿ø¿¿ß
                        {
                            yellowCount++;
                        }
                    }
                }
            }
        }
    }

    void YellowRight(int col, int row)
    {
        var obj = board.allTiles[col, row];
        if (obj != null && board.allTiles[col, row].GetComponent<Tiles>().isYellow) // ø¿
        {
            yellowCount++;
            if (board.allTiles[col, row + 1] != null && board.allTiles[col, row + 1].GetComponent<Tiles>().isYellow) //ø¿¿ß
            {
                isTurnLeft = true;
                yellowCount++;
                if (board.allTiles[col - 1, row + 1] != null && board.allTiles[col - 1, row + 1].GetComponent<Tiles>().isYellow)//ø¿¿ßøﬁ
                {
                    yellowCount++;
                    if (board.allTiles[col - 2, row + 1] != null && board.allTiles[col - 2, row + 1].GetComponent<Tiles>().isYellow) // ø¿¿ßøﬁøﬁ
                    {
                        yellowCount++;
                        if (board.allTiles[col - 2, row] != null && board.allTiles[col -2, row].GetComponent<Tiles>().isYellow) // ø¿¿ßøﬁøﬁæ∆
                        {
                            yellowCount++;
                        }
                    }
                }
            }
            else if (board.allTiles[col, row - 1] != null && board.allTiles[col, row - 1].GetComponent<Tiles>().isYellow)//ø¿æ∆
            {
                isTurnRight = true;
                yellowCount++;
                if (board.allTiles[col - 1, row - 1] != null && board.allTiles[col - 1, row - 1].GetComponent<Tiles>().isYellow)//ø¿æ∆øﬁ
                {
                    yellowCount++;
                    if (board.allTiles[col - 2, row - 1] != null && board.allTiles[col - 2, row - 1].GetComponent<Tiles>().isYellow) // ø¿æ∆øﬁøﬁ
                    {
                        yellowCount++;
                        if (board.allTiles[col - 2, row] != null && board.allTiles[col - 2, row].GetComponent<Tiles>().isYellow) // ø¿æ∆øﬁøﬁ¿ß
                        {
                            yellowCount++;
                        }
                    }
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
