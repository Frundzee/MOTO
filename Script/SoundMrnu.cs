using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

public class SoundMrnu : MonoBehaviour
{
    
    public Slider slider;
    public Text valyeCount;
    // Update is called once per frame
    void Update()
    {
        valyeCount.text = slider.value.ToString();
        AudioListener.volume = slider.value;
    }
}
