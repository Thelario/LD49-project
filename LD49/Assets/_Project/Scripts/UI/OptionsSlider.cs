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
    public Slider slider;

    public void UpdateVolume()
    {
        GameManager.Instance.UpdateVolume(vt, slider.value); 
    }
}
