using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mouth : MonoBehaviour
{
    LineRenderer wave;
    public float _widthMultiplier;

    [Range(5, 100)]
    public int _maxScale = 5;

    public float xShift;
    public float yShift;

    [Range(1, 4)]
    public float xDistance = 1;

    [Range(10, 100)]
    public int count = 45;

    Vector3 position;


    // Start is called before the first frame update
    void Start()
    {
        
        wave = GetComponent<LineRenderer>();
        wave.widthMultiplier = _widthMultiplier;
        wave.positionCount = count; //length of samples in AudioPeer
        
        transform.localPosition = new Vector3(0,0,0);
        
        /*for (int i = 0; i < wave.positionCount; i++)
        {

            position = transform.localPosition + (new Vector3(i + xShift, (AudioPeer._audioBand[i % 8] * _maxScale) + yShift, 0));
            Debug.Log(position);
            wave.SetPosition(i, position);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        Vector3 position;
        wave.positionCount = count;
        for (int i = 0; i < wave.positionCount; i++)
        {
            int number = (int)Random.Range(0,8);
            yShift = (int)Random.Range(2, 4);
            
            position = transform.position +  new Vector3((i*xDistance) + xShift, (AudioPeer._audioBand[number] * _maxScale) + yShift + (i/2), 0);
            Debug.Log(position);
            wave.SetPosition(i, position);
        }
    }
}
