using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpScript : MonoBehaviour
{

    public RectTransform popUpRect;
    public RectTransform windowRect;
    public RawImage shadowImage;

    bool close = false;
    bool open = true;
    float alpha = 0;

    void Start()
    {
        this.gameObject.transform.SetParent(GameObject.Find("Canvas").transform);
        popUpRect.localScale = new Vector3(1, 1, 1);
        popUpRect.anchoredPosition = new Vector2(0, 0);
        windowRect.anchoredPosition = new Vector2(0, 500);
        shadowImage.color = new Color(0f, 0f, 0f, 0.0f);
        SessionScript.ButtonAudioLow(SessionScript.popUp);
    }

    void Update()
    {
        if (open)
        {
            if (alpha < 0.5)
            {
                alpha = shadowImage.color.a + Time.deltaTime / 2;
            }
            windowRect.anchoredPosition = new Vector2(windowRect.anchoredPosition.x, windowRect.anchoredPosition.y - Time.deltaTime * 1200);
            shadowImage.color = new Color(0f, 0f, 0f, alpha);
            if (windowRect.anchoredPosition.y <= 0)
            {
                windowRect.anchoredPosition = new Vector2(windowRect.anchoredPosition.x, 0);
                shadowImage.color = new Color(0f, 0f, 0f, 0.5f);
                open = false;
            }
        }
        if (close)
        {
            alpha = shadowImage.color.a - Time.deltaTime / 2;
            windowRect.anchoredPosition = new Vector2(windowRect.anchoredPosition.x, windowRect.anchoredPosition.y + Time.deltaTime * 1200);
            shadowImage.color = new Color(0f, 0f, 0f, alpha);
        }
    }

    public static void InstantiatePopUp(string newMainText, string newButtonText){
        GameObject newPopUp = Instantiate(Resources.Load("Prefabs/PopUp") as GameObject);
        PopUpScript popUpScript = newPopUp.GetComponent<PopUpScript>();
        newPopUp.transform.Find("PopUpWindow/Text").GetComponent<Text>().text = newMainText;
        newPopUp.transform.Find("PopUpWindow/Button/Text").GetComponent<Text>().text = ""; //newButtonText;
        newPopUp.transform.Find("PopUpWindow/Image").gameObject.SetActive(false);
    }

    public static void InstantiatePopUp(string newMainText, string newButtonText, string newTexture){
        GameObject newPopUp = Instantiate(Resources.Load("Prefabs/PopUp") as GameObject);
        PopUpScript popUpScript = newPopUp.GetComponent<PopUpScript>();
        newPopUp.transform.Find("PopUpWindow/Text").GetComponent<Text>().text = newMainText;
        newPopUp.transform.Find("PopUpWindow/Button/Text").GetComponent<Text>().text = "";	//newButtonText;
        newPopUp.transform.Find("PopUpWindow/Image").GetComponent<RawImage>().texture = Resources.Load("Textures/PopUp/" + newTexture) as Texture;
        if (newPopUp.transform.Find("PopUpWindow/Image").GetComponent<RawImage>().texture == null)
        {
            newPopUp.transform.Find("PopUpWindow/Image").GetComponent<RawImage>().texture = Resources.Load("Textures/PopUp/missingTexture") as Texture;
        }
    }

    public static void InstantiatePopUp(string newMainText, string newButtonText, Texture newTexture){
        GameObject newPopUp = Instantiate(Resources.Load("Prefabs/PopUp") as GameObject);
        PopUpScript popUpScript = newPopUp.GetComponent<PopUpScript>();
        newPopUp.transform.Find("PopUpWindow/Text").GetComponent<Text>().text = newMainText;
        newPopUp.transform.Find("PopUpWindow/Button/Text").GetComponent<Text>().text = "";	//newButtonText;
        newPopUp.transform.Find("PopUpWindow/Image").GetComponent<RawImage>().texture = newTexture;
        if (newPopUp.transform.Find("PopUpWindow/Image").GetComponent<RawImage>().texture == null)
        {
            newPopUp.transform.Find("PopUpWindow/Image").GetComponent<RawImage>().texture = Resources.Load("Textures/PopUp/missingTexture") as Texture;
        }
    }

    public static void InstantiatePopUp(string newMainText, string newButtonText, Avatar newAvatar){
        GameObject newPopUp = Instantiate(Resources.Load("Prefabs/PopUp") as GameObject);
        PopUpScript popUpScript = newPopUp.GetComponent<PopUpScript>();
        newPopUp.transform.Find("PopUpWindow/Text").GetComponent<Text>().text = newMainText;
        newPopUp.transform.Find("PopUpWindow/Button/Text").GetComponent<Text>().text = "";	//newButtonText;
        newPopUp.transform.Find("PopUpWindow/Image").gameObject.SetActive(false);
        GameObject avatarPortrait = Instantiate(Resources.Load("Prefabs/AvatarPortrait") as GameObject);
        avatarPortrait.GetComponent<AvatarPortrait>().SpecificAvatar(newAvatar);
    }

    public void PressButton(){
        SessionScript.ButtonAudioLow(SessionScript.subtle);
        Invoke("ClosePopUp", 0.25f);
        Invoke("DestroyPopUp", 1.25f);
    }

    void ClosePopUp(){
        SessionScript.ButtonAudioLow(SessionScript.popUpOut);
        close = true;
        open = false;
    }

    void DestroyPopUp(){
        print("Delete!");
        Destroy(this.gameObject);
    }

}

