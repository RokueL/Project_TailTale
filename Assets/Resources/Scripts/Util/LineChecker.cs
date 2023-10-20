using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineChecker : MonoBehaviour
{
    RedCheck redCheck;
    BlueCheck blueCheck;

    Board board;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        redCheck = FindObjectOfType<RedCheck>();
        blueCheck = FindObjectOfType<BlueCheck>();

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
                if (a.objectType == Tiles.ObejctType.Object)  // 배터리 0 불 1 물 2
                {
                    if (a.gameObject.tag == "Battery")
                    {
                        if (playerController.myBlue == 0)
                        {
                            blueCheck.BlueCheckStart(i, j, 0, i, j);
                        }
                        redCheck.RedCheckStart(i, j, 0);
                    }
                    else if (a.gameObject.tag == "Fire")
                    {
                        if (playerController.myBlue == 0)
                        {
                            blueCheck.BlueCheckStart(i, j, 1, i, j);
                        }
                        redCheck.RedCheckStart(i, j, 1);
                    }
                    else if (a.gameObject.tag == "Water")
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
                    if (a.gameObject.tag == "Cannon")
                    {
                        if (playerController.myBlue == 0)
                        {
                            Debug.Log("Cannon Start");
                            blueCheck.BlueCheckStart(i, j, 3, i, j);
                        }
                    }
                    if (a.gameObject.tag == "Hose")
                    {
                        if (playerController.myBlue == 0)
                        {
                            blueCheck.BlueCheckStart(i, j, 4, i, j);
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
