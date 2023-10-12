using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 curPos;
    Vector2 nextPos;

    float moveSpeed = 1f;
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        curPos = transform.position;
        GameManager.Inputs.KeyAction -= OnKeyboard;
        GameManager.Inputs.KeyAction += OnKeyboard;
    }

    void OnKeyboard()
    {
        //horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.W))
            vertical = 1;
        else if(Input.GetKey(KeyCode.S))
            vertical = -1;
        else
            vertical = 0;

        if (Input.GetKey(KeyCode.D))
            horizontal = 1;
        else if (Input.GetKey(KeyCode.A))
            horizontal = -1;
        else
            horizontal = 0;

        nextPos = new Vector2(horizontal, vertical).normalized * Time.deltaTime ;

        this.transform.position = curPos + nextPos;
    }

    // Update is called once per frame
    void Update()
    {
        curPos = transform.position;
    }
}
