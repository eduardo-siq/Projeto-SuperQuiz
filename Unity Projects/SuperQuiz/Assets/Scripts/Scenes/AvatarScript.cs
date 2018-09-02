using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AvatarScript : MonoBehaviour
{

    // UI
    public RectTransform avatarRect;
    public bool endScene;
    public GameObject nextStage;

    // Avatar variables
    public GameObject portrait;
    public RawImage baseTexture;    
    public RawImage hairTexture;
    public RawImage item0Texture;   // óculos
    public RawImage item1Texture;   // camisa
    public RawImage item2Texture;   // calça
    public RawImage item3Texture;   // sapato
    public RawImage item0TextureB;  // óculos_B
    public RawImage item1TextureB;  // camisa_B
    public RawImage item2TextureB;  // calça_B
    public RawImage item3TextureB;  // sapato_B
    public GameObject item0Selection;
    public GameObject item1Selection;
    public GameObject item2Selection;
    public GameObject item3Selection;
    bool quit;
    int currentTier = 0;    // BETTER MOVE TO SESSION SCRIPT
    int item0MaxIndex;
    int item1MaxIndex;
    int item2MaxIndex;
    int item3MaxIndex;

    // Creation process
    public GameObject gender;
    public GameObject complextion;
    public GameObject hair;
    public GameObject items;
    public bool allStagesCompleted;
    public int stage = 3;   // 0: gender, 1: complexion, 2: hair, 3: itens	// BETTER MOVE TO SESSION SCRIPT

    void Start()
    {
        StartCoroutine(StartScene());

        if (currentTier == 0)
        {
            print("TIER 0");
            item0MaxIndex = Mathf.RoundToInt(SessionScript.item0TierIndex.x);
            item1MaxIndex = Mathf.RoundToInt(SessionScript.item1TierIndex.x);
            item2MaxIndex = Mathf.RoundToInt(SessionScript.item2TierIndex.x);
            item3MaxIndex = Mathf.RoundToInt(SessionScript.item3TierIndex.x);
        }
        if (currentTier == 1)
        {
            print("TIER 1");
            item0MaxIndex = Mathf.RoundToInt(SessionScript.item0TierIndex.y);
            item1MaxIndex = Mathf.RoundToInt(SessionScript.item1TierIndex.y);
            item2MaxIndex = Mathf.RoundToInt(SessionScript.item2TierIndex.y);
            item3MaxIndex = Mathf.RoundToInt(SessionScript.item3TierIndex.y);
        }
        if (currentTier == 2)
        {
            print("TIER 2");
            item0MaxIndex = Mathf.RoundToInt(SessionScript.item0TierIndex.z);
            item1MaxIndex = Mathf.RoundToInt(SessionScript.item1TierIndex.z);
            item2MaxIndex = Mathf.RoundToInt(SessionScript.item2TierIndex.z);
            item3MaxIndex = Mathf.RoundToInt(SessionScript.item3TierIndex.z);
        }
        if (SessionScript.firstLogIn)
        {
            TransitionScript.SkipAnimation();
            GameObject newPopUp = PopUpScript.InstantiatePopUp("Bem-vindo ao SuperQuiz! Customize seu avatar!", "OK", "0");
            SessionScript.RaffleInitialAvatar();
            SessionScript.firstLogIn = false;
        }
    }

    IEnumerator StartScene()
    {
        yield return null;
        nextStage = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/NextStage").gameObject;
        avatarRect = GameObject.Find("Canvas/Scroll View/Viewport/Avatar").GetComponent<RectTransform>();
        portrait = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Portrait").gameObject;
        baseTexture = portrait.transform.Find("Base").GetComponent<RawImage>();
        hairTexture = portrait.transform.Find("Hair").GetComponent<RawImage>();
        item0Texture = portrait.transform.Find("Item0").GetComponent<RawImage>();
        item1Texture = portrait.transform.Find("Item1").GetComponent<RawImage>();
        item2Texture = portrait.transform.Find("Item2").GetComponent<RawImage>();
        item3Texture = portrait.transform.Find("Item3").GetComponent<RawImage>();
        item0TextureB = portrait.transform.Find("Item0B").GetComponent<RawImage>();
        item1TextureB = portrait.transform.Find("Item1B").GetComponent<RawImage>();
        item2TextureB = portrait.transform.Find("Item2B").GetComponent<RawImage>();
        item3TextureB = portrait.transform.Find("Item3B").GetComponent<RawImage>();
        item0Selection = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Items/SelectItem0").gameObject;
        item1Selection = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Items/SelectItem1").gameObject;
        item2Selection = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Items/SelectItem2").gameObject;
        item3Selection = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Items/SelectItem3").gameObject;
        // Creation process
        gender = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Gender").gameObject;
        complextion = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Complexion").gameObject;
        hair = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Hair").gameObject;
        items = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Items").gameObject;
        CustomizationStage();
        Portrait();
    }

    void Update()
    {
        // if (endScene){
        // avatarRect.anchoredPosition = new Vector2 (avatarRect.anchoredPosition.x, avatarRect.anchoredPosition.y - Time.deltaTime * 1200);
        // return;
        // }
        if (quit)
        {
            SessionScript.songAudio.volume = SessionScript.songAudio.volume - (Time.deltaTime * 2);
        }
    }

    public void NextItem(int item)
    {
        switch (item)
        {
            case 0:
                SessionScript.playerAvatar.item0 = SessionScript.playerAvatar.item0 + 1;
                if (SessionScript.playerAvatar.item0 > item0MaxIndex)
                {
                    SessionScript.playerAvatar.item0 = 0;

                }
                break;
            case 1:
                SessionScript.playerAvatar.item1 = SessionScript.playerAvatar.item1 + 1;
                if (SessionScript.playerAvatar.item1 > item1MaxIndex)
                {
                    SessionScript.playerAvatar.item1 = 0;

                }
                break;
            case 2:
                SessionScript.playerAvatar.item2 = SessionScript.playerAvatar.item2 + 1;
                if (SessionScript.playerAvatar.item2 > item2MaxIndex)
                {
                    SessionScript.playerAvatar.item2 = 0;

                }
                break;
            case 3:
                SessionScript.playerAvatar.item3 = SessionScript.playerAvatar.item3 + 1;
                if (SessionScript.playerAvatar.item3 > item3MaxIndex)
                {
                    SessionScript.playerAvatar.item3 = 0;

                }
                break;
            default:
                break;
        }
        SessionScript.ButtonAudio(SessionScript.subtle);
        Portrait();
    }

    public void PreviousItem(int item)
    {
        switch (item)
        {
            case 0:
                SessionScript.playerAvatar.item0 = SessionScript.playerAvatar.item0 - 1;
                if (SessionScript.playerAvatar.item0 < 0)
                {
                    SessionScript.playerAvatar.item0 = item0MaxIndex - 1;

                }
                break;
            case 1:
                SessionScript.playerAvatar.item1 = SessionScript.playerAvatar.item1 - 1;
                if (SessionScript.playerAvatar.item1 < 0)
                {
                    SessionScript.playerAvatar.item1 = item1MaxIndex - 1;

                }
                break;
            case 2:
                SessionScript.playerAvatar.item2 = SessionScript.playerAvatar.item2 - 1;
                if (SessionScript.playerAvatar.item2 < 0)
                {
                    SessionScript.playerAvatar.item2 = item2MaxIndex - 1;

                }
                break;
            case 3:
                SessionScript.playerAvatar.item3 = SessionScript.playerAvatar.item3 - 1;
                if (SessionScript.playerAvatar.item3 < 0)
                {
                    SessionScript.playerAvatar.item3 = item3MaxIndex - 1;

                }
                break;
            default:
                break;
        }
        SessionScript.ButtonAudio(SessionScript.subtle);
        Portrait();
    }

    public void NextHair()
    {
        int index;
        if (SessionScript.playerAvatar.gender == 0)
        {
            index = SessionScript.playerAvatar.hair + 1;
            if (index >= SessionScript.avatarHairFem.Count)
            {
                index = 0;
            }
            SessionScript.playerAvatar.hair = index;
        }
        if (SessionScript.playerAvatar.gender == 1)
        {
            index = SessionScript.playerAvatar.hair + 1;
            if (index >= SessionScript.avatarHairMasc.Count)
            {
                index = 0;
            }
            SessionScript.playerAvatar.hair = index;
        }
        SessionScript.ButtonAudio(SessionScript.subtle);
        Portrait();
    }

    public void PreviousHair()
    {
        int index;
        if (SessionScript.playerAvatar.gender == 0)
        {
            index = SessionScript.playerAvatar.hair - 1;
            if (index < 0)
            {
                index = SessionScript.avatarHairFem.Count - 1;
            }
            SessionScript.playerAvatar.hair = index;
        }
        if (SessionScript.playerAvatar.gender == 1)
        {
            index = SessionScript.playerAvatar.hair - 1;
            if (index < 0)
            {
                index = SessionScript.avatarHairMasc.Count - 1;
            }
            SessionScript.playerAvatar.hair = index;
        }
        SessionScript.ButtonAudio(SessionScript.subtle);
        Portrait();
    }

    public void CustomizationStage()
    {
        if (stage == 0)
        {
            PopUpScript.InstantiatePopUp("Vamos criar seu avatar nesse jogo! Comece escolhendo seu gênero.", "OK");
            portrait.SetActive(false);
            gender.SetActive(true);
            complextion.SetActive(false);
            hair.SetActive(false);
            items.SetActive(false);
            nextStage.SetActive(true);
        }
        if (stage == 1)
        {
            PopUpScript.InstantiatePopUp("Agora, escolha a aparência.", "OK");
            portrait.SetActive(true);
            gender.SetActive(false);
            complextion.SetActive(true);
            hair.SetActive(true);
            items.SetActive(false);
            nextStage.SetActive(true);
        }
        if (stage == 2)
        {
            if (allStagesCompleted)
            {
                PopUpScript.InstantiatePopUp("Por último, escolha seus acessórios.", "OK");
            }
            SessionScript.RaffleAvatar(item0MaxIndex, item1MaxIndex, item2MaxIndex, item3MaxIndex);
            portrait.SetActive(true);
            gender.SetActive(false);
            complextion.SetActive(false);
            hair.SetActive(false);
            items.SetActive(true);
            nextStage.SetActive(false);
            allStagesCompleted = true;
        }
    }

    public void SelectBase(int index)
    {
        SessionScript.playerAvatar.skin = index;
        SessionScript.ButtonAudio(SessionScript.subtle);
        Portrait();
    }

    public void Portrait()
    {
        baseTexture.texture = SessionScript.avatarBase[SessionScript.playerAvatar.skin];
        if (SessionScript.playerAvatar.gender == 0)
        {
            hairTexture.texture = SessionScript.avatarHairFem[SessionScript.playerAvatar.hair];
        }
        if (SessionScript.playerAvatar.gender == 1)
        {
            hairTexture.texture = SessionScript.avatarHairMasc[SessionScript.playerAvatar.hair];
        }
        item0Texture.texture = SessionScript.avatarItem0[SessionScript.playerAvatar.item0];
        item1Texture.texture = SessionScript.avatarItem1[SessionScript.playerAvatar.item1];
        item2Texture.texture = SessionScript.avatarItem2[SessionScript.playerAvatar.item2];
        item3Texture.texture = SessionScript.avatarItem3[SessionScript.playerAvatar.item3];
        item0TextureB.texture = SessionScript.avatarItem0b[SessionScript.playerAvatar.item0];
        item1TextureB.texture = SessionScript.avatarItem1b[SessionScript.playerAvatar.item1];
        item2TextureB.texture = SessionScript.avatarItem2b[SessionScript.playerAvatar.item2];
        item3TextureB.texture = SessionScript.avatarItem3b[SessionScript.playerAvatar.item3];

        // Selection Options
        item0Selection.transform.Find("Item").GetComponent<RawImage>().texture = SessionScript.avatarItem0[SessionScript.playerAvatar.item0];
        item1Selection.transform.Find("Item").GetComponent<RawImage>().texture = SessionScript.avatarItem1[SessionScript.playerAvatar.item1];
        item2Selection.transform.Find("Item").GetComponent<RawImage>().texture = SessionScript.avatarItem2[SessionScript.playerAvatar.item2];
        item3Selection.transform.Find("Item").GetComponent<RawImage>().texture = SessionScript.avatarItem3[SessionScript.playerAvatar.item3];

        // AJUSTE MANUAL COTURNO
        // if coturno + calça ; algo acontece

    }

    public void SelectRaffle()
    {   // OBSOLETE
        SessionScript.ButtonAudio(SessionScript.neutral);
        SessionScript.RaffleAvatar(item0MaxIndex, item1MaxIndex, item2MaxIndex, item3MaxIndex);
        Invoke("Portrait", 0.5f);
    }

    public void NakedAvatar()
    {
        item0Texture.texture = SessionScript.avatarItem0[SessionScript.avatarBlank];
        item1Texture.texture = SessionScript.avatarItem1[SessionScript.avatarBlank];
        item2Texture.texture = SessionScript.avatarItem2[SessionScript.avatarBlank];
        item3Texture.texture = SessionScript.avatarItem3[SessionScript.avatarBlank];
        item0TextureB.texture = SessionScript.avatarItem0b[SessionScript.avatarBlank];
        item1TextureB.texture = SessionScript.avatarItem1b[SessionScript.avatarBlank];
        item2TextureB.texture = SessionScript.avatarItem2b[SessionScript.avatarBlank];
        item3TextureB.texture = SessionScript.avatarItem3b[SessionScript.avatarBlank];
    }

    public void SelectResetAvatar()
    {
        SessionScript.ButtonAudio(SessionScript.neutral);
        SelectRaffle();
        allStagesCompleted = false;
        stage = 0;
        CustomizationStage();
    }

    public void NextStage()
    {
        SessionScript.ButtonAudio(SessionScript.positive);
        stage = stage + 1;
        CustomizationStage();
    }

    public void SelectMenu()
    {
        SessionScript.ButtonAudio(SessionScript.neutral);
        Invoke("EndScene", 0.5f);
        Invoke("NextScene", 1f);

    }

    public void NextScene()
    {
        SceneManager.LoadScene("menu", LoadSceneMode.Single);
    }

    void EndScene()
    {
        endScene = true;
    }
}
