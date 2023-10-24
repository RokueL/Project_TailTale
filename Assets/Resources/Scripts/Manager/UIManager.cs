using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Canvas cvsEnd;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        cvsEnd.gameObject.SetActive(false);
    }

    public void GameClearEvent()
    {
        cvsEnd.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
