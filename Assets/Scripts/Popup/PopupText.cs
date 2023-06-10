using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupText : Popup
{
    public TextMeshProUGUI tmp;
    float originalY;
    public float floatHeight = 3f ;

    public void setText(string text)
    {
        tmp.text = text;
    }
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        //tmp = GetComponent<TextMeshPro>();
        originalY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        startT();
        Vector3 off = transform.position;
        off.y = originalY + Mathf.Lerp(originalY, originalY + floatHeight,t / duration);
        transform.position = off;
    }
}
