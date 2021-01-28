using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    float _offset;
    [SerializeField] float offset = 3; 

    void Start()
    {
        if (!player)
        {
            Debug.Log("No player assigned to the camera!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        _offset = (player.transform.position.x + offset);
        transform.position = new Vector3(_offset, 5, -10);
    }
}
