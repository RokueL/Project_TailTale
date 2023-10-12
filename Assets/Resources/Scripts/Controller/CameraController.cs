using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;

    Vector3 offset = new Vector3(0,0,-10f);
    public float cameraSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetVec = target.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetVec, Time.deltaTime * cameraSpeed);
    }
}
