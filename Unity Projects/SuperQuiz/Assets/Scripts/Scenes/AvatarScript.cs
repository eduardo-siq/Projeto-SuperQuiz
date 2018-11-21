using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AvatarScript : MonoBehaviour{

    // UI
    public RectTransform avatarRect;
    public bool endScene;
    public GameObject selectStageNext;
	public GameObject selectStagePrevious;
    public bool allowNext = true;   // OBSOLETE
    public GameObject lowerMenu;
    public GameObject options;

    // Avatar variables
    public GameObject portrait;
	public GameObject portraitBackgroundComplexion;
	public GameObject portraitBackgroundItems;
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
	public Sprite item_initial_1;
	public Sprite item_initial_2;

    // Creation process
    public GameObject gender;
    public GameObject complextion;
    public GameObject hair;
    public GameObject items;
    public bool allStagesCompleted; // MOVE TO SESSION SCRIPT
                                    // public int stage = 0;   // 0: gender, 1: complexion & hair, 2: itens	// OBSOLETE
									
	// Photography
	public GameObject photographyWindow;

    void Start(){
        StartCoroutine(StartScene());
    }

    IEnumerator StartScene(){
        yield return null;
        selectStageNext = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/ChangeState/Next").gameObject;
		selectStagePrevious = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/ChangeState/Previous").gameObject;
        avatarRect = GameObject.Find("Canvas/Scroll View/Viewport/Avatar").GetComponent<RectTransform>();
        portrait = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Portrait").gameObject;
		portraitBackgroundComplexion = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/PortraitBackgroundComplexion").gameObject;
		portraitBackgroundItems = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/PortraitBackgroundItems").gameObject;
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
		item_initial_1 = Resources.Load("Textures/Avatar/avatar_initial_0", typeof(Sprite)) as Sprite;
		item_initial_2 = Resources.Load("Textures/Avatar/avatar_initial_1", typeof(Sprite)) as Sprite;
		// Photography
		photographyWindow = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/PhotographyWindow").gameObject;
		photographyWindow.SetActive(false);
        if (SessionScript.customizationStage == 2){
            allStagesCompleted = true;
        }
		// if (SessionScript.customizationStage == 0){	// Gender selection disabled
			// Invoke("CustomizationStage", 0.75f);
			// portrait.SetActive(false);
			// gender.SetActive(true);
			// complextion.SetActive(false);
			// hair.SetActive(false);
			// items.SetActive(false);
			// selectStageNext.SetActive(false);
			// selectStagePrevious
			// lowerMenu.SetActive(false);
			// options.SetActive(false);
		// }
		// if (SessionScript.customizationStage != 0){
			// CustomizationStage();
		// }
		if (SessionScript.customizationStage == 1){
			//allowNext = false;
			Invoke("InitialPopUp", 0.25f);
			portrait.SetActive(true);
			portrait.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0f, -46f);
			portraitBackgroundComplexion.SetActive(true);
			portraitBackgroundItems.SetActive(false);
			gender.SetActive(false);
			complextion.SetActive(true);
			hair.SetActive(true);
			items.SetActive(false);
			selectStageNext.SetActive(true);
			selectStagePrevious.SetActive(false);
			lowerMenu.SetActive(false);
			options.SetActive(false);
			RaffleBlankAvatar();
		}
		if (SessionScript.customizationStage != 1){
			CustomizationStage();
		}

        if (SessionScript.player.score >= SessionScript.thresholdTier1){
            SessionScript.currentTier = 1;
        }
        if (SessionScript.player.score >= SessionScript.thresholdTier2){
            SessionScript.currentTier = 2;
        }
        if (SessionScript.currentTier == 0){
            print("TIER 0");
            item0MaxIndex = Mathf.RoundToInt(SessionScript.item0TierIndex.x);
            item1MaxIndex = Mathf.RoundToInt(SessionScript.item1TierIndex.x);
            item2MaxIndex = Mathf.RoundToInt(SessionScript.item2TierIndex.x);
            item3MaxIndex = Mathf.RoundToInt(SessionScript.item3TierIndex.x);
        }
        if (SessionScript.currentTier == 1){
            print("TIER 1");
            item0MaxIndex = Mathf.RoundToInt(SessionScript.item0TierIndex.y);
            item1MaxIndex = Mathf.RoundToInt(SessionScript.item1TierIndex.y);
            item2MaxIndex = Mathf.RoundToInt(SessionScript.item2TierIndex.y);
            item3MaxIndex = Mathf.RoundToInt(SessionScript.item3TierIndex.y);
        }
        if (SessionScript.currentTier == 2){
            print("TIER 2");
            item0MaxIndex = Mathf.RoundToInt(SessionScript.item0TierIndex.z);
            item1MaxIndex = Mathf.RoundToInt(SessionScript.item1TierIndex.z);
            item2MaxIndex = Mathf.RoundToInt(SessionScript.item2TierIndex.z);
            item3MaxIndex = Mathf.RoundToInt(SessionScript.item3TierIndex.z);
        }
        // if (SessionScript.firstLogIn)
        // {
            // TransitionScript.SkipAnimation();
            // SessionScript.RaffleInitialAvatar();
            // SessionScript.firstLogIn = false;
        // }
		
		Portrait();
        allowNext = false;
    }

    void Update(){
        // if (endScene){
        // avatarRect.anchoredPosition = new Vector2 (avatarRect.anchoredPosition.x, avatarRect.anchoredPosition.y - Time.deltaTime * 1200);
        // return;
        // }
        if (quit)
        {
            SessionScript.songAudio.volume = SessionScript.songAudio.volume - (Time.deltaTime * 2);
        }
    }
	
	void InitialPopUp(){
		PopUpScript.InstantiatePopUp("Escolha a aparência.", "OK");
	}
	
	

    public void NextItem(int item){
        switch (item){
            case 0:
                SessionScript.player.avatar.item0 = SessionScript.player.avatar.item0 + 1;
                if (SessionScript.player.avatar.item0 > item0MaxIndex){
                    SessionScript.player.avatar.item0 = 0;
                }
                break;
            case 1:
                SessionScript.player.avatar.item1 = SessionScript.player.avatar.item1 + 1;
                if (SessionScript.player.avatar.item1 > item1MaxIndex){
                    SessionScript.player.avatar.item1 = 0;
                }
                break;
            case 2:
                SessionScript.player.avatar.item2 = SessionScript.player.avatar.item2 + 1;
                if (SessionScript.player.avatar.item2 > item2MaxIndex){
                    SessionScript.player.avatar.item2 = 0;
                }
                break;
            case 3:
                SessionScript.player.avatar.item3 = SessionScript.player.avatar.item3 + 1;
                if (SessionScript.player.avatar.item3 > item3MaxIndex){
                    SessionScript.player.avatar.item3 = 0;
                }
                break;
            default:
                break;
        }
        SessionScript.ButtonAudio(SessionScript.subtle);
        Portrait();
    }

    public void PreviousItem(int item){
        switch (item){
            case 0:
                SessionScript.player.avatar.item0 = SessionScript.player.avatar.item0 - 1;
                if (SessionScript.player.avatar.item0 < 0){
                    SessionScript.player.avatar.item0 = item0MaxIndex;
                }
                print(SessionScript.player.avatar.item0);
                break;
            case 1:
                SessionScript.player.avatar.item1 = SessionScript.player.avatar.item1 - 1;
                if (SessionScript.player.avatar.item1 < 0){
                    SessionScript.player.avatar.item1 = item1MaxIndex;
                }
                print(SessionScript.player.avatar.item1);
                break;
            case 2:
                SessionScript.player.avatar.item2 = SessionScript.player.avatar.item2 - 1;
                if (SessionScript.player.avatar.item2 < 0){
                    SessionScript.player.avatar.item2 = item2MaxIndex;
                }
                print(SessionScript.player.avatar.item2);
                break;
            case 3:
                SessionScript.player.avatar.item3 = SessionScript.player.avatar.item3 - 1;
                if (SessionScript.player.avatar.item3 < 0){
                    SessionScript.player.avatar.item3 = item3MaxIndex;
                }
                print(SessionScript.player.avatar.item3);
                break;
            default:
                break;
        }
        SessionScript.ButtonAudio(SessionScript.subtle);
        Portrait();
    }

    public void NextHair(){
        int index;
        if (SessionScript.player.avatar.gender == 0){
            index = SessionScript.player.avatar.hair + 1;
            if (index >= SessionScript.avatarHairFem.Count){
                index = 0;
            }
            SessionScript.player.avatar.hair = index;
        }
        if (SessionScript.player.avatar.gender == 1){
            index = SessionScript.player.avatar.hair + 1;
            if (index >= SessionScript.avatarHairMasc.Count){
                index = 0;
            }
            SessionScript.player.avatar.hair = index;
        }
        SessionScript.ButtonAudio(SessionScript.subtle);
        Portrait();
    }

    public void PreviousHair(){
        int index;
        if (SessionScript.player.avatar.gender == 0){
            index = SessionScript.player.avatar.hair - 1;
            if (index < 0){
                index = SessionScript.avatarHairFem.Count - 1;
            }
            SessionScript.player.avatar.hair = index;
        }
        if (SessionScript.player.avatar.gender == 1){
            index = SessionScript.player.avatar.hair - 1;
            if (index < 0){
                index = SessionScript.avatarHairMasc.Count - 1;
            }
            SessionScript.player.avatar.hair = index;
        }
        SessionScript.ButtonAudio(SessionScript.subtle);
        Portrait();
    }

	public void CustomizationStage(){
		if (SessionScript.customizationStage == 0){
			//allowNext = false;
			// PopUpScript.InstantiatePopUp("Vamos criar seu avatar nesse jogo! Comece escolhendo seu gênero.", "OK");
			// portrait.SetActive(false);
			// gender.SetActive(true);
			// complextion.SetActive(false);
			// hair.SetActive(false);
			// items.SetActive(false);
			// selectStageNext.SetActive(false);
			// selectStagePrevious
			// lowerMenu.SetActive(false);
			// options.SetActive(false);
			SessionScript.customizationStage = 1;	// Gender selection disabled
		}
		if (SessionScript.customizationStage == 1){
			//allowNext = false;
			if (!allStagesCompleted){
				PopUpScript.InstantiatePopUp("Escolha a aparência.", "OK");
			}
			portrait.SetActive(true);
			portrait.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0f, -46f);
			portraitBackgroundComplexion.SetActive(true);
			portraitBackgroundItems.SetActive(false);
			gender.SetActive(false);
			complextion.SetActive(true);
			hair.SetActive(true);
			items.SetActive(false);
			selectStageNext.SetActive(true);
			selectStagePrevious.SetActive(false);
			lowerMenu.SetActive(false);
			options.SetActive(false);
			RaffleBlankAvatar();
		}
		if (SessionScript.customizationStage == 2){
			if (!allStagesCompleted){
				RaffleAvatarItems();
				PopUpScript.InstantiatePopUp("Por último, escolha seus acessórios.", "OK");
			}
			portrait.SetActive(true);
			portrait.GetComponent<RectTransform>().anchoredPosition = new Vector2 (-43.5f, -5f);
			portraitBackgroundComplexion.SetActive(false);
			portraitBackgroundItems.SetActive(true);
			gender.SetActive(false);
			complextion.SetActive(false);
			hair.SetActive(false);
			items.SetActive(true);
			selectStageNext.SetActive(false);
			selectStagePrevious.SetActive(false);
			lowerMenu.SetActive(true);
			options.SetActive(true);
			allStagesCompleted = true;
			Portrait();
		}
	}

    public void SelectBase(int index){
        SessionScript.player.avatar.skin = index;
        SessionScript.ButtonAudio(SessionScript.subtle);
        Portrait();
    }

    public void Portrait(){
        if (SessionScript.customizationStage == 0){
            RaffleBlankAvatar();
            return;
        }
        baseTexture.sprite = SessionScript.avatarBase[SessionScript.player.avatar.skin];
        if (SessionScript.player.avatar.gender == 0){
            hairTexture.sprite = SessionScript.avatarHairFem[SessionScript.player.avatar.hair];
        }
        if (SessionScript.player.avatar.gender == 1){
            hairTexture.sprite = SessionScript.avatarHairMasc[SessionScript.player.avatar.hair];
        }
        if (SessionScript.customizationStage != 2){
            return;
        }
        item0Texture.sprite = SessionScript.avatarItem0[SessionScript.player.avatar.item0];
        item1Texture.sprite = SessionScript.avatarItem1[SessionScript.player.avatar.item1];
        item2Texture.sprite = SessionScript.avatarItem2[SessionScript.player.avatar.item2];
        item3Texture.sprite = SessionScript.avatarItem3[SessionScript.player.avatar.item3];
        item0TextureB.sprite = SessionScript.avatarItem0b[SessionScript.player.avatar.item0];
        item1TextureB.sprite = SessionScript.avatarItem1b[SessionScript.player.avatar.item1];
        item2TextureB.sprite = SessionScript.avatarItem2b[SessionScript.player.avatar.item2];
        item3TextureB.sprite = SessionScript.avatarItem3b[SessionScript.player.avatar.item3];

        // Selection Options
        item0Selection.transform.Find("Frame/Viewport/Item").GetComponent<Image>().sprite = SessionScript.avatarItem0[SessionScript.player.avatar.item0];
        item1Selection.transform.Find("Frame/Viewport/Item").GetComponent<Image>().sprite = SessionScript.avatarItem1[SessionScript.player.avatar.item1];
        item2Selection.transform.Find("Frame/Viewport/Item").GetComponent<Image>().sprite = SessionScript.avatarItem2[SessionScript.player.avatar.item2];
        item3Selection.transform.Find("Frame/Viewport/Item").GetComponent<Image>().sprite = SessionScript.avatarItem3[SessionScript.player.avatar.item3];

        // AJUSTE MANUAL COTURNO
        // if coturno + calça ; algo acontece

    }

	public void SelectRaffle(){   // OBSOLETE
		SessionScript.ButtonAudio(SessionScript.neutral);
		SessionScript.RaffleAvatar(item0MaxIndex, item1MaxIndex, item2MaxIndex, item3MaxIndex);
		Invoke("Portrait", 0.5f);
	}

    public void BlankAvatar(){
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

    public void RaffleAvatarItems(){
        SessionScript.player.avatar.item0 = Mathf.RoundToInt(Random.Range(1, SessionScript.item0TierIndex.x));	// Never selects avatar_0_0_0
        SessionScript.player.avatar.item1 = Mathf.RoundToInt(Random.Range(0, SessionScript.item1TierIndex.x));
        SessionScript.player.avatar.item2 = Mathf.RoundToInt(Random.Range(0, SessionScript.item2TierIndex.x));
        SessionScript.player.avatar.item3 = Mathf.RoundToInt(Random.Range(0, SessionScript.item3TierIndex.x));
        Portrait();
    }

    public void RaffleBlankAvatar(){
        int randomComplexion = Random.Range(1, SessionScript.avatarBase.Count);
        int randomHair = 0;
        baseTexture.sprite = SessionScript.avatarBase[randomComplexion];
        if (SessionScript.player.avatar.gender == 0){
            randomHair = Random.Range(0, SessionScript.avatarHairFem.Count);
            hairTexture.sprite = SessionScript.avatarHairFem[randomHair];
        }
        if (SessionScript.player.avatar.gender == 1){
            randomHair = Random.Range(0, SessionScript.avatarHairMasc.Count);
            hairTexture.sprite = SessionScript.avatarHairMasc[randomHair];
        }
        SessionScript.player.avatar.skin = randomComplexion;
        SessionScript.player.avatar.hair = randomHair;
        item0Texture.sprite = SessionScript.avatarBlank;
        item1Texture.sprite = item_initial_1;
        item2Texture.sprite = item_initial_2;
        item3Texture.sprite = SessionScript.avatarBlank;
        item0TextureB.sprite = SessionScript.avatarBlank;
        item1TextureB.sprite = SessionScript.avatarBlank;
        item2TextureB.sprite = SessionScript.avatarBlank;
        item3TextureB.sprite = SessionScript.avatarBlank;
    }

    public void SelectResetAvatar(){
        SessionScript.ButtonAudio(SessionScript.neutral);
        SelectRaffle();
        //allStagesCompleted = false;
        SessionScript.customizationStage = 1;
        CustomizationStage();
    }

    public void SelectGender(int index){	// DEPRICATED
        SessionScript.ButtonAudio(SessionScript.neutral);
        SessionScript.player.avatar.gender = index;
        allowNext = true;
        SessionScript.customizationStage = SessionScript.customizationStage + 1;
        if (SessionScript.customizationStage > 2) SessionScript.customizationStage = 2;
        CustomizationStage();
    }

    public void SelectComplexion(int index){
        if (index == 0) return; // Never allow index 0
        SessionScript.ButtonAudio(SessionScript.neutral);
        SessionScript.player.avatar.skin = index;
        allowNext = true;
        Portrait();
    }

    public void SelectNextStage(){
		allowNext = true;	// Two stages only
        if (allowNext){
            SessionScript.ButtonAudio(SessionScript.positive);
            SessionScript.customizationStage = SessionScript.customizationStage + 1;
            if (SessionScript.customizationStage > 2) SessionScript.customizationStage = 2;
            CustomizationStage();
        }else{
            if (SessionScript.customizationStage == 0) PopUpScript.InstantiatePopUp("Selecione seu gênero!", "OK");
            if (SessionScript.customizationStage == 1) PopUpScript.InstantiatePopUp("Selecione sua aparência!", "OK");
        }
    }

    public void SelectPreviousStage(){
        SessionScript.ButtonAudio(SessionScript.positive);
        SessionScript.customizationStage = SessionScript.customizationStage - 1;
        if (SessionScript.customizationStage < 0) SessionScript.customizationStage = 0;
        CustomizationStage();
    }
	
	public void SelectPhotography(){
		SessionScript.ButtonAudioLow(SessionScript.neutral);
		Invoke("OpenPhotographyWindow", 0.5f);
		Invoke("TakePhotography", 0.55f);
		Invoke("ClosePhotographyWindow", 1f);
	}
	
	void OpenPhotographyWindow(){
		SessionScript.ButtonAudioLoud(SessionScript.photography);
		photographyWindow.SetActive(true);
		photographyWindow.transform.Find("Portrait").GetComponent<AvatarPortrait>().SpecificAvatar(SessionScript.player.avatar);
	}
	
	void TakePhotography(){
		// script for photography here
		// EzSS_Editor.ScreenshotButton();
	}
	
	void ClosePhotographyWindow(){
		photographyWindow.SetActive(false);
	}
	
	

    public void SelectMenu(){
        SessionScript.ButtonAudio(SessionScript.neutral);
        Invoke("EndScene", 0.5f);
        Invoke("NextScene", 1f);
    }

    public void NextScene(){
        SceneManager.LoadScene("menu", LoadSceneMode.Single);
    }

    void EndScene(){
        endScene = true;
    }
}
