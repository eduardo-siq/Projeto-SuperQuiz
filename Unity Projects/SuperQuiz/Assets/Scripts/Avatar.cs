using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar
{

    public int skin;
    public int hair;
    public int gender;  // 0: female, 1: male
    public int item0;
    public int item1;
    public int item2;
    public int item3;
    public string name;

    public Avatar()
    {
        skin = 0;
        hair = 0;
        gender = 0;
        item0 = 0;
        item1 = 0;
        item2 = 0;
        item3 = 0;
        name = "noName";
    }

    public Avatar(int newSkin, int newHair, int newGender, int newItem0, int newItem1, int newItem2, int newItem3, string newName)
    {
        skin = newSkin;
        hair = newHair;
        gender = newGender;
        item0 = newItem0;
        item1 = newItem1;
        item2 = newItem2;
        item3 = newItem3;
        name = newName;
    }
}