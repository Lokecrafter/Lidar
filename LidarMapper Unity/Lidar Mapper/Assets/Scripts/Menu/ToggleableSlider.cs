using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleableSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] string key = "Toggle...";
    private float prevVal = 0;
    // Start is called before the first frame update
    void Awake()
    {
        slider.value = PlayerPrefs.GetInt(key);
        prevVal = slider.value;
    }

    public void ToggleSlider()
    {
        if(Mathf.RoundToInt(slider.value) != 0)
        {
            slider.value = 1;
        }
        else
        {
            slider.value = 0;
        }
        UpdatePrefs();
    }

    void UpdatePrefs()
    {
        PlayerPrefs.SetInt(key, Mathf.RoundToInt(slider.value));
        prevVal = slider.value;
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            if(slider.value != prevVal)
            {
                UpdatePrefs();
            }
        }
    }
}
