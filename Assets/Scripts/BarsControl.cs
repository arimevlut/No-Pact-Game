using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarsControl : MonoBehaviour
{
    // Base Health, Mana etc. bars
    private Image content;

    // ccurrentFill represent bars amount
    private float currentFill;
    private float currentValue;

    // For bars value smooth move
    [SerializeField]
    private float lerpSpeed;

    // Health, Mana etc. max value
    public float MyMaxValue { get; set; }

    // Value action in bars for every character
    public float MyCurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            if (value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }

            currentFill = currentValue / MyMaxValue;

        }
    }

    void Start()
    {
        content = GetComponent<Image>();
    }

    void Update()
    {
        if (currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
    }

    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
