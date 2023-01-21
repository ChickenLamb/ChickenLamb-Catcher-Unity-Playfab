using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertWindow : WindowRoot
{
    public Image bgTips;
    public Text txtTips;
    public Animator animator;

    private Queue<string> tipsQue = new Queue<string>();
    private bool isTipsShow = false;
    protected override void InitializeWindow()
    {
        base.InitializeWindow();
        SetActive(bgTips, false);
        tipsQue.Clear();
    }
    private void Update()
    {
        if (tipsQue.Count > 0 && isTipsShow == false)
        {
            string tips = tipsQue.Dequeue();
            isTipsShow = true;
            SetTips(tips);
        }
    }
    private void SetTips(string tips)
    {
        int len = tips.Length;
        SetActive(bgTips);
        txtTips.text = tips;
      
        //bgTips.GetComponent<RectTransform>().sizeDelta = new Vector2(35 * len + 100, 80);
        //animator.Play(AnimationName.TipsWindow, 0, 0);
    }
    public void ShowAlert(string tips)
    {

        tipsQue.Enqueue(tips);
    }


    public void AnimationPlayCompleted()
    {
        SetActive(bgTips, false);
        isTipsShow = false;
    }

}
