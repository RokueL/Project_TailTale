using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineCreate : MonoBehaviour
{
    bool isRed;
    bool isBlue;
    bool isYellow;

    bool isOut;

    Board board;

    List<GameObject> RedPaints = new List<GameObject>();
    List<GameObject> BluePaints = new List<GameObject>();
    List<GameObject> YellowPaints = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }


    public void RedCheck(int col, int row)
    {
        //yield return new WaitForSeconds(.5f);
        var objects = board.allTiles[col,row];
        if (col > 0 && col < board.Width - 1)
        {
            GameObject left = board.allTiles[col - 1,row];
            GameObject right = board.allTiles[col + 1,row];
            var lefts = left.GetComponent<Tiles>();
            if (lefts.isRed)
            {
                if (lefts.isConnect == false)
                {
                    lefts.isConnect = true;
                    RedCheck(col - 1, row);
                }
                else
                {
                    RedCheck(col - 1, row);
                }
            }
            if(lefts.objectType == Tiles.ObejctType.End)
            {
                lefts.isConnect = true;
                lefts.typeAct();
            }
            else
            {
            }

        }
        else if (col == board.Width - 1)
        {
        }
        else if (col == 0)
        {

        }

        
    }

    /*
    IEnumerator redCheck(int col, int row)
    {
        Debug.Log("Start");
        var tiles = board.allTiles[col, row];
        if (col > 0 && col < board.Width - 1)
        {
            GameObject left = board.allTiles[col - 1, row];
            GameObject right = board.allTiles[col + 1, row];

            var lefts = left.GetComponent<Tiles>();
            var rights = right.GetComponent<Tiles>();

            if (lefts.isRed && !lefts.isConnect)
            {
                lefts.isConnect = true;
                RedCheck(col - 1, row);
            }
            else if(lefts.objectType == Tiles.ObejctType.End)
            {
                Debug.Log("Find ENd");
                lefts.GetComponent<SpriteRenderer>().color = Color.gray;
                lefts.isConnect = true;
                lefts.typeAct();
            }
            else if(rights.isRed && !rights.isConnect)
            {
                rights.isConnect = true;
                if (rights.objectType == Tiles.ObejctType.End)
                {
                    rights.typeAct();
                }
                else
                {
                    RedCheck(col + 1, row);
                }
            }
        }
        if (col == 0)
        {
            GameObject right = board.allTiles[col + 1, row];
            var rights = right.GetComponent<Tiles>();
            if (rights.isRed && !rights.isConnect)
            {
                rights.isConnect = true;
                if (rights.objectType == Tiles.ObejctType.End)
                {
                    rights.typeAct();
                }
                else
                {
                    RedCheck(col + 1, row);
                }
            }
        }
        if(col == board.Width - 1)
        {
            GameObject left = board.allTiles[col - 1, row];
            var lefts = left.GetComponent<Tiles>();
            if (lefts.isRed && !lefts.isConnect)
            {
                lefts.isConnect = true;
                if (lefts.objectType == Tiles.ObejctType.End)
                {
                    lefts.typeAct();
                }
                else
                {
                    RedCheck(col - 1, row);
                }
            }
        }
        else
        {
            Debug.Log("Last Tile : " +  col + ", " + row);
            Debug.Log("Col not Found");
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if( row > 0 && row < board.Height - 1)
        {
            GameObject up = board.allTiles[col, row + 1];
            GameObject down = board.allTiles[col, row - 1];

            var ups = up.GetComponent<Tiles>();
            var downs = down.GetComponent<Tiles>();

            if (ups.isRed && !ups.isConnect)
            {
                ups.isConnect = true;
                if (ups.objectType == Tiles.ObejctType.End)
                {
                    ups.typeAct();
                }
                else
                {
                    Debug.Log(col + "+" + row);
                    RedCheck(col, row + 1);
                }
            }
            else if (downs.isRed && !downs.isConnect)
            {
                downs.isConnect = true;
                if (downs.objectType == Tiles.ObejctType.End)
                {
                   downs.typeAct();
                }
                else
                {
                    RedCheck(col, row - 1);
                }
            }
        }
        if (row == 0)
        {
            GameObject up = board.allTiles[col, row + 1 ];
            var ups = up.GetComponent<Tiles>();
            if (ups.isRed && !ups.isConnect)
            {
                ups.isConnect = true;
                if (ups.objectType == Tiles.ObejctType.End)
                {
                    ups.typeAct();
                }
                else
                {
                    RedCheck(col, row + 1);
                }
            }
        }
        if (row == board.Height - 1)
        {
            GameObject down = board.allTiles[col, row - 1];
            var downs = down.GetComponent<Tiles>();
            if (downs.isRed && !downs.isConnect)
            {
                downs.isConnect = true;
                if (downs.objectType == Tiles.ObejctType.End)
                {
                    downs.typeAct();
                }
                else
                {
                    RedCheck(col, row - 1);
                }
            }
        }
        else
        {
            Debug.Log("Last Tile : " + col + ", " + row);
            Debug.Log("Col not Found");
        }
        yield return null;
    }

    */

    IEnumerator BlueLineCheck()
    {
        for (int i = 0; i < board.Width; i++)
        {
            for (int j = 0; j < board.Height; j++)
            {
                var tiles = board.allTiles[i, j];
                if (tiles.GetComponent<Tiles>().isBlue)
                {
                    if (!BluePaints.Contains(tiles))
                        BluePaints.Add(tiles);
                }
            }
        }
        yield return new WaitForSeconds(.5f);
    }
    IEnumerator YellowLineCheck()
    {
        for (int i = 0; i < board.Width; i++)
        {
            for (int j = 0; j < board.Height; j++)
            {
                var tiles = board.allTiles[i, j];
                if (tiles.GetComponent<Tiles>().isYellow)
                {
                    if (!YellowPaints.Contains(tiles))
                        YellowPaints.Add(tiles);
                }
            }
        }
        yield return new WaitForSeconds(.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
