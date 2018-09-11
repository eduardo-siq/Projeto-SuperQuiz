using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour{

    public Animator transitionAnim;

    public static TransitionScript instance = null;

    public RectTransform rect;

	// void Awake(){
		// if (instance == null){
			// print ("SINGLETON TRANSITION");
			// instance = this;
			// DontDestroyOnLoad(this.gameObject);
		// }
		// else{
			// if (instance != this){
			// Destroy(gameObject);
			// }
		// }
	// }


	public void Start(){
		instance = this;
		//this.gameObject.transform.SetParent(GameObject.Find("Canvas/Scroll View").transform);

		RaffleVariation();
		print ("SessionScript.startAnimationNextScene " + SessionScript.startAnimationNextScene);
		if (SessionScript.startAnimationNextScene){
			StartAnimation();
		}
	}

    public void RaffleVariation()
    {
        int random = Random.Range(0, 5);
        switch (random)
        {
            case 0:
                rect.anchorMax = new Vector2(0, 0);
                rect.anchorMin = new Vector2(0, 0);
                break;
            case 1:
                rect.anchorMax = new Vector2(1, 0);
                rect.anchorMin = new Vector2(1, 0);
                break;
            case 2:
                rect.anchorMax = new Vector2(1, 1);
                rect.anchorMin = new Vector2(1, 1);
                break;
            case 3:
                rect.anchorMax = new Vector2(1, 1);
                rect.anchorMin = new Vector2(1, 1);
                break;
            default:
                return;
                break;
        }
    }

    public static void SkipAnimation()
    {
        // instance.transitionAnim.SetBool("skip", true);
    }

    public static void PlayAnimation()
    {
        // instance.transitionAnim.SetBool("skip", false);
    }


    public static void StartAnimation()
    {
        instance.RaffleVariation();
        instance.transitionAnim.SetTrigger("in");
        SessionScript.startAnimationNextScene = false;
    }


    public static void EndAnimation()
    {
        SessionScript.startAnimationNextScene = true;
        instance.RaffleVariation();
        instance.transitionAnim.SetTrigger("out");
    }
}

