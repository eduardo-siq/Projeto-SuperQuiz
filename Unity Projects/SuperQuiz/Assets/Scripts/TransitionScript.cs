using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{

    public Animator transitionAnim;

    public static TransitionScript instance = null;


    public void Start()
    {
        instance = this;
        this.gameObject.transform.SetParent(GameObject.Find("Canvas").transform);
    }
    

    public static void StartAnim()
    {
        instance.transitionAnim.SetTrigger("end");
    }
}

