using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;

public class MaskSelectCanvas : MonoBehaviour
{
    public bool isMaskCanvasActive;

    public Animator maskCanvasAnimator;
    
    public void MyAnimatorActive()
    {
        isMaskCanvasActive = true;
        maskCanvasAnimator.SetBool("isActive", isMaskCanvasActive);
    }

    public void init()
    {

    }

    public void Start()
    {
        //StartCoroutine(Delay(() => { Do_OtherThing(); Debug.Log("Test Function, auto close the tab"); },4f));   
    }

    public void Do_OtherThing()
    {
        isMaskCanvasActive = false;
        maskCanvasAnimator.SetBool("isActive", isMaskCanvasActive);
    }

    public void MaskA_ButtonClick()
    {

    }
    public void MaskB_ButtonClick()
    {

    }

    public void MaskC_ButtonClick()
    {

    }

    public void MaskD_ButtonClick()
    {

    }

    public void MaskE_ButtonClick()
    {

    }

    public void MaskF_ButtonClick()
    {

    }

    public IEnumerator Delay(Action act, float dur)
    {
        yield return new WaitForSeconds(dur);
        act();
    }
}
