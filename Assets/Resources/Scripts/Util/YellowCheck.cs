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
        StartCoroutine(yellowRotation(col, row));
    }

    IEnumerator yellowRotation(int col, int row)
    {
        yield return new WaitForSeconds(.1f);
        yellowCount = 0;
        var obj = board.allTiles[col, row].GetComponent<Tiles>();
        if (obj.isUp)
        {
            Debug.Log("¿»·Î¿ì Ã¼Å©");
            YellowUp(col, row + 1);
            if (yellowCount == 3)
                {
                    if (isTurnLeft)
                    {
                        Debug.Log("¿ÞÂÊÀ¸·Î µ¹¾ÆÀ¯");
                    }
                    else if (isTurnRight)
                    {
                        Debug.Log("¿À¸¥ÂÊÀ¸·Î µ¹¾ÆÀ¯");
                    }
                }
                else if (yellowCount == 5)
                {
                    Debug.Log("µÚÁý¾î");
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
            Debug.Log("À­ ³ë¶û");
            yellowCount++;
            if (board.allTiles[col - 1, row] != null && board.allTiles[col - 1, row].GetComponent<Tiles>().isYellow) //À§¿Þ
            {
                isTurnLeft = true;
                yellowCount++;
                Debug.Log("À­¿Þ ³ë¶û");
                if (board.allTiles[col - 1, row -1 ] != null && board.allTiles[col - 1, row - 1].GetComponent<Tiles>().isYellow)//À§¿Þ¾Æ
                {
                    yellowCount++;
                    Debug.Log("À­¿Þ¾Æ ³ë¶û");
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
                if (board.allTiles[col + 1, row - 1] != null && board.allTiles[col + 1, row - 1].GetComponent<Tiles>().isYellow)//À§¿À¾Æ
                {
                    isTurnRight = true;
                    yellowCount++;
                    if (board.allTiles[col + 1, row - 2] != null && board.allTiles[col - 1, row - 2].GetComponent<Tiles>().isYellow) // À§¿À¾Æ¾Æ
                    {
                        yellowCount++;
                        if (board.allTiles[col, row - 2] != null && board.allTiles[col, row - 2].GetComponent<Tiles>().isYellow) // À§¿Þ¾Æ¾Æ¿Þ
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
