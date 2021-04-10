using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class CSVParser : MonoBehaviour
{
    private List<string> stringList;
    private List<string[]> parsedList;
    public List<float> xPoints;
    public List<float> yPoints;
    

    // Start is called before the first frame update
    void Start()
    {
        xPoints = new List<float>();
        yPoints = new List<float>();
        readTextFile();
        printTextFile();

    }

    // Update is called once per frame
    void Update()
    {

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
        for(int i = 0; i < xPoints.Count; i++)
        {
            Debug.Log(xPoints[i]);
        }
    }

}
