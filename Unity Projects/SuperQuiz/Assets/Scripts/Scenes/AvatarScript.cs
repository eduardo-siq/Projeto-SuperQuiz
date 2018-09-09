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
    public GameObject selectStage;
    public bool allowNext = true;   // OBSOLETE
    public GameObject lowerMenu;
    public GameObject options;

    // Avatar variables
    public GameObject portrait;
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
    public GameObject item0Selection;
    public GameObject item1Selection;
    public GameObject item2Selection;
    public GameObject item3Selection;
    bool quit;
    // int currentTier = 0;    // OBSOLETE
    int item0MaxIndex;
    int item1MaxIndex;
    int item2MaxIndex;
    int item3MaxIndex;

    // Creation process
    public GameObject gender;
    public GameObject complextion;
    public GameObject hair;
    public GameObject items;
    public bool allStagesCompleted; // MOVE TO SESSION SCRIPT
                                    // public int stage = 0;   // 0: gender, 1: complexion & hair, 2: itens	// OBSOLETE

    void Start()
    {
        TransitionScript.StartAnimation();
        StartCoroutine(StartScene());
    }

    IEnumerator StartScene()
    {
        yield return null;
        selectStage = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/ChangeState").gameObject;
        avatarRect = GameObject.Find("Canvas/Scroll View/Viewport/Avatar").GetComponent<RectTransform>();
        portrait = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Portrait").gameObject;
        lowerMenu = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/LowerMenu").gameObject;
        options = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Options").gameObject;
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
        item0Selection = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Items/SelectItem0").gameObject;
        item1Selection = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Items/SelectItem1").gameObject;
        item2Selection = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Items/SelectItem2").gameObject;
        item3Selection = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Items/SelectItem3").gameObject;
        // Creation process
        gender = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Gender").gameObject;
        complextion = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Complexion").gameObject;
        hair = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Hair").gameObject;
        items = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Items").gameObject;
        if (SessionScript.customizationStage == 2)
        {
            allStagesCompleted = true;
        }
        CustomizationStage();
        Portrait();
        allowNext = false;

        if (SessionScript.score >= SessionScript.thresholdTier1)
        {
            SessionScript.currentTier = 1;
        }
        if (SessionScript.score >= SessionScript.thresholdTier2)
        {
            SessionScript.currentTier = 2;
        }
        if (SessionScript.currentTier == 0)
        {
            print("TIER 0");
            item0MaxIndex = Mathf.RoundToInt(SessionScript.item0TierIndex.x);
            item1MaxIndex = Mathf.RoundToInt(SessionScript.item1TierIndex.x);
            item2MaxIndex = Mathf.RoundToInt(SessionScript.item2TierIndex.x);
            item3MaxIndex = Mathf.RoundToInt(SessionScript.item3TierIndex.x);
        }
        if (SessionScript.currentTier == 1)
        {
            print("TIER 1");
            item0MaxIndex = Mathf.RoundToInt(SessionScript.item0TierIndex.y);
            item1MaxIndex = Mathf.RoundToInt(SessionScript.item1TierIndex.y);
            item2MaxIndex = Mathf.RoundToInt(SessionScript.item2TierIndex.y);
            item3MaxIndex = Mathf.RoundToInt(SessionScript.item3TierIndex.y);
        }
        if (SessionScript.currentTier == 2)
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
            SessionScript.RaffleInitialAvatar();
            SessionScript.firstLogIn = false;
        }
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
                    SessionScript.playerAvatar.item0 = item0MaxIndex;
                }
                print(SessionScript.playerAvatar.item0);
                break;
            case 1:
                SessionScript.playerAvatar.item1 = SessionScript.playerAvatar.item1 - 1;
                if (SessionScript.playerAvatar.item1 < 0)
                {
                    SessionScript.playerAvatar.item1 = item1MaxIndex;
                }
                print(SessionScript.playerAvatar.item1);
                break;
            case 2:
                SessionScript.playerAvatar.item2 = SessionScript.playerAvatar.item2 - 1;
                if (SessionScript.playerAvatar.item2 < 0)
                {
                    SessionScript.playerAvatar.item2 = item2MaxIndex;
                }
                print(SessionScript.playerAvatar.item2);
                break;
            case 3:
                SessionScript.playerAvatar.item3 = SessionScript.playerAvatar.item3 - 1;
                if (SessionScript.playerAvatar.item3 < 0)
                {
                    SessionScript.playerAvatar.item3 = item3MaxIndex;
                }
                print(SessionScript.playerAvatar.item3);
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
        if (SessionScript.customizationStage == 0)
        {
            //allowNext = false;
            PopUpScript.InstantiatePopUp("Vamos criar seu avatar nesse jogo! Comece escolhendo seu gênero.", "OK");
            portrait.SetActive(false);
            gender.SetActive(true);
            complextion.SetActive(false);
            hair.SetActive(false);
            items.SetActive(false);
            selectStage.SetActive(false);
            lowerMenu.SetActive(false);
            options.SetActive(false);
        }
        if (SessionScript.customizationStage == 1)
        {
            //allowNext = false;
            PopUpScript.InstantiatePopUp("Agora, escolha a aparência.", "OK");
            portrait.SetActive(true);
            gender.SetActive(false);
            complextion.SetActive(true);
            hair.SetActive(true);
            items.SetActive(false);
            selectStage.SetActive(true);
            lowerMenu.SetActive(false);
            options.SetActive(false);
            RaffleBlankAvatar();
        }
        if (SessionScript.customizationStage == 2)
        {
            if (!allStagesCompleted)
            {
                RaffleAvatarItems();
                PopUpScript.InstantiatePopUp("Por último, escolha seus acessórios.", "OK");
            }
            portrait.SetActive(true);
            gender.SetActive(false);
            complextion.SetActive(false);
            hair.SetActive(false);
            items.SetActive(true);
            selectStage.SetActive(false);
            lowerMenu.SetActive(true);
            options.SetActive(true);
            allStagesCompleted = true;
            Portrait();
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
        if (SessionScript.customizationStage == 0)
        {
            RaffleBlankAvatar();
            return;
        }
        baseTexture.sprite = SessionScript.avatarBase[SessionScript.playerAvatar.skin];
        if (SessionScript.playerAvatar.gender == 0)
        {
            hairTexture.sprite = SessionScript.avatarHairFem[SessionScript.playerAvatar.hair];
        }
        if (SessionScript.playerAvatar.gender == 1)
        {
            hairTexture.sprite = SessionScript.avatarHairMasc[SessionScript.playerAvatar.hair];
        }
        if (SessionScript.customizationStage != 2)
        {
            return;
        }
        item0Texture.sprite = SessionScript.avatarItem0[SessionScript.playerAvatar.item0];
        item1Texture.sprite = SessionScript.avatarItem1[SessionScript.playerAvatar.item1];
        item2Texture.sprite = SessionScript.avatarItem2[SessionScript.playerAvatar.item2];
        item3Texture.sprite = SessionScript.avatarItem3[SessionScript.playerAvatar.item3];
        item0TextureB.sprite = SessionScript.avatarItem0b[SessionScript.playerAvatar.item0];
        item1TextureB.sprite = SessionScript.avatarItem1b[SessionScript.playerAvatar.item1];
        item2TextureB.sprite = SessionScript.avatarItem2b[SessionScript.playerAvatar.item2];
        item3TextureB.sprite = SessionScript.avatarItem3b[SessionScript.playerAvatar.item3];

        // Selection Options
        item0Selection.transform.Find("Item").GetComponent<Image>().sprite = SessionScript.avatarItem0[SessionScript.playerAvatar.item0];
        item1Selection.transform.Find("Item").GetComponent<Image>().sprite = SessionScript.avatarItem1[SessionScript.playerAvatar.item1];
        item2Selection.transform.Find("Item").GetComponent<Image>().sprite = SessionScript.avatarItem2[SessionScript.playerAvatar.item2];
        item3Selection.transform.Find("Item").GetComponent<Image>().sprite = SessionScript.avatarItem3[SessionScript.playerAvatar.item3];

        // AJUSTE MANUAL COTURNO
        // if coturno + calça ; algo acontece

    }

    public void SelectRaffle()
    {   // OBSOLETE
        SessionScript.ButtonAudio(SessionScript.neutral);
        SessionScript.RaffleAvatar(item0MaxIndex, item1MaxIndex, item2MaxIndex, item3MaxIndex);
        Invoke("Portrait", 0.5f);
    }

    public void BlankAvatar()
    {
        baseTexture.sprite = SessionScript.avatarBlank;
        hairTexture.sprite = SessionScript.avatarBlank;
        item0Texture.sprite = SessionScript.avatarBlank;
        item1Texture.sprite = SessionScript.avatarBlank;
        item2Texture.sprite = SessionScript.avatarBlank;
        item3Texture.sprite = SessionScript.avatarBlank;
        item0TextureB.sprite = SessionScript.avatarBlank;
        item1TextureB.sprite = SessionScript.avatarBlank;
        item2TextureB.sprite = SessionScript.avatarBlank;
        item3TextureB.sprite = SessionScript.avatarBlank;
    }

    public void RaffleAvatarItems()
    {
        SessionScript.playerAvatar.item0 = Mathf.RoundToInt(Random.Range(0, SessionScript.item0TierIndex.x));
        SessionScript.playerAvatar.item1 = Mathf.RoundToInt(Random.Range(0, SessionScript.item1TierIndex.x));
        SessionScript.playerAvatar.item2 = Mathf.RoundToInt(Random.Range(0, SessionScript.item2TierIndex.x));
        SessionScript.playerAvatar.item3 = Mathf.RoundToInt(Random.Range(0, SessionScript.item3TierIndex.x));
        Portrait();
    }

    public void RaffleBlankAvatar()
    {
        int randomComplexion = Random.Range(1, SessionScript.avatarBase.Count);
        int randomHair = 0;
        baseTexture.sprite = SessionScript.avatarBase[randomComplexion];
        if (SessionScript.playerAvatar.gender == 0)
        {
            randomHair = Random.Range(0, SessionScript.avatarHairFem.Count);
            hairTexture.sprite = SessionScript.avatarHairFem[randomHair];
        }
        if (SessionScript.playerAvatar.gender == 1)
        {
            randomHair = Random.Range(0, SessionScript.avatarHairMasc.Count);
            hairTexture.sprite = SessionScript.avatarHairMasc[randomHair];
        }
        SessionScript.playerAvatar.skin = randomComplexion;
        SessionScript.playerAvatar.hair = randomHair;
        item0Texture.sprite = SessionScript.avatarBlank;
        item1Texture.sprite = SessionScript.avatarBlank;
        item2Texture.sprite = SessionScript.avatarBlank;
        item3Texture.sprite = SessionScript.avatarBlank;
        item0TextureB.sprite = SessionScript.avatarBlank;
        item1TextureB.sprite = SessionScript.avatarBlank;
        item2TextureB.sprite = SessionScript.avatarBlank;
        item3TextureB.sprite = SessionScript.avatarBlank;
    }

    public void SelectResetAvatar()
    {
        SessionScript.ButtonAudio(SessionScript.neutral);
        SelectRaffle();
        allStagesCompleted = false;
        SessionScript.customizationStage = 0;
        CustomizationStage();
    }

    public void SelectGender(int index)
    {
        SessionScript.ButtonAudio(SessionScript.neutral);
        SessionScript.playerAvatar.gender = index;
        allowNext = true;
        SessionScript.customizationStage = SessionScript.customizationStage + 1;
        if (SessionScript.customizationStage > 2) SessionScript.customizationStage = 2;
        CustomizationStage();
    }

    public void SelectComplexion(int index)
    {
        if (index == 0) return; // Never allow index 0
        SessionScript.ButtonAudio(SessionScript.neutral);
        SessionScript.playerAvatar.skin = index;
        allowNext = true;
        Portrait();
    }

    public void SelectNextStage()
    {
        if (allowNext)
        {
            SessionScript.ButtonAudio(SessionScript.positive);
            SessionScript.customizationStage = SessionScript.customizationStage + 1;
            if (SessionScript.customizationStage > 2) SessionScript.customizationStage = 2;
            CustomizationStage();
        }
        else
        {
            if (SessionScript.customizationStage == 0) PopUpScript.InstantiatePopUp("Selecione seu gênero.", "OK");
            if (SessionScript.customizationStage == 1) PopUpScript.InstantiatePopUp("Selecione sua aparência.", "OK");
        }
    }

    public void SelectPreviousStage()
    {
        SessionScript.ButtonAudio(SessionScript.positive);
        SessionScript.customizationStage = SessionScript.customizationStage - 1;
        if (SessionScript.customizationStage < 0) SessionScript.customizationStage = 0;
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
