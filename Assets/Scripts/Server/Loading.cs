using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    private const float STEP = 0.1f;
    private Image icon;
    public void Start()
    {
        icon = GetComponent<Image>();
        StartCoroutine(IconPulse());
    }

    IEnumerator IconPulse()
    {
        while(true)
        {
            yield return new WaitForSeconds(STEP);
            if (icon.color.a > 0.5f)
                icon.CrossFadeAlpha(0f, STEP, true);
            else
                icon.CrossFadeAlpha(1f, STEP, true);
        }
    }
}
