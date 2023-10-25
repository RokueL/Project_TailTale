using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        static GameManager s_instance; // ���ϼ��� ����ȴ�.
        static GameManager Instance { get { Init(); return s_instance; } } // ������ �Ŵ����� ����´�.

        InputManager _inputManager = new InputManager();
        public static InputManager Inputs { get { return Instance._inputManager; } }

        void Start()
        {
            Init();
        }

        void Update()
        {
            _inputManager.keyUpdate();
        }

        static void Init()
        {
            if (s_instance == null)
            {
                GameObject go = GameObject.Find("@Managers");
                if (go == null)
                {
                    //�������Ʈ�� �����Ѵ�.
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<GameManager>();
                }

                // ������� �߰��ϰ� ������ �� ������ ��.
                //DontDestroyOnLoad(go);
                s_instance = go.GetComponent<GameManager>();
            }
        }


    }
}