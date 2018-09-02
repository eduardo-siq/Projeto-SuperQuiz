using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SessionScript : MonoBehaviour
{

    public static SessionScript instance = null;

    // User & Game
    public static int score;
    public static int userGroup;
    public static bool firstLogIn;
    public bool getBuiltInQuestions;

    // Questions
    //public static bool loadQuestions = true;	// If the demo script is enabled, enable this 		// DEPRECATED
    public static List<QuestionPreLoad> questionListPreLoad;
    public static List<Question> questionList;
    public static List<int> questionsAskedList;
    public static List<Answer> answersList;
    public static int numberOfQuestionsDemanded = 5;
    //public static int pointsByQuestion = 50;	// maybe redundant
    public static float questionTime = 30f; // locally defined, maybe defined by BncQ later
    public static List<string> subjectName;
    public static List<string> userGroupName;   // maybe unecessary
    public static bool singleRun = true;

    // Point-and-Click
    public static Texture texturePoint;
    public static Texture2D pointAndClickSource;
    public static Sprite spritePoint;
    public static List<Detail> detail;
    public static bool useQuestionPointOffset = true;

    // Avatar
    public static List<Texture> avatarItem0;
    public static List<Texture> avatarItem1;
    public static List<Texture> avatarItem2;
    public static List<Texture> avatarItem3;
    public static List<Texture> avatarItem0b;
    public static List<Texture> avatarItem1b;
    public static List<Texture> avatarItem2b;
    public static List<Texture> avatarItem3b;
    public static List<Texture> avatarHairMasc;
    public static List<Texture> avatarHairFem;
    public static List<Texture> avatarBase;
    public static Texture avatarBlank;
    public static Avatar playerAvatar;
    public static int selectedItem0;
    public static int selectedItem1;
    public static int selectedItem2;
    public static int selectedItem3;
    public static int zeroTierMaxIndex;
    public static int firstTierMaxIndex;
    public static int secondTierMaxIndex;
    public static int thirdTierMaxIndex;
    public static Vector3 item0TierIndex;
    public static Vector3 item1TierIndex;
    public static Vector3 item2TierIndex;
    public static Vector3 item3TierIndex;

    // Sound
    public static AudioSource songAudio;
    public static AudioSource buttonAudio;
    public static AudioClip song1;
    public static AudioClip song2;
    public static AudioClip positive;
    public static AudioClip negative;
    public static AudioClip neutral;
    public static AudioClip subtle;
    public static AudioClip popUp;
    public static AudioClip popUpOut;
    public static AudioClip blop;
    public static int currentSong;
    public static bool soundOn;
    public static float soundVolume;

    // Auxiliary / Editor
    public static Texture missingTexture;
    public float questionsAskedListCount;
    public float answersListCount;
    public static string bncQFileName;
    [SerializeField] string bncQFileNameEditor;

    // Score
    public static int rightScore;
    public static int timeoutScore;
    public static int wrongScore;

    void Awake()
    {
        bncQFileName = bncQFileNameEditor;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        // Users
        score = 0;
        userGroup = -1; // default for no/unqualified user
        firstLogIn = true;

        // Auxiliary
        missingTexture = Resources.Load("Textures/Questions/missing") as Texture;


        // Questions
        questionListPreLoad = new List<QuestionPreLoad>();
        questionList = new List<Question>();
        questionsAskedList = new List<int>();
        answersList = new List<Answer>();
        subjectName = new List<string>();
        userGroupName = new List<string>();

        // Point-and-Click
        texturePoint = Resources.Load("Textures/PointAndClick/pointAndClick") as Texture;
        if (texturePoint == null)
        {
            print("missing texture");
            texturePoint = missingTexture;
        }
        pointAndClickSource = Resources.Load("Textures/PointAndClick/pointAndClickSource") as Texture2D;
        if (pointAndClickSource == null)
        {
            print("missing texture map");
            pointAndClickSource = Resources.Load("Textures/PointAndClick/missingSource") as Texture2D;
        }
        spritePoint = Resources.Load("Textures/PointAndClick/pointAndClickSprite", typeof(Sprite)) as Sprite;
        if (spritePoint == null)
        {
            print("missing sprite");
            spritePoint = Resources.Load("Textures/PointAndClick/missingSprite") as Sprite;
        }
        detail = new List<Detail>();
        GetDetailList();

        // Avatar
        avatarItem0 = new List<Texture>();
        avatarItem1 = new List<Texture>();
        avatarItem2 = new List<Texture>();
        avatarItem3 = new List<Texture>();
        avatarItem0b = new List<Texture>();
        avatarItem1b = new List<Texture>();
        avatarItem2b = new List<Texture>();
        avatarItem3b = new List<Texture>();
        avatarBlank = Resources.Load("Textures/Avatar/avatar_blank") as Texture;
        playerAvatar = new Avatar();
        selectedItem1 = 0;  // REMOVE LATER: REPLACE FOR LOADING FROM SAVE FILE
        selectedItem2 = 0;  // REMOVE LATER: REPLACE FOR LOADING FROM SAVE FILE
        selectedItem3 = 0;  // REMOVE LATER: REPLACE FOR LOADING FROM SAVE FILE
        firstTierMaxIndex = 5;
        secondTierMaxIndex = 5;
        thirdTierMaxIndex = 5;
        item0TierIndex = new Vector3(0, 0, 0);
        item1TierIndex = new Vector3(0, 0, 0);
        item2TierIndex = new Vector3(0, 0, 0);
        item3TierIndex = new Vector3(0, 0, 0);
        LoadAvatarAssets();


        //Sound
        songAudio = this.gameObject.AddComponent<AudioSource>();
        buttonAudio = this.gameObject.AddComponent<AudioSource>();
        positive = Resources.Load("Sound/positive_sound", typeof(AudioClip)) as AudioClip;
        negative = Resources.Load("Sound/negative_sound", typeof(AudioClip)) as AudioClip;
        neutral = Resources.Load("Sound/neutral_sound", typeof(AudioClip)) as AudioClip;
        popUp = Resources.Load("Sound/neutral_popUp", typeof(AudioClip)) as AudioClip;
        popUpOut = Resources.Load("Sound/neutral_popUpOut", typeof(AudioClip)) as AudioClip;
        blop = Resources.Load("Sound/neutral_blop", typeof(AudioClip)) as AudioClip;
        subtle = Resources.Load("Sound/subtle_sound", typeof(AudioClip)) as AudioClip;
        song1 = Resources.Load("Sound/trilhaSuperQuiz", typeof(AudioClip)) as AudioClip;
        song2 = Resources.Load("Sound/trilhaSuperQuiz", typeof(AudioClip)) as AudioClip;
        currentSong = 1;
        soundOn = true;
        soundVolume = 0.5f;

        // Score
        rightScore = 10;
        timeoutScore = 0;
        wrongScore = -10;

        StartCoroutine(UpdateScene());
        if (!getBuiltInQuestions)
        {
            print("load questions");
            StartCoroutine(LoadFile());
        }
        if (getBuiltInQuestions)
        {
            print("load built-in questions");
            StartCoroutine(LoadBuiltInQuestions());
        }
    }

    IEnumerator UpdateScene()
    {
        yield return null;
        questionsAskedListCount = questionsAskedList.Count;
        answersListCount = answersList.Count;   // MAYBE IRRELEVANT
        if (!songAudio.isPlaying)
        {
            if (currentSong == 1)
            {
                PlaySong(song2);
            }
            if (currentSong == 2)
            {
                PlaySong(song1);
            }
            currentSong = currentSong + 1;
            if (currentSong > 2) { currentSong = 1; }
        }
        StartCoroutine(UpdateScene());
    }

    public void GetDetailList()
    {
        print("GetDetailList");
        detail = Detail.GetBuiltInList();
        print("detail.Count: " + detail.Count);
    }

    public void StartNewScene()
    {
        songAudio.volume = soundVolume;
        buttonAudio.volume = soundVolume;
    }

    IEnumerator LoadFile()
    {
        yield return null;
        if (File.Exists(Application.persistentDataPath + "/" + bncQFileName + ".dat"))
        {
            print("load questions");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + "/" + bncQFileName + ".dat", FileMode.Open);
            BncQFile qdbFile = (BncQFile)bf.Deserialize(fileStream);

            // info loaded - start
            print("qdbFile.t.Count " + qdbFile.t.Count);
            for (int i = 0; i < qdbFile.t.Count; i++)
            {
                print(i);
                print("qdbFile.tp[" + i + "] " + qdbFile.tp[i]);
                print("qdbFile.t[" + i + "] " + qdbFile.t[i]);
                print("qdbFile.a[" + i + "] " + qdbFile.a[i]);
                print("qdbFile.b[" + i + "] " + qdbFile.b[i]);
                print("qdbFile.c[" + i + "] " + qdbFile.c[i]);
                print("qdbFile.d[" + i + "] " + qdbFile.d[i]);
                print("qdbFile.e[" + i + "] " + qdbFile.e[i]); ;
                if (qdbFile.u.Count > 0)
                {
                    print("qdbFile.u[" + i + "] " + qdbFile.u[i]);
                }
                if (qdbFile.s.Count > 0)
                {
                    print("qdbFile.s[" + i + "] " + qdbFile.s[i]);
                }
                questionListPreLoad.Add(new QuestionPreLoad(i, qdbFile.tp[i], qdbFile.t[i], qdbFile.a[i], qdbFile.b[i], qdbFile.c[i], qdbFile.d[i], qdbFile.e[i], qdbFile.u[i], qdbFile.s[i]));
                //questionsAskedList.Add(-1); // This list will be localy created
            }
            userGroupName = qdbFile.uName;
            subjectName = qdbFile.sName;
            // info loaded - end

            fileStream.Close();
        }
        else
        { // Missing BncQ
            print("there is no file to be loaded");
            // errorWindow.SetActive(true);
            // Destroy(characters);
            // Destroy(questionBox);
            // frame.SetActive(false);
            // errorWindow.transform.Find("Text").gameObject.GetComponent<Text>().text = "	Não foi possível localizar o banco de questões (" + Application.persistentDataPath + "/RPG_test_BncQ.dat) . Providencie o banco de quesões e reinicie o programa.";
        }
    }

    IEnumerator LoadBuiltInQuestions()
    {
        yield return null;
        questionListPreLoad = BuiltInQuestions.GetBuildInQuestions();
    }

    public static void GetQuestionListFromPreLoad()
    {
        print("GetQuestionListFromPreLoad");
        Texture missingTexture = Resources.Load("Textures/Questions/missing") as Texture;
        Texture texture;
        if (userGroup == -1) return;
        print("GetQuestionListFromPreLoad OK");
        print("questionListPreLoad.Count " + questionListPreLoad.Count);
        for (int i = 0; i < questionListPreLoad.Count; i++)
        {
            print("GetQuestionListFromPreLoad " + i);
            if (questionListPreLoad[i].userGroupString == "X")
            {   // No specific user
                print("get question " + i + "(i = " + i + ")");
                questionList.Add(new Question(questionListPreLoad[i]));
                if (questionList[i].questionType == 4)
                {
                    print("loading texture");
                    texture = Resources.Load("Textures/Questions/" + i.ToString()) as Texture;
                    if (texture != null)
                    {
                        questionList[i].questionImage = texture;
                        print("image loaded");
                    }
                    if (texture == null)
                    {
                        questionList[i].questionImage = missingTexture;
                        print("image not found");
                    }
                }
            }
            if (questionListPreLoad[i].userGroupString != "X")
            {
                for (int y = 0; y < questionListPreLoad[i].userGroup.Count; y++)
                {
                    if (questionListPreLoad[i].userGroup[userGroup] == true)
                    {
                        print("get question " + i + "(y = " + y + ")");
                        questionList.Add(new Question(questionListPreLoad[i]));
                        if (questionList[i].questionType == 4)
                        {
                            print("loading texture");
                            texture = Resources.Load("Textures/Questions/" + i.ToString()) as Texture;
                            if (texture != null)
                            {
                                questionList[i].questionImage = texture;
                                print("image loaded");
                            }
                            if (texture == null)
                            {
                                questionList[i].questionImage = missingTexture;
                                print("image not found");
                            }
                        }
                        break;
                    }
                }
            }
        }
        if (numberOfQuestionsDemanded > questionList.Count)
            numberOfQuestionsDemanded = questionList.Count;
    }

    public void LoadQuestions()
    {
        for (int i = 0; i < questionListPreLoad.Count; i++)
        {
            questionList.Add(new Question(questionListPreLoad[i]));
        }
    }

    void LoadAvatarAssets()
    {
        bool loadItems = true;
        bool loadHairFem = true;
        bool loadHairMasc = true;
        bool loadBase = true;
        int t = 0;  // item type
        int r = 0;  // item tier
        int i = 0;  // item index
        do
        {   // Item type 1: hat (?)
            Texture texture = Resources.Load("Textures/Avatar/avatar_" + t.ToString() + "_" + r.ToString() + "_" + i.ToString()) as Texture;    //avatar_type_tier_index
            if (texture != null)
            {
                Texture textureB = Resources.Load("Textures/Avatar/avatar_" + t.ToString() + "_" + r.ToString() + "_" + i.ToString() + "b") as Texture; //avatar_type_tier_index
                if (textureB == null)
                {
                    textureB = avatarBlank;
                }
                if (t == 0)
                {
                    avatarItem0.Add(texture);
                    avatarItem0b.Add(textureB);
                    print("loaded avatar textures! #: " + avatarItem0.Count + "/ " + t + " " + r + " " + i);
                }
                if (t == 1)
                {
                    avatarItem1.Add(texture);
                    avatarItem1b.Add(textureB);
                    print("loaded avatar textures! #: " + avatarItem1.Count + "/ " + t + " " + r + " " + i);
                }
                if (t == 2)
                {
                    avatarItem2.Add(texture);
                    avatarItem2b.Add(textureB);
                    print("loaded avatar textures! #: " + avatarItem2.Count + "/ " + t + " " + r + " " + i);
                }
                if (t == 3)
                {
                    avatarItem3.Add(texture);
                    avatarItem3b.Add(textureB);
                    print("loaded avatar textures! #: " + avatarItem3.Count + "/ " + t + " " + r + " " + i);
                }
                i = i + 1;
            }
            else
            {
                ItemTierIndex(i, r, t);
                i = 0;
                r = r + 1;
                print("change tier: " + r);
                if (r > 2)
                {
                    i = 0;
                    r = 0;
                    t = t + 1;
                    print("change type: " + t);
                }
                if (t > 3)
                {
                    loadItems = false;
                }
            }
        } while (loadItems);
        i = 0;
        do
        {
            Texture texture = Resources.Load("Textures/Avatar/avatar_hf_" + i.ToString()) as Texture;
            if (texture != null)
            {
                avatarHairFem.Add(texture);
            }
            else
            {
                loadHairFem = false;
            }
        } while (loadHairFem);
        i = 0;
        do
        {
            Texture texture = Resources.Load("Textures/Avatar/avatar_hm_" + i.ToString()) as Texture;
            if (texture != null)
            {
                avatarHairMasc.Add(texture);
            }
            else
            {
                loadHairMasc = false;
            }
        } while (loadHairMasc);
        i = 0;
        do
        {
            Texture texture = Resources.Load("Texture/Avatar/avatar_base_" + i.ToString()) as Texture;
            if (texture != null)
            {
                avatarBase.Add(texture);
            }
            else
            {
                loadBase = false;
            }
        } while (loadBase);
    }

    void ItemTierIndex(int i, int r, int t)
    {
        if (t == 0)
        {
            if (r == 0)
            {
                item0TierIndex.x = i;
            }
            if (r == 1)
            {
                item0TierIndex.y = i + item0TierIndex.x;
            }
            if (r == 2)
            {
                item0TierIndex.z = i + item0TierIndex.y;
            }
        }
        if (t == 1)
        {
            if (r == 0)
            {
                item1TierIndex.x = i;
            }
            if (r == 1)
            {
                item1TierIndex.y = i + item1TierIndex.x;
            }
            if (r == 2)
            {
                item1TierIndex.z = i + item1TierIndex.y;
            }
        }
        if (t == 2)
        {
            if (r == 0)
            {
                item2TierIndex.x = i;
            }
            if (r == 1)
            {
                item2TierIndex.y = i + item2TierIndex.x;
            }
            if (r == 2)
            {
                item2TierIndex.z = i + item2TierIndex.y;
            }
        }
        if (t == 3)
        {
            if (r == 0)
            {
                item3TierIndex.x = i;
            }
            if (r == 1)
            {
                item3TierIndex.y = i + item3TierIndex.x;
            }
            if (r == 2)
            {
                item3TierIndex.z = i + item3TierIndex.y;
            }
        }
        print("item 0 tier index " + item0TierIndex);
        print("item 1 tier index " + item1TierIndex);
        print("item 2 tier index " + item2TierIndex);
        print("item 3 tier index " + item3TierIndex);
    }

    public static void RaffleInitialAvatar()
    {
        playerAvatar.item0 = Mathf.RoundToInt(Random.Range(0, item0TierIndex.x));
        playerAvatar.item1 = Mathf.RoundToInt(Random.Range(0, item1TierIndex.x));
        playerAvatar.item2 = Mathf.RoundToInt(Random.Range(0, item2TierIndex.x));
        playerAvatar.item3 = Mathf.RoundToInt(Random.Range(0, item3TierIndex.x));
    }

    public static void RaffleAvatar(int maxItem0Index, int maxItem1Index, int maxItem2Index, int maxItem3Index)
    {
        if (avatarItem0.Count > 0 && avatarItem1.Count > 0 && avatarItem2.Count > 0 && avatarItem2.Count > 0)
        {
            selectedItem1 = Random.Range(0, maxItem0Index);
            selectedItem1 = Random.Range(0, maxItem1Index);
            selectedItem2 = Random.Range(0, maxItem2Index);
            selectedItem3 = Random.Range(0, maxItem3Index);
        }
    }

    public void PlaySong(AudioClip audio)
    {
        songAudio.PlayOneShot(audio, soundVolume);
    }

    public static void ButtonAudio(AudioClip audio)
    {
        buttonAudio.PlayOneShot(audio, soundVolume * 2);
    }

    public static void ButtonAudioLow(AudioClip audio)
    {
        buttonAudio.PlayOneShot(audio, soundVolume * 0.75f);
    }

    public static void TurnOnOffSound()
    {
        soundOn = !soundOn;
        if (soundOn)
        {
            soundVolume = 0.5f;
        }
        if (!soundOn)
        {
            soundVolume = 0f;
        }
        songAudio.volume = soundVolume;
        buttonAudio.volume = soundVolume;
    }

}
