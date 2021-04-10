using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Material waveMaterial;
    
    Color waveColor = Color.white;
    Color pink = new Color(250, 65, 190);
    Color sphereColor = Color.cyan;
    MeshRenderer mesh;
    float lerpTime = 0.5f;
    float t = 0f;
    private Color _startColor, _endColor;
    public int band;
    LineRenderer line;
    TrailRenderer trail;
    public bool useLineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        if (useLineRenderer)
        {
            line = GetComponent<LineRenderer>();
        }
        else
        {
            trail = GetComponent<TrailRenderer>();
        }
        
        mesh.material.EnableKeyword("_EMISSION");
       
        _startColor = new Color(0, 0, 1, 1);
        _endColor = new Color(1, 0, 0, 1);
        mesh.material.SetColor("_Color", _endColor);
        //mesh.material.SetColor("_Color", pink);
        //mesh.material.SetColor("_EmissionColor", pink);


    }

    // Update is called once per frame
    void Update()
    {
        //waveColor = Color.Lerp(Color.white , pink  ,0.5f *Time.deltaTime);
        //sphereColor = Color.Lerp(pink, Color.cyan , 0.12f * Time.deltaTime);
        //mesh.material.color = Color.Lerp(mesh.material.color, pink, lerpTime * Time.deltaTime);
        //mesh.material.SetColor("_EmissionColor", waveColor);
        //mesh.material.SetColor("_Color", sphereColor);
        //t = Mathf.Lerp(t, 1f, lerpTime * lerpTime.DeltaTime);
        //Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        //mesh.material.SetColor("_Color", waveColor);
        ChangeColor();

    }
    void ChangeColor()
    {
        Color colorLerp = Color.Lerp(_startColor, _endColor, AudioPeer._audioBand[band]*Time.deltaTime);
        //Debug.Log(colorLerp);
        Color randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        Color test = new Color(1 * AudioPeer._audioBand[band], 0, 0, 1);
        Color test2 = Color.Lerp(_startColor, _endColor, 4f*AudioPeer._audioBand[band]);
        SetColor(test2);
    }
    void SetColor(Color c)
    {
        
        if (useLineRenderer)
        {
            line.SetColors(c, c);
        }
        else
        {
            mesh.material.SetColor("_Color", c);
            mesh.material.SetColor("_EmissionColor", c);
            trail.material.SetColor("_Color", c);
            trail.material.SetColor("_EmissionColor", c);
        }

        //trail.endColor(c);
    }
}
