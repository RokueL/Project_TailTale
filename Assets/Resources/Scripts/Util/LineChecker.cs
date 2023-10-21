using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineChecker : MonoBehaviour
{
    RedCheck redCheck;
    BlueCheck blueCheck;
    YellowCheck yellowCheck;

    Board board;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        redCheck = FindObjectOfType<RedCheck>();
        blueCheck = FindObjectOfType<BlueCheck>();
        yellowCheck = FindObjectOfType<YellowCheck>();

        board = FindObjectOfType<Board>();
        playerController = FindObjectOfType<PlayerController>();
    }

    public void FindObject()
    {
        StartCoroutine(findObject());
    }

    IEnumerator findObject() 
    {
        yield return new WaitForSeconds(.1f);
        for(int i = 0; i < board.Width; i++)
        {
            for (int j = 0; j < board.Height; j++)
            {
                var a = board.allTiles[i, j].GetComponent<Tiles>();
                if (a.objectType == Tiles.ObejctType.Object)  // ���͸� 0 �� 1 �� 2
                {
                    if (a.gameObject.tag == "Battery")  // �̵� ����, ������ üũ �ʿ�
                    {
                        if (playerController.myBlue == 0)
                        {
                            blueCheck.BlueCheckStart(i, j, 0, i, j);
                        }
                        redCheck.RedCheckStart(i, j, 0);
                    }
                    else if (a.gameObject.tag == "Fire")   // ������ üũ �ʿ�
                    {
                        redCheck.RedCheckStart(i, j, 1);
                    }
                    else if (a.gameObject.tag == "Water")  // �̵� ����, ������ üũ �ʿ�
                    {
                        if (playerController.myBlue == 0)
                        {
                            blueCheck.BlueCheckStart(i, j, 2, i, j);
                        }
                        redCheck.RedCheckStart(i, j, 2);
                    }
                }


                if(a.objectType == Tiles.ObejctType.End) // ĳ�� 3 ȣ�� 4
                {
                    if (a.gameObject.tag == "Cannon")   // ȸ�� ����, �̵� ����
                    {
                        if (playerController.myBlue == 0)
                        {
                            Debug.Log("Cannon Start");
                            blueCheck.BlueCheckStart(i, j, 3, i, j);
                        }
                        yellowCheck.YellowRotation(i,j);
                    }
                    if (a.gameObject.tag == "Hose")   // ȸ�� ����, �̵� ����
                    {
                        if (playerController.myBlue == 0)
                        {
                            blueCheck.BlueCheckStart(i, j, 4, i, j);
                        }
                        yellowCheck.YellowRotation(i, j);
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
