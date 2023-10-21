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


    bool isNotDisconnect;

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
        redCheck.AllReset();
        for(int i = 0; i < board.Width; i++)
        {
            for (int j = 0; j < board.Height; j++)
            {
                var a = board.allTiles[i, j].GetComponent<Tiles>();
                if (a.objectType == Tiles.ObejctType.Object)  // 배터리 0 불 1 물 2
                {
                    if (a.gameObject.tag == "Battery")  // 이동 가능, 빨간색 체크 필요
                    {
                        if (playerController.myBlue == 0)
                        {
                            blueCheck.BlueCheckStart(i, j, 0, i, j);
                        }
                        redCheck.RedCheckStart(i, j, 0);
                    }
                    else if (a.gameObject.tag == "Fire")   // 빨간색 체크 필요
                    {
                        redCheck.RedCheckStart(i, j, 1);
                    }
                    else if (a.gameObject.tag == "Water")  // 이동 가능, 빨간색 체크 필요
                    {
                        if (playerController.myBlue == 0)
                        {
                            blueCheck.BlueCheckStart(i, j, 2, i, j);
                        }
                        redCheck.RedCheckStart(i, j, 2);
                    }
                }


                if(a.objectType == Tiles.ObejctType.End) // 캐논 3 호스 4
                {
                    if (a.gameObject.tag == "Cannon")   // 회전 가능, 이동 가능
                    {
                        if (playerController.myBlue == 0)
                        {
                            Debug.Log("Cannon Start");
                            blueCheck.BlueCheckStart(i, j, 3, i, j);
                        }
                        yellowCheck.YellowRotation(i,j);
                    }
                    if (a.gameObject.tag == "Hose")   // 회전 가능, 이동 가능
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

        for (int i = 0; i < board.Width; i++)
        {
            for (int j = 0; j < board.Height; j++)
            {
                var a = board.allTiles[i, j].GetComponent<Tiles>();
                if (a.objectType == Tiles.ObejctType.End)
                {
                    if (a.isConnect)
                    {
                        DisconnectEnds(i, j);
                    }
                }
            }
        }
    }

    void DisconnectEnds(int col, int row)
    {
        var a = board.allTiles[col, row].GetComponent<Tiles>();
        isNotDisconnect = false;
        if (row > 0 && row < board.Height - 1)
        {
            GameObject up = board.allTiles[col, row + 1];
            GameObject down = board.allTiles[col, row - 1];
            var ups = up.GetComponent<Tiles>();
            var downs = down.GetComponent<Tiles>();
            if (ups.isRed)
            {
                if (ups.isConnect)
                {
                    isNotDisconnect = true;
                }
            }
            else if (downs.isRed)
            {
                if (downs.isConnect)
                {
                    isNotDisconnect = true;
                }
            }
            if (ups.objectType == Tiles.ObejctType.Object || downs.objectType == Tiles.ObejctType.Object)
            {
                isNotDisconnect = true;
            }
        }
        else if (row == 0)
        {
            GameObject up = board.allTiles[col, row + 1];
            var ups = up.GetComponent<Tiles>();
            if (ups.isRed)
            {
                if (ups.isConnect)
                {
                    isNotDisconnect = true;
                }
            }
            else if (ups.objectType == Tiles.ObejctType.Object)
            {
                isNotDisconnect = true;
            }
        }

        else if (row == board.Height - 1)
        {
            GameObject down = board.allTiles[col, row - 1];
            var downs = down.GetComponent<Tiles>();
            if (downs.isRed)
            {
                if (downs.isConnect)
                {
                    isNotDisconnect = true;
                }
            }
            else if (downs.objectType == Tiles.ObejctType.Object)
            {
                isNotDisconnect = true;
            }
        }
        ////////////////////////////////////////////
        if (col > 0 && col < board.Width - 1)
        {
            GameObject left = board.allTiles[col - 1, row];
            GameObject right = board.allTiles[col + 1, row];
            var lefts = left.GetComponent<Tiles>();
            var rights = right.GetComponent<Tiles>();
            if (lefts.isRed)
            {
                if (lefts.isConnect)
                {
                    isNotDisconnect = true;
                }
            }
            if (rights.isRed)
            {
                if (rights.isConnect)
                {
                    isNotDisconnect = true;
                }
            }
            if (lefts.objectType == Tiles.ObejctType.Object || rights.objectType == Tiles.ObejctType.Object)
            {
                isNotDisconnect = true;
            }
        }
        else if (col == 0)
        {
            GameObject right = board.allTiles[col + 1, row];
            var rights = right.GetComponent<Tiles>();
            if (rights.isRed)
            {
                if (rights.isConnect)
                {
                    isNotDisconnect = true;
                }
            }
            if (rights.objectType == Tiles.ObejctType.Object)
            {
                isNotDisconnect = true;
            }
        }
        else if (col == board.Width - 1)
        {
            GameObject left = board.allTiles[col - 1, row];
            var lefts = left.GetComponent<Tiles>();
            if (lefts.isRed)
            {
                if (lefts.isConnect)
                {
                    isNotDisconnect = true;
                }
            }
            else if (lefts.objectType == Tiles.ObejctType.Object)
            {
                isNotDisconnect = true;
            }
        }

        if (!isNotDisconnect)
        {
            a.isConnect = false;
            a.typeAct();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
