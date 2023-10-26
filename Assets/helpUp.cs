using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class helpUp : MonoBehaviour
{
    RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        rect = this.gameObject.GetComponent<RectTransform>();
        rect.DOAnchorPosY(300f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
