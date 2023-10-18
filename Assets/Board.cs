using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int Width;
    public int Height;

    public GameObject objects;
    public GameObject ends;

    public GameObject[,] allTiles;

    // Start is called before the first frame update
    void Start()
    {
        allTiles = new GameObject[Width, Height];
        SetUp();
        objects = allTiles[3, 3];
        objects.GetComponent<Tiles>().objectType = Tiles.ObejctType.Object;
        objects.GetComponent<SpriteRenderer>().color = Color.green;

        ends = allTiles[0, 3];
        ends.GetComponent<Tiles>().objectType = Tiles.ObejctType.End;
        ends.GetComponent<SpriteRenderer>().color = Color.black;
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
