using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarPortrait : MonoBehaviour{
	
	public GameObject portrait;
	public Avatar thisAvatar;
	public Image baseTexture;    
	public Image hairTexture;
	public Image item0Texture;   // óculos
	public Image item1Texture;   // camisa
	public Image item2Texture;   // calça
	public Image item3Texture;   // sapato
	public Image item0TextureB;  // óculos_B
	public Image item1TextureB;  // camisa_B
	public Image item2TextureB;  // calça_B
	public Image item3TextureB;  // sapato_B
	
	void Start(){
		portrait = this.gameObject;
		baseTexture = portrait.transform.Find("Base").GetComponent<Image>();
		hairTexture = portrait.transform.Find("Hair").GetComponent<Image>();
		item0Texture = portrait.transform.Find("Item0").GetComponent<Image>();
		item1Texture = portrait.transform.Find("Item1").GetComponent<Image>();
		item2Texture = portrait.transform.Find("Item2").GetComponent<Image>();
		item3Texture = portrait.transform.Find("Item3").GetComponent<Image>();
		item0TextureB = portrait.transform.Find("Item0B").GetComponent<Image>();
		item1TextureB = portrait.transform.Find("Item1B").GetComponent<Image>();
		item2TextureB = portrait.transform.Find("Item2B").GetComponent<Image>();
		item3TextureB = portrait.transform.Find("Item3B").GetComponent<Image>();
		Portrait();
	}
	
	void Portrait(){
		if (thisAvatar == null){
			thisAvatar = Avatar.RandomAvatar();
		}
		if (thisAvatar.skin != -1){
			baseTexture.sprite = SessionScript.avatarBase[thisAvatar.skin];
			if (thisAvatar.gender == 0){
				hairTexture.sprite = SessionScript.avatarHairFem[thisAvatar.hair];
			}
			if (thisAvatar.gender == 1){
				hairTexture.sprite = SessionScript.avatarHairMasc[thisAvatar.hair];
			}
			item0Texture.sprite = SessionScript.avatarItem0[thisAvatar.item0];
			item1Texture.sprite = SessionScript.avatarItem1[thisAvatar.item1];
			item2Texture.sprite = SessionScript.avatarItem2[thisAvatar.item2];
			item3Texture.sprite = SessionScript.avatarItem3[thisAvatar.item3];
			item0TextureB.sprite = SessionScript.avatarItem0b[thisAvatar.item0];
			item1TextureB.sprite = SessionScript.avatarItem1b[thisAvatar.item1];
			item2TextureB.sprite = SessionScript.avatarItem2b[thisAvatar.item2];
			item3TextureB.sprite = SessionScript.avatarItem3b[thisAvatar.item3];
		}
		if (thisAvatar.skin == -1){
			baseTexture.sprite = SessionScript.noAvatar;
			if (thisAvatar.gender == 0){
				hairTexture.sprite = SessionScript.avatarBlank;
			}
			if (thisAvatar.gender == 1){
				hairTexture.sprite = SessionScript.avatarBlank;
			}
			item0Texture.sprite = SessionScript.avatarBlank;
			item1Texture.sprite = SessionScript.avatarBlank;
			item2Texture.sprite = SessionScript.avatarBlank;
			item3Texture.sprite = SessionScript.avatarBlank;
			item0TextureB.sprite = SessionScript.avatarBlank;
			item1TextureB.sprite = SessionScript.avatarBlank;
			item2TextureB.sprite = SessionScript.avatarBlank;
			item3TextureB.sprite = SessionScript.avatarBlank;
		}
	}
	
	public void RandomAvatar(){
		thisAvatar = Avatar.RandomAvatar();
		Portrait();
	}
	
	public void SpecificAvatar(Avatar newAvatar){
		thisAvatar = newAvatar;
		Portrait();
	}
	
}