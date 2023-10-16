using Manager;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 curPos;
    Vector2 nextPos;

    float moveSpeed = 3f;
    float horizontal;
    float vertical;

    float maxPaintGage = 10f;
    float myPaintGage;

    int interactCount;

    bool isEnter;
    bool isPaint;
    bool isDraw;

    GameObject myTail;
    GameObject enterObject;
    Color enterColor;
    Color originColor;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Paint")
        {
            isEnter = true;
            interactCount++;
            enterObject = collision.gameObject;
            enterColor = enterObject.GetComponent<SpriteRenderer>().color;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Paint")
        {
            isEnter = false;
            enterObject = null;
        }
    }

    void ColorTail()
    {
        var tail = myTail.GetComponent<SpriteRenderer>();
        if (interactCount > 0)
        {
            tail.color = enterColor;
            myPaintGage = maxPaintGage;
            isPaint = true;
            isDraw = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        originColor = GetComponent<SpriteRenderer>().color;
        myTail = GameObject.Find("Tail");
        curPos = transform.position;
        GameManager.Inputs.KeyAction -= OnKeyboard;
        GameManager.Inputs.KeyAction += OnKeyboard;
    }

    void OnKeyboard()
    {
        // 페인트 줍기
        if(isEnter && enterObject != null)
        {
            if(Input.GetKeyDown(KeyCode.F)) 
            {
                ColorTail();
                interactCount--;
            }
        }
        //이동만이에용
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

    IEnumerator drawDelay()
    {
        isDraw = false;
        yield return new WaitForSeconds(.3f);
        isDraw = true;
    }


    // Update is called once per frame
    void Update()
    {
        curPos = transform.position;

        if (isPaint && myPaintGage > 0)
        {
            if (Input.GetMouseButton(1))
            {
                if (isDraw)
                {
                    myPaintGage -= .5f;
                    Debug.Log(myPaintGage);
                    var paints = ObjectPoolManager.Instance.PaintPool.Get();
                    paints.transform.position = this.transform.position;

                    Color alpha = myTail.GetComponent<SpriteRenderer>().color;
                    paints.GetComponent<SpriteRenderer>().color = alpha;
                    alpha.a = 0.3f;
                    paints.GetComponent<SpriteRenderer>().color = alpha;

                    StartCoroutine(drawDelay());
                }
            }
        }

        if(myPaintGage <= 0)
        {
            myTail.GetComponent<SpriteRenderer>().color = originColor;
            myPaintGage = 0;
            isPaint = false;
        }
    }
}
