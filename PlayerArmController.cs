using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmController : MonoBehaviour
{

    public Sprite one_HandSprite, two_HandSprite;

    private SpriteRenderer sr;


    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void ChangeToOneHand()
    {
        sr.sprite = one_HandSprite;
    }

    public void ChangeToTwoHand()
    {
        sr.sprite = two_HandSprite;
    }

} //class
























