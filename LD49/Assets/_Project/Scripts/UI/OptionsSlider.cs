using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum VolumeType
{
    Sounds,
    Music
}

public class OptionsSlider : MonoBehaviour
{
    public VolumeType vt;

    public void UpdateVolume(float value)
    {
        GameManager.Instance.UpdateVolume(vt, value); 
    }
}
