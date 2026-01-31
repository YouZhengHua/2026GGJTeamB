using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

public class MaskSelectCanvas : MonoBehaviour
{
    public GameManager gameManager;

    public bool isMaskCanvasActive;

    public Animator maskCanvasAnimator;

    public RectTransform L_Mask_ArchRect;
    public RectTransform R_Mask_ArchRect;

    public bool is_L_MaskClog = false;
    public bool is_R_MaskClog = false;

    public RectTransform maskA_ArchPos;
    public RectTransform maskB_ArchPos;
    public RectTransform maskC_ArchPos;
    public RectTransform maskD_ArchPos;
    public RectTransform maskE_ArchPos;
    public RectTransform maskF_ArchPos;

    public Image maskA;
    public Image maskB;
    public Image maskC;
    public Image maskD;
    public Image maskE;
    public Image maskF;

    public bool isMaskA_Installing;
    public bool isMaskB_Installing;
    public bool isMaskC_Installing;
    public bool isMaskD_Installing;
    public bool isMaskE_Installing;
    public bool isMaskF_Installing;

    public UnityEvent MaskA_Event_Install_Start;
    public UnityEvent MaskA_Event_Install_End;
    public UnityEvent MaskA_Event_Uninstall_Start;
    public UnityEvent MaskA_Event_Uninstall_End;

    public UnityEvent MaskB_Event_Install_Start;
    public UnityEvent MaskB_Event_Install_End;
    public UnityEvent MaskB_Event_Uninstall_Start;
    public UnityEvent MaskB_Event_Uninstall_End;

    public UnityEvent MaskC_Event_Install_Start;
    public UnityEvent MaskC_Event_Install_End;
    public UnityEvent MaskC_Event_Uninstall_Start;
    public UnityEvent MaskC_Event_Uninstall_End;

    public UnityEvent MaskD_Event_Install_Start;
    public UnityEvent MaskD_Event_Install_End;
    public UnityEvent MaskD_Event_Uninstall_Start;
    public UnityEvent MaskD_Event_Uninstall_End;

    public UnityEvent MaskE_Event_Install_Start;
    public UnityEvent MaskE_Event_Install_End;
    public UnityEvent MaskE_Event_Uninstall_Start;
    public UnityEvent MaskE_Event_Uninstall_End;

    public UnityEvent MaskF_Event_Install_Start;
    public UnityEvent MaskF_Event_Install_End;
    public UnityEvent MaskF_Event_Uninstall_Start;
    public UnityEvent MaskF_Event_Uninstall_End;

    public bool MaskAShow_Clog = false;
    public bool MaskBShow_Clog = false;
    public bool MaskCShow_Clog = false;
    public bool MaskDShow_Clog = false;
    public bool MaskEShow_Clog = false;
    public bool MaskFShow_Clog = false;

    public RectTransform Larger_Y_Extend;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    void Update()
    {
        detectMouse();
        MaskShowAndHide();
    }
    public void MyAnimatorActive()
    {
        //isMaskCanvasActive = ;
        maskCanvasAnimator.SetBool("isActive", isMaskCanvasActive);
    }
    
    public void detectMouse()
    {
        if (Input.mousePosition.y > Larger_Y_Extend.anchoredPosition.y)
        {
            isMaskCanvasActive = true;
            MyAnimatorActive();
        }
        else
        {
            isMaskCanvasActive = false;
            MyAnimatorActive(); 
        }

        // Debug.Log(Input.mousePosition);
    }

    public void Do_OtherThing()
    {
        isMaskCanvasActive = false;
        maskCanvasAnimator.SetBool("isActive", isMaskCanvasActive);
    }


    public void init()
    {

    }

    public void MaskShowAndHide()
    {
        MaskShow(gameManager.isMaskA_active, MaskAShow_Clog, maskA);
        MaskShow(gameManager.isMaskB_active, MaskAShow_Clog, maskB);
        MaskShow(gameManager.isMaskC_active, MaskAShow_Clog, maskC);
        MaskShow(gameManager.isMaskD_active, MaskAShow_Clog, maskD);
        MaskShow(gameManager.isMaskE_active, MaskAShow_Clog, maskE);
        MaskShow(gameManager.isMaskF_active, MaskAShow_Clog, maskF);
    }

    public void MaskShow(bool condition, bool clog, Image targetImg_P)
    {
        if (condition && !clog)
        {
            clog = true;
            Image targetImg = targetImg_P;
            targetImg.DOColor(new Color(targetImg.color.r, targetImg.color.g, targetImg.color.b, 1), 0.5f);
        }

    }

    public void MakeMaskInstall_L(Image mask, UnityEvent startEvent,UnityEvent endEvent)
    {
        is_L_MaskClog = true;
        startEvent.Invoke();
        Delay(() => { endEvent.Invoke(); }, 0.6f);
        mask.rectTransform.DOAnchorPosX(L_Mask_ArchRect.anchoredPosition.x, 0.6f);
    }
    public void MakeMaskInstall_R(Image mask, UnityEvent startEvent, UnityEvent endEvent)
    {
        is_R_MaskClog = true;
        startEvent.Invoke();
        Delay(() => { endEvent.Invoke(); }, 0.6f);
        mask.rectTransform.DOAnchorPosX(R_Mask_ArchRect.anchoredPosition.x, 0.6f);
    }

