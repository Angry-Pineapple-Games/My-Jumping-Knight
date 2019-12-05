using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    public InputField[] inputsFields;

    private bool debug = false;
    private InputField fieldFocused;
    private Text text;
    private bool showed = true;

    /*Botones*/
    private bool isMayus = false;

    public void Start()
    {
        text = GameObject.Find("textKeyboard").GetComponent<Text>();
        StartCoroutine(CheckFocused());
    }

    public void InputChar(string c)
    {
        if (text.text.Length < 30)
        {
            if (isMayus)
                text.text += c.ToUpper();
            else
                text.text += c;
        }
    }

    public void Remove()
    {
        if (text.text.Length > 0) { text.text = text.text.Substring(0, text.text.Length - 1); }
    }

    public void Mayus()
    {
        GameObject button = GameObject.Find("mayus");
        ColorBlock colorBlock = button.GetComponent<Button>().colors;
        if (isMayus)
        {
            isMayus = false;
            colorBlock.highlightedColor = new Color(0f, 0f, 0f, 1f);
        }
        else
        {
            isMayus = true;
            colorBlock.highlightedColor = new Color(0.4f, 0.4f, 0.4f, 1f);
        }
        button.GetComponent<Button>().colors = colorBlock;
    }

    public void Enter()
    {
        fieldFocused.text = text.text;
        fieldFocused = null;
    }

    public void Spacebar()
    {
        text.text += " ";
    }

    private void WriteText(string aChar)
    {
        text.text += aChar;
    }

    private void Show()
    {
        if (debug) { print("show"); }
        foreach (Transform button in transform.GetComponentInChildren<Transform>())
        {
            button.gameObject.SetActive(true);
        }
        showed = true;
    }

    private void Hide()
    {
        if (debug) { print("hide"); }
        foreach (Transform button in transform.GetComponentInChildren<Transform>())
        {
            button.gameObject.SetActive(false);
        }
        showed = false;
    }

    IEnumerator CheckFocused()
    {
        while (true)
        {
            yield return new WaitUntil(() => fieldFocused == null);
            foreach (InputField field in inputsFields)
            {
                if (field.isFocused)
                {
                    fieldFocused = field;
                    text.text = field.text;
                    Show();
                    break;
                }
            }
            yield return new WaitUntil(() => fieldFocused == null);
            if (showed) { Hide(); }
            if (!Application.isMobilePlatform)
                yield break;
        }
    }
}
