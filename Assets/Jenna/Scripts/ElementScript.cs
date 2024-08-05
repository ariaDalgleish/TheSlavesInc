using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.Build.Content;

public class ElementScript : MonoBehaviour
{

    public Image targetImage;
    public Sprite[] newImages;
    public Sprite targetSprite;
 

    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ImageChange()
    {
        if (newImages.Length == 0) return;
        targetImage.sprite = newImages[currentIndex];   

        currentIndex = (currentIndex+1) % newImages.Length;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ImageChange();
    }
}