    public void MakeMaskUninstall(Image mask, Vector2 MaskOriPos, bool isLeft, UnityEvent startEvent, UnityEvent endEvent)
    {
        if (isLeft)
        {
            is_L_MaskClog = false;
        }
        else
        {
            is_R_MaskClog = false;
        }
        startEvent.Invoke();
        Delay(() => { endEvent.Invoke(); }, 0.6f);
        mask.rectTransform.DOAnchorPosX(MaskOriPos.x, 0.6f);
    }

    public void MaskA_ButtonClick()
    {
        MaskButton(gameManager.isMaskA_active, ref isMaskA_Installing, maskA, maskA_ArchPos.anchoredPosition, true, MaskA_Event_Install_Start, MaskA_Event_Install_End, MaskA_Event_Uninstall_Start, MaskA_Event_Uninstall_End);
        if (is_L_MaskClog == false)
        {
            //MaskButton(ref isMaskA_Installing, maskA, maskA_ArchPos.anchoredPosition, true);
        }
        else
        {

        }
        
    }
    public void MaskB_ButtonClick()
    {
        MaskButton(gameManager.isMaskB_active, ref isMaskB_Installing, maskB, maskB_ArchPos.anchoredPosition, true, MaskB_Event_Install_Start, MaskB_Event_Install_Start, MaskB_Event_Uninstall_Start, MaskB_Event_Uninstall_End);
        if (is_L_MaskClog == false)
        {
            //MaskButton(ref isMaskB_Installing, maskB, maskB_ArchPos.anchoredPosition, true);
        }
    }

    public void MaskC_ButtonClick()
    {
        MaskButton(gameManager.isMaskC_active, ref isMaskC_Installing, maskC, maskC_ArchPos.anchoredPosition, true, MaskC_Event_Install_Start, MaskC_Event_Install_End, MaskC_Event_Uninstall_Start, MaskC_Event_Uninstall_End);
        if (is_L_MaskClog == false)
        {
            //MaskButton(ref isMaskC_Installing, maskC, maskC_ArchPos.anchoredPosition, true);
        }
    }

    public void MaskD_ButtonClick()
    {
        MaskButton(gameManager.isMaskD_active, ref isMaskD_Installing, maskD, maskD_ArchPos.anchoredPosition, false, MaskD_Event_Install_Start, MaskD_Event_Install_End, MaskD_Event_Uninstall_Start, MaskD_Event_Uninstall_End);
        if (is_R_MaskClog == false)
        {
            //MaskButton(ref isMaskD_Installing, maskD, maskD_ArchPos.anchoredPosition, false);
        }
    }

    public void MaskE_ButtonClick()
    {
        MaskButton(gameManager.isMaskE_active, ref isMaskE_Installing, maskE, maskE_ArchPos.anchoredPosition, false, MaskE_Event_Install_Start, MaskE_Event_Install_End, MaskE_Event_Uninstall_Start, MaskE_Event_Uninstall_End);
        if (is_R_MaskClog == false)
        {
            //MaskButton(ref isMaskE_Installing, maskE, maskE_ArchPos.anchoredPosition, false);
        }
    }

    public void MaskF_ButtonClick()
    {
        MaskButton(gameManager.isMaskF_active, ref isMaskF_Installing, maskF, maskF_ArchPos.anchoredPosition, false, MaskF_Event_Install_Start, MaskF_Event_Install_End, MaskF_Event_Uninstall_Start, MaskF_Event_Uninstall_End);
        if (is_R_MaskClog == false)
        {
            //MaskButton(ref isMaskF_Installing, maskF, maskF_ArchPos.anchoredPosition, false);
        }
    }

    public void MaskButton(bool activeState ,ref bool ControlFlag, Image maskImage, Vector2 oriPos, bool isLeft, UnityEvent StartEvent, UnityEvent EndEvent, UnityEvent Un_StartEvent, UnityEvent Un_EndEvent)
    {
        if (!activeState) return;

        bool judgeTargetBool = false;
        if (isLeft)
        {
            judgeTargetBool = is_L_MaskClog;
        }
        else
        {
            judgeTargetBool = is_R_MaskClog;    
        }

        if (judgeTargetBool == false)
        {
            //Allow anyway

            ControlFlag = true;
            StartEvent.Invoke();
            if (isLeft)
            {
                MakeMaskInstall_L(maskImage, StartEvent, EndEvent);
            }
            else
            {
                MakeMaskInstall_R(maskImage, StartEvent, EndEvent);
            }
            
        }
        else
        {
            Debug.Log("unInstall");
            if (ControlFlag == true)
            {
                MakeMaskUninstall(maskImage, oriPos, isLeft, Un_StartEvent, Un_EndEvent);
                ControlFlag = false;
            }
            else
            {
                maskImage.rectTransform.DOShakeAnchorPos(0.5f, 20, 100, 45);
            }
        }


        /*
        ControlFlag = !ControlFlag;
        if (ControlFlag)
        {
            if (isLeft)
            {
                MakeMaskInstall_L(maskImage);
            }
            else
            {
                MakeMaskInstall_R(maskImage);
            }
        }
        else
        {
            MakeMaskUninstall(maskImage, oriPos, isLeft);
        }*/
    }

    public void Delay(Action act, float dur)
    {
        StartCoroutine(DelayCoroutine(act, dur));
    }
    public IEnumerator DelayCoroutine(Action act, float dur)
    {
        yield return new WaitForSeconds(dur);
        act();
    }
}
