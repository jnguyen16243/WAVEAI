using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class Movement : MonoBehaviour
{
    float timeCounter = 0;
    float speed;
    float width;
    float height;
    float maxSpeed;
    float minSpeed;
    public float _radius;
    public int _band;
    private List<string> stringList;
    private List<string[]> parsedList;
    public List<float> xPoints;
    public List<float> yPoints;
    public bool usePlanes;
    
    int index;

    // Start is called before the first frame update
    void Start()
    {
        int index = 1;
        xPoints = new List<float>();
        yPoints = new List<float>();
        readTextFile();
        //printTextFile();
        speed = 1.5f;
        maxSpeed = 1.5f;
        minSpeed = 0.25f;
        width = 10f;
        height = 3f;
        transform.position = new Vector2(0, 0);
        FaceMovement();
        
    }

    // Update is called once per frame
    void Update()
    {

        //ringMovement();
        FaceMovement();

    }

    void RingMovement()
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
    void FaceMovement()
    {
        index++;
        if (index >= xPoints.Count - 1 || index >= yPoints.Count - 1)
        {
            index = 1;
        }
        

        float x = xPoints[index];
        float y = yPoints[index];
        Vector2 oldPosition = new Vector2(xPoints[index - 1], yPoints[index - 1]);
        Vector2 newPosition = new Vector3(x,y);
        Vector2 between = newPosition - oldPosition;
        Vector2 perpVector = Vector2.Perpendicular(between);
        Debug.Log(AudioPeer._AmplitudeBuffer);
        //perpVector = perpVector * AudioPeer._AmplitudeBuffer;
        //transform.position = Vector2.MoveTowards(transform.position, perpVector, 100f);
        
            transform.position = Vector2.MoveTowards(transform.position, newPosition, 100f);
        
        //    transform.position = Vector2.MoveTowards(transform.position, perpVector, 100f);
      
        
        
    }

    void readTextFile()
    {
        TextAsset DataCSV = Resources.Load<TextAsset>("Coordinates");
        string[] line = DataCSV.text.Split(new char[] { '\n' });
        for (int i = 1; i < line.Length - 1; i++)
        {
            string[] part = line[i].Split(new char[] { ',' });
            xPoints.Add(float.Parse(part[0], CultureInfo.InvariantCulture));
            yPoints.Add(float.Parse(part[1], CultureInfo.InvariantCulture));
        }
    }
    void printTextFile()
    {
        for (int i = 0; i < xPoints.Count; i++)
        {
            Debug.Log(xPoints[i]);
        }
    }
}
