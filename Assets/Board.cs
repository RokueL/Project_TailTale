using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int Width;
    public int Height;

    public GameObject battery;
    public GameObject cannon;
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
        battery.GetComponent<SpriteRenderer>().color = new Color32(0,102,255,255);
        battery.gameObject.tag = "Water";

        cannon = allTiles[5, 3];
        cannon.GetComponent<Tiles>().objectType = Tiles.ObejctType.End;
        cannon.GetComponent<SpriteRenderer>().color = Color.gray;
        cannon.gameObject.tag = "Hose";

        //fire = allTiles[5, 7];
        //fire.GetComponent<Tiles>().objectType = Tiles.ObejctType.Object;
        //fire.GetComponent<SpriteRenderer>().color = Color.red;
        //fire.gameObject.tag = "Fire";

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
