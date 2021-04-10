using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    AudioSource _audioSource;
    public  static float[] _samples = new float[512];
    public float[] _freqBand = new float[8];
    public float[] _bandBuffer = new float[8];
    public float[] _bufferDecrease = new float[8];
    public float[] _freqBandHighest = new float[8];
    public static float[] _audioBand = new float[8];
    public static float[] _audioBandBuffer = new float[8];
    public static float _Amplitude, _AmplitudeBuffer;
    private float _AmplitudeHighest;

    // Use this for initialization
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        for (int i = 0; i < 8; i++)
        {
            _freqBandHighest[i] = 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
    }

    void GetAmplitude()
    {
        float _CurrentAmplitude = 0;
        float _CurrentAmpltideBuffer = 0;
        for (int i = 0; i < 8; i++)
        {
            _CurrentAmplitude += _audioBand[i];
            _CurrentAmpltideBuffer += _audioBandBuffer[i];
        }
        if (_CurrentAmplitude > _AmplitudeHighest)
        {
            _AmplitudeHighest = _CurrentAmplitude;
        }
        _Amplitude = _CurrentAmplitude / _AmplitudeHighest;
        _AmplitudeBuffer = _CurrentAmpltideBuffer / _AmplitudeHighest;
    }

    void CreateAudioBands()
    {
        float max = 0f;
        int maxIndex = 0;
        for (int i = 0; i < 8; i++)
        {
            if (_freqBand[i] > _freqBandHighest[i])
            {
                _freqBandHighest[i] = _freqBand[i];
            }
            _audioBand[i] = _freqBand[i] / _freqBandHighest[i];
            if(_audioBand[i] > max)
            {
                max = _audioBand[i];
                maxIndex = i;
            }
            _audioBandBuffer[i] = _bandBuffer[i] / _freqBandHighest[i];
            //Debug.Log("Max: " + maxIndex);
        }
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);

    }

    void BandBuffer()
    {
        for (int g = 0; g < 8; g++)
        {
            if (_freqBand[g] > _bandBuffer[g])
            {
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = 0.005f;
            }
            if (_freqBand[g] < _bandBuffer[g])
            {
                _bandBuffer[g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 1.2f;
            }
        }
    }
    /*create 8 bands of the 512 samples, lower bands for the lower base to the higher frequencies 
     * 20-60 hertz
     * 60-250 hertz
     * 500-2000 hertz
     * 2000-4000 hertz
     * 4000-6000 hertz
     * 6000-20000 hertz
     * 
     */
    
    void MakeFrequencyBands()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if (i == 7)
            {
                sampleCount += 2;
            }
            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }

            average /= count;
            _freqBand[i] = average * 10;
        }
    }
}
