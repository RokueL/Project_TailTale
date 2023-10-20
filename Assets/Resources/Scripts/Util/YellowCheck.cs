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
            Debug.Log("���ο� üũ");
            YellowUp(col, row + 1);
            if (yellowCount == 3)
                {
                    if (isTurnLeft)
                    {
                        Debug.Log("�������� ������");
                    }
                    else if (isTurnRight)
                    {
                        Debug.Log("���������� ������");
                    }
                }
                else if (yellowCount == 5)
                {
                    Debug.Log("������");
                }
            }



    }

    void YellowUp(int col, int row)
    {
        Debug.Log("�� üũ");
        var obj = board.allTiles[col, row];
        Debug.Log(obj.name);
        if (obj != null && board.allTiles[col, row].GetComponent<Tiles>().isYellow) // ��
        {
            Debug.Log("�� ���");
            yellowCount++;
            if (board.allTiles[col - 1, row] != null && board.allTiles[col - 1, row].GetComponent<Tiles>().isYellow) //����
            {
                isTurnLeft = true;
                yellowCount++;
                Debug.Log("���� ���");
                if (board.allTiles[col - 1, row -1 ] != null && board.allTiles[col - 1, row - 1].GetComponent<Tiles>().isYellow)//���޾�
                {
                    yellowCount++;
                    Debug.Log("���޾� ���");
                    if (board.allTiles[col - 1, row - 2] != null && board.allTiles[col - 1, row - 2].GetComponent<Tiles>().isYellow) // ���޾ƾ�
                    {
                        yellowCount++;
                        if(board.allTiles[col, row - 2] != null && board.allTiles[col, row - 2].GetComponent<Tiles>().isYellow) // ���޾ƾƿ�
                        {
                            yellowCount++;
                        }
                    }
                }
            }
            else if (board.allTiles[col + 1, row] != null && board.allTiles[col + 1, row].GetComponent<Tiles>().isYellow)//����
            {
                if (board.allTiles[col + 1, row - 1] != null && board.allTiles[col + 1, row - 1].GetComponent<Tiles>().isYellow)//������
                {
                    isTurnRight = true;
                    yellowCount++;
                    if (board.allTiles[col + 1, row - 2] != null && board.allTiles[col - 1, row - 2].GetComponent<Tiles>().isYellow) // �����ƾ�
                    {
                        yellowCount++;
                        if (board.allTiles[col, row - 2] != null && board.allTiles[col, row - 2].GetComponent<Tiles>().isYellow) // ���޾ƾƿ�
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
