using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimErrorScript : MonoBehaviour
{

    GameObject animationObj;

    float amountCircle = 0;
    float amountSymbol = 0;
    float amountSymbol2 = 0;
    public Image animationCircle;
    public Image animationSymbol;
    public Image animationSymbol2;
    bool circle = false;
    bool symbol = false;
    bool symbol2 = false;

    void Start()
    {
        animationObj = this.gameObject.transform.Find("Error").gameObject;
        animationCircle = animationObj.transform.Find("Circle").GetComponent<Image>();
        animationSymbol = animationObj.transform.Find("Symbol").GetComponent<Image>();
        animationSymbol2 = animationObj.transform.Find("Symbol2").GetComponent<Image>();
        animationCircle.fillAmount = amountCircle;
        animationSymbol.fillAmount = amountSymbol;
        animationSymbol2.fillAmount = amountSymbol2;
        animationObj.SetActive(false);
    }

    void Update()
    {
        if (circle)
        {
            amountCircle = amountCircle + Time.deltaTime * 3f;
            animationCircle.fillAmount = amountCircle;
        }
        if (amountCircle >= 1)
        {
            circle = false;
            symbol = true;
        }
        if (symbol)
        {
            amountSymbol = amountSymbol + Time.deltaTime * 3.5f;
            animationSymbol.fillAmount = amountSymbol;
        }
        if (amountSymbol >= 1)
        {
            symbol = false;
            symbol2 = true;
        }
        if (symbol2)
        {
            amountSymbol2 = amountSymbol2 + Time.deltaTime * 3.5f;
            animationSymbol2.fillAmount = amountSymbol2;
        }
        if (amountSymbol2 >= 1)
        {
            symbol2 = false;
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
        amountSymbol2 = 0;
        circle = false;
        symbol = false;
        symbol2 = false;
        animationCircle.fillAmount = 0;
        animationSymbol.fillAmount = 0;
        animationSymbol2.fillAmount = 0;
        animationObj.SetActive(false);
    }
}

