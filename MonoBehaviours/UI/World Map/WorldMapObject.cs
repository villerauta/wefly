using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WorldMapObject : MonoBehaviour
{
    private Vector3 originalSize;

    public string description;
    // Start is called before the first frame update
    void Start()
    {
        originalSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {   
        print(name + " was clicked");
        transform.DOPunchScale(Vector3.zero,0.25f,5).SetUpdate(true);
    }

    public void Enter()
    {
        print(name + " being entered");
        //LeanTween.scale(gameObject,originalSize*1.2f,0.1f).setIgnoreTimeScale(true);
    }

    public void Exit()
    {
        print(name + " being exited");
        Vector3 size = new Vector3(1,1,1);
        //LeanTween.scale(gameObject,originalSize,0.1f).setIgnoreTimeScale(true);
    }
}
