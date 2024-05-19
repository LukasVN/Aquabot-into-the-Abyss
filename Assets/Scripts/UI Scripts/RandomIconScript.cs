using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomIconScript : MonoBehaviour
{
    private Image spriteImage;
    public Sprite[] icons;
    void Start()
    {
        spriteImage = GetComponent<Image>();
        SetRandomIcon();
    }
    private void SetRandomIcon(){
        spriteImage.sprite = icons[Random.Range(0,icons.Length)];
    }
}
