using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimSucessScript : MonoBehaviour
{

    GameObject animationObj;

    float amountCircle = 0;
    float amountSymbol = 0;
    public Image animationCircle;
    public Image animationSymbol;
    bool circle = false;
    bool symbol = false;

    void Start()
    {
        animationObj = this.gameObject.transform.Find("Sucess").gameObject;
        animationCircle = animationObj.transform.Find("Circle").GetComponent<Image>();
        animationSymbol = animationObj.transform.Find("Symbol").GetComponent<Image>();
        animationCircle.fillAmount = amountCircle;
        animationSymbol.fillAmount = amountSymbol;
        animationObj.SetActive(false);
    }

    void Update()
    {
        if (circle)
        {
            amountCircle = amountCircle + Time.deltaTime * 2.5f;
            animationCircle.fillAmount = amountCircle;
        }
        if (amountCircle >= 1)
        {
            circle = false;
            symbol = true;
        }
        if (symbol)
        {
            amountSymbol = amountSymbol + Time.deltaTime * 2f;
            animationSymbol.fillAmount = amountSymbol;
        }
        if (amountSymbol >= 1)
        {
            symbol = false;
            Invoke("Reset", 1f);
        }
    }

    public void PlayAnimation()
    {
        animationObj.SetActive(true);
        circle = true;
    }

    void Reset()
    {
        amountCircle = 0;
        amountSymbol = 0;
        circle = false;
        symbol = false;
        animationCircle.fillAmount = 0;
        animationSymbol.fillAmount = 0;
        animationObj.SetActive(false);
    }
}

