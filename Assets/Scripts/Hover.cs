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
        LeanTween.scale(_text, new Vector3(1.1f, 1.1f, 1.1f), 0.1f);
    }
    
    public void HoverExit()
    {
        LeanTween.scale(_text, new Vector3(1f, 1f, 1f), 0.1f);
    }
}
