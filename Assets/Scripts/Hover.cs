using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    private GameObject _text;
    
    private void Awake()
    {
        _text = GameObject.Find(gameObject.name+"Text");
    }
    
    public void HoverEnter()
    {
        print("enter");
        LeanTween.scale(_text, new Vector3(1.1f, 1.1f, 1.1f), 0.1f);
    }
    
    public void HoverExit()
    {
        print("exit");
        LeanTween.scale(_text, new Vector3(1f, 1f, 1f), 0.1f);
    }
}
