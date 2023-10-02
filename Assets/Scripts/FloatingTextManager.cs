using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textcontainter;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();
   
    private void Update() 
    {   
        foreach (FloatingText txt in floatingTexts)
            txt.UpdateFloatingText();
    }
    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText dataText = GetFloatingText();

        dataText.txt.text = msg;
        dataText.txt.fontSize = fontSize;
        dataText.txt.color = color;
        dataText.go.transform.position = Camera.main.WorldToScreenPoint(position);
        dataText.motion = motion;
        dataText.duration = duration;

        dataText.Show();
    }

    private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(t => !t.active);
    
        if(txt == null)
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textcontainter.transform);
            txt.txt = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        }

        return txt;
    }
}
