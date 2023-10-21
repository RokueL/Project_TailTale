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
            if (yellowCount == 3)
            {
                if (isTurnLeft)
                {
                    Debug.Log("¿ÞÂÊÀ¸·Î µ¹¾ÆÀ¯");
                    obj.isUp = false;
                    obj.isDown = false;
                    obj.isLeft = true;
                    obj.isRight = false;
                }
                else if (isTurnRight)
                {
                    Debug.Log("¿À¸¥ÂÊÀ¸·Î µ¹¾ÆÀ¯");
                    obj.isUp = false;
                    obj.isDown = false;
                    obj.isLeft = false;
                    obj.isRight = true;
                }
            }
            else if (yellowCount == 5)
            {
                Debug.Log("µÚÁý¾î");
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
                }
            }
        }
        
        else if(obj.isIndexDown)
        {
            YellowDown(col, row - 1);
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
                Debug.Log("µÚÁý¾î");
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
            }
        }
        else if (obj.isIndexLeft)
        {
            YellowLeft(col - 1, row);
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
            }
        }
        else if (obj.isIndexRight)
        {
            YellowRight(col + 1, row);
            if (yellowCount == 3)
            {
                if (isTurnLeft)
                {
                    Debug.Log("¿ÞÂÊÀ¸·Î µ¹¾ÆÀ¯");
                    obj.isUp = true;
                    obj.isDown = false;
                    obj.isLeft = false;
                    obj.isRight = false;
                }
                else if (isTurnRight)
                {
                    Debug.Log("¿À¸¥ÂÊÀ¸·Î µ¹¾ÆÀ¯");
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
            }
        }
    }

    void YellowUp(int col, int row)
    {
        Debug.Log("¾÷ Ã¼Å©");
        var obj = board.allTiles[col, row];
        Debug.Log(obj.name);
        if (obj != null && board.allTiles[col, row].GetComponent<Tiles>().isYellow) // À§
        {
            yellowCount++;
            if (board.allTiles[col - 1, row] != null && board.allTiles[col - 1, row].GetComponent<Tiles>().isYellow) //À§¿Þ
            {
                isTurnLeft = true;
                yellowCount++;
                if (board.allTiles[col - 1, row -1 ] != null && board.allTiles[col - 1, row - 1].GetComponent<Tiles>().isYellow)//À§¿Þ¾Æ
                {
                    yellowCount++;
                    if (board.allTiles[col - 1, row - 2] != null && board.allTiles[col - 1, row - 2].GetComponent<Tiles>().isYellow) // À§¿Þ¾Æ¾Æ
                    {
                        yellowCount++;
                        if(board.allTiles[col, row - 2] != null && board.allTiles[col, row - 2].GetComponent<Tiles>().isYellow) // À§¿Þ¾Æ¾Æ¿À
                        {
                            yellowCount++;
                        }
                    }
                }
            }
            else if (board.allTiles[col + 1, row] != null && board.allTiles[col + 1, row].GetComponent<Tiles>().isYellow)//À§¿À
            {
                isTurnRight = true;
                yellowCount++;
                Debug.Log(yellowCount);
                if (board.allTiles[col + 1, row - 1] != null && board.allTiles[col + 1, row - 1].GetComponent<Tiles>().isYellow)//À§¿À¾Æ
                {
                    yellowCount++;

                    Debug.Log(yellowCount);
                    if (board.allTiles[col + 1, row - 2] != null && board.allTiles[col + 1, row - 2].GetComponent<Tiles>().isYellow) // À§¿À¾Æ¾Æ
                    {
                        yellowCount++;

                        Debug.Log(yellowCount);
                        if (board.allTiles[col, row - 2] != null && board.allTiles[col, row - 2].GetComponent<Tiles>().isYellow) // À§¿Þ¾Æ¾Æ¿Þ
                        {
                            yellowCount++;

                            Debug.Log(yellowCount);
                        }
                    }
                }
            }
        }
    }

    void YellowDown(int col, int row) //´Ù¿î °ªÀ» °¡Á®¿È
    {
        Debug.Log("´Ù¿î Ã¼Å©");
        var obj = board.allTiles[col, row];
        Debug.Log(obj.name);
        if (obj != null && board.allTiles[col, row].GetComponent<Tiles>().isYellow) // ¾Æ·¡
        {
            yellowCount++;
            if (board.allTiles[col - 1, row] != null && board.allTiles[col - 1, row].GetComponent<Tiles>().isYellow) //¾Æ¿Þ
            {
                isTurnLeft = true;
                yellowCount++;
                if (board.allTiles[col - 1, row + 1] != null && board.allTiles[col - 1, row + 1].GetComponent<Tiles>().isYellow)//¾Æ¿ÞÀ§
                {
                    yellowCount++;
                    if (board.allTiles[col - 1, row + 2] != null && board.allTiles[col - 1, row + 2].GetComponent<Tiles>().isYellow) // ¾Æ¿ÞÀ§À§
                    {
                        yellowCount++;
                        if (board.allTiles[col, row + 2] != null && board.allTiles[col, row + 2].GetComponent<Tiles>().isYellow) // ¾Æ¿ÞÀ§À§¿À
                        {
                            yellowCount++;
                        }
                    }
                }
            }
            else if (board.allTiles[col + 1, row] != null && board.allTiles[col + 1, row].GetComponent<Tiles>().isYellow)//¾Æ¿À
            {
                isTurnRight = true;
                yellowCount++;
                if (board.allTiles[col + 1, row + 1] != null && board.allTiles[col + 1, row + 1].GetComponent<Tiles>().isYellow)//¾Æ¿ÀÀ§
                {
                    yellowCount++;
                    if (board.allTiles[col + 1, row + 2] != null && board.allTiles[col + 1, row + 2].GetComponent<Tiles>().isYellow) // ¾Æ¿ÀÀ§À§
                    {
                        yellowCount++;
                        if (board.allTiles[col, row + 2] != null && board.allTiles[col, row + 2].GetComponent<Tiles>().isYellow) // ¾Æ¿ÀÀ§À§¿Þ
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
        Debug.Log("¿Þ Ã¼Å©");
        var obj = board.allTiles[col, row];
        Debug.Log(obj.name);
        if (obj != null && board.allTiles[col, row].GetComponent<Tiles>().isYellow) // ¿Þ
        {
            yellowCount++;
            if (board.allTiles[col, row + 1] != null && board.allTiles[col, row + 1].GetComponent<Tiles>().isYellow) //¿ÞÀ§
            {
                isTurnRight = true;
                yellowCount++;
                if (board.allTiles[col + 1, row + 1] != null && board.allTiles[col + 1, row + 1].GetComponent<Tiles>().isYellow)//¿ÞÀ§¿À
                {
                    yellowCount++;
                    if (board.allTiles[col + 2, row + 1] != null && board.allTiles[col + 2, row + 1].GetComponent<Tiles>().isYellow) // ¿ÞÀ§¿À¿À
                    {
                        yellowCount++;
                        if (board.allTiles[col + 2, row] != null && board.allTiles[col + 2, row].GetComponent<Tiles>().isYellow) // ¿ÞÀ§¿À¿À¾Æ
                        {
                            yellowCount++;
                        }
                    }
                }
            }
            else if (board.allTiles[col, row - 1] != null && board.allTiles[col, row - 1].GetComponent<Tiles>().isYellow)//¿Þ¾Æ
            {
                isTurnLeft = true;
                yellowCount++;
                if (board.allTiles[col + 1, row - 1] != null && board.allTiles[col + 1, row - 1].GetComponent<Tiles>().isYellow)//¿Þ¾Æ¿À
                {
                    yellowCount++;
                    if (board.allTiles[col + 2, row - 1] != null && board.allTiles[col + 2, row - 1].GetComponent<Tiles>().isYellow) // ¿Þ¾Æ¿À¿À
                    {
                        yellowCount++;
                        if (board.allTiles[col + 2, row] != null && board.allTiles[col + 2, row].GetComponent<Tiles>().isYellow) // ¿Þ¾Æ¿À¿ÀÀ§
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
        Debug.Log("¿À¸¥ÂÊ Ã¼Å©");
        var obj = board.allTiles[col, row];
        Debug.Log(obj.name);
        if (obj != null && board.allTiles[col, row].GetComponent<Tiles>().isYellow) // ¿À
        {
            yellowCount++;
            if (board.allTiles[col, row + 1] != null && board.allTiles[col, row + 1].GetComponent<Tiles>().isYellow) //¿ÀÀ§
            {
                isTurnLeft = true;
                yellowCount++;
                if (board.allTiles[col - 1, row + 1] != null && board.allTiles[col - 1, row + 1].GetComponent<Tiles>().isYellow)//¿ÀÀ§¿Þ
                {
                    yellowCount++;
                    if (board.allTiles[col - 2, row + 1] != null && board.allTiles[col - 2, row + 1].GetComponent<Tiles>().isYellow) // ¿ÀÀ§¿Þ¿Þ
                    {
                        yellowCount++;
                        if (board.allTiles[col - 2, row] != null && board.allTiles[col -2, row].GetComponent<Tiles>().isYellow) // ¿ÀÀ§¿Þ¿Þ¾Æ
                        {
                            yellowCount++;
                        }
                    }
                }
            }
            else if (board.allTiles[col, row - 1] != null && board.allTiles[col, row - 1].GetComponent<Tiles>().isYellow)//¿À¾Æ
            {
                isTurnRight = true;
                yellowCount++;
                if (board.allTiles[col - 1, row - 1] != null && board.allTiles[col - 1, row - 1].GetComponent<Tiles>().isYellow)//¿À¾Æ¿Þ
                {
                    yellowCount++;
                    if (board.allTiles[col - 2, row - 1] != null && board.allTiles[col - 2, row - 1].GetComponent<Tiles>().isYellow) // ¿À¾Æ¿Þ¿Þ
                    {
                        yellowCount++;
                        if (board.allTiles[col - 2, row] != null && board.allTiles[col - 2, row].GetComponent<Tiles>().isYellow) // ¿À¾Æ¿Þ¿ÞÀ§
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
