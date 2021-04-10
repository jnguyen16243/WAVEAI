using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMovement : MonoBehaviour
{

    float timeCounter = 0;
    float speed;
    float width;
    float height;
    float maxSpeed;
    float minSpeed;
    public float _radius;
    public int _band;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1.5f;
        maxSpeed = 1.5f;
        minSpeed = 0.25f;
        width = 10f;
        height = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        speed = AudioPeer._audioBand[_band] * 5f;
        //Debug.Log("Speed: " + speed);
        if (speed <= 0.2f)
        {
            speed = 0.2f;
        }
        timeCounter += Time.deltaTime * speed;
        float x = Mathf.Cos(timeCounter) * _radius;
        float y = Mathf.Cos(timeCounter * 8f);
        float z = Mathf.Sin(timeCounter) * (_radius / 2f);
        transform.position = new Vector3(x, y, z);
    }
}
