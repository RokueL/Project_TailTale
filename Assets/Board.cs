using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int Width;
    public int Height;

    public GameObject battery;
    public GameObject cannon;
    public GameObject hose;
    public GameObject fire;
    public GameObject ends;

    public GameObject[,] allTiles;

    // Start is called before the first frame update
    void Start()
    {
        allTiles = new GameObject[Width, Height];
        SetUp();
        battery = allTiles[0, 3];
        battery.GetComponent<Tiles>().objectType = Tiles.ObejctType.Object;
        battery.GetComponent<SpriteRenderer>().color = new Color32(0, 102, 255, 255);
        battery.gameObject.tag = "Water";

        hose = allTiles[3, 3];
        hose.GetComponent<Tiles>().objectType = Tiles.ObejctType.End;
        hose.GetComponent<SpriteRenderer>().color = Color.gray;
        hose.gameObject.tag = "Hose";

        var fire1 = allTiles[9, 3];
        fire1.GetComponent<Tiles>().objectType = Tiles.ObejctType.Object;
        fire1.GetComponent<SpriteRenderer>().color = new Color32(255, 102, 102, 255);
        fire1.gameObject.tag = "Fire";

        //fire = allTiles[0, 7];
        //fire.GetComponent<Tiles>().objectType = Tiles.ObejctType.Object;
        //fire.GetComponent<SpriteRenderer>().color = new Color32(153,0,0,255);
        //fire.gameObject.tag = "Fire";

        //cannon = allTiles[2, 6];
        //cannon.GetComponent<Tiles>().objectType = Tiles.ObejctType.End;
        //cannon.GetComponent<SpriteRenderer>().color = Color.gray;
        //cannon.gameObject.tag = "Cannon";


        //var rock = allTiles[8, 7];
        //rock.GetComponent<Tiles>().objectType = Tiles.ObejctType.Rock;
        //rock.GetComponent<SpriteRenderer>().color = new Color32(175,47,0,255);
        //rock.gameObject.tag = "Rock";


        var block1 = allTiles[7, 7];
        block1.GetComponent<Tiles>().objectType = Tiles.ObejctType.Block;
        block1.GetComponent<SpriteRenderer>().color = Color.green;
        block1.gameObject.tag = "Block";

        var block2 = allTiles[7, 1];
        block2.GetComponent<Tiles>().objectType = Tiles.ObejctType.Block;
        block2.GetComponent<SpriteRenderer>().color = Color.green;
        block2.gameObject.tag = "Block";

    }

    void SetUp()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                Vector2 tempVec2 = new Vector2(i, j);
                var tileSet = ObjectPoolManager.Instance.tilePool.Get();
                tileSet.transform.position = tempVec2;
                tileSet.transform.parent = this.transform;
                tileSet.name = "( " + i + " , " + j + " )";
                allTiles[i, j] = tileSet;
                tileSet.GetComponent<Tiles>().col = i;
                tileSet.GetComponent<Tiles>().row = j;
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
