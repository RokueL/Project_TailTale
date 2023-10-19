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
        battery.GetComponent<SpriteRenderer>().color = Color.green;
        battery.gameObject.tag = "Water";

        fire = allTiles[2, 8];
        fire.GetComponent<Tiles>().objectType = Tiles.ObejctType.Object;
        fire.GetComponent<SpriteRenderer>().color = Color.red;
        fire.gameObject.tag = "Fire";

        ends = allTiles[2, 3];
        ends.GetComponent<Tiles>().objectType = Tiles.ObejctType.End;
        ends.GetComponent<SpriteRenderer>().color = Color.black;
        ends.gameObject.tag = "Hose";

        cannon = allTiles[3, 0];
        cannon.GetComponent<Tiles>().objectType = Tiles.ObejctType.End;
        cannon.GetComponent<SpriteRenderer>().color = Color.blue;
        cannon.gameObject.tag = "Cannon";

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
