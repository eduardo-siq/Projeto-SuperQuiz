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

    public static Avatar RandomAvatar()
    {
        Avatar randomAvatar = new Avatar();
        randomAvatar.gender = Random.Range(0, 2);
        randomAvatar.skin = Random.Range(1, SessionScript.avatarBase.Count);
        if (randomAvatar.gender == 0) randomAvatar.hair = Random.Range(0, SessionScript.avatarHairFem.Count);
        if (randomAvatar.gender == 1) randomAvatar.hair = Random.Range(0, SessionScript.avatarHairMasc.Count);
        randomAvatar.item0 = Random.Range(0, SessionScript.avatarItem0.Count);
        randomAvatar.item1 = Random.Range(0, SessionScript.avatarItem1.Count);
        randomAvatar.item2 = Random.Range(0, SessionScript.avatarItem2.Count);
        randomAvatar.item3 = Random.Range(0, SessionScript.avatarItem3.Count);
        return randomAvatar;
    }
}