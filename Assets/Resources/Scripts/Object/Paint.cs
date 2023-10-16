using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Datas;
using UnityEngine.Pool;

public class Paint : MonoBehaviour
{
    Data.PaintType paintType;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (name)
        {
            case "A":
                paintType = Data.PaintType.Red;
                spriteRenderer.color = Color.red;
                break;
            case "B":
                paintType = Data.PaintType.Blue;
                spriteRenderer.color = Color.blue;
                break; 
            case "C":
                paintType = Data.PaintType.Yellow;
                spriteRenderer.color = Color.yellow;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
