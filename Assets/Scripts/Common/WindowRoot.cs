using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WindowRoot : MonoBehaviour
{
    protected GameRoot root;
    protected NetService netService;
    protected ResourceService resourceService;
    protected AudioService audioService;
    public virtual void SetWindowState(bool isActive = true)
    {
        if (gameObject.activeSelf != isActive)
            gameObject.SetActive(isActive);

        if (isActive)
        {
            InitializeWindow();
        }
        else
        {
            UninitializedWindow();
        }
    }
    protected virtual void InitializeWindow()
    {
        root = GameRoot.instance;
        netService = NetService.instance;
        resourceService = ResourceService.instance;
        audioService = AudioService.instance;
    }
    protected virtual void UninitializedWindow()
    {
        root = null;
        netService = null;
        resourceService = null;
        audioService = null;
    }
    protected void SetActive(GameObject go, bool state = true)
    {
        go.SetActive(state);
    }
    protected void SetActive(Transform trans, bool state = true)
    {
        trans.gameObject.SetActive(state);
    }
    protected void SetActive(RectTransform rectTrans, bool state = true)
    {
        rectTrans.gameObject.SetActive(state);
    }
    protected void SetActive(Image img, bool state = true)
    {
        img.gameObject.SetActive(state);
    }
    protected void SetActive(Text text, bool state = true)
    {
        text.gameObject.SetActive(state);
    }
    protected void SetActive(InputField inpt, bool state = true)
    {
        inpt.gameObject.SetActive(state);
    }
    //protected void SetSprite(Image image, string path)
    //{
    //    Sprite sp = ResourceService.instance.LoadSprite(path, true);
    //    image.sprite = sp;
    //}
    protected Image GetImage(Transform trans, string path)
    {
        if (trans != null)
        {
            return trans.Find(path).GetComponent<Image>();
        }
        else
        {
            return transform.Find(path).GetComponent<Image>();
        }
    }
    protected Image GetImage(Transform trans)
    {
        if (trans != null)
        {
            return trans.GetComponent<Image>();
        }
        else
        {
            return transform.GetComponent<Image>();
        }
    }
    protected Text GetText(Transform trans, string path)
    {
        if (trans != null)
        {
            return trans.Find(path).GetComponent<Text>();
        }
        else
        {
            return transform.Find(path).GetComponent<Text>();
        }
    }
    protected Transform GetTrans(Transform trans, string name)
    {
        if (trans != null)
        {
            return trans.Find(name);
        }
        else
        {
            return transform.Find(name);
        }
    }
    protected void SetText(Text txt, string context = "")
    {
        txt.text = context;
    }
    protected void SetText(Text txt, int context = 0)
    {
        SetText(txt, context.ToString());
    }
    protected void SetText(Transform trans, string context = "")
    {
        SetText(trans.GetComponent<Text>(), context);
    }
    protected void SetText(Transform trans, int context = 0)
    {
        SetText(trans.GetComponent<Text>(), context.ToString());
    }
    private T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T t = go.GetComponent<T>();
        if (t == null)
        {
            t = go.AddComponent<T>();
        }
        return t;
    }
    //protected void OnClick(GameObject go, Action<PointerEventData, object[]> clickCB, params object[] args)
    //{
    //    PEListener listener = GetOrAddComponent<PEListener>(go);
    //    listener.onClick = clickCB;
    //    if (args != null)
    //    {
    //        listener.args = args;
    //    }
    //}
    //protected void OnClickDown(GameObject go, Action<PointerEventData, object[]> clickDownCB, params object[] args)
    //{
    //    PEListener listener = GetOrAddComponent<PEListener>(go);
    //    listener.onClickDown = clickDownCB;
    //    if (args != null)
    //    {
    //        listener.args = args;
    //    }
    //}
    //protected void OnClickUp(GameObject go, Action<PointerEventData, object[]> clickUpCB, params object[] args)
    //{
    //    PEListener listener = GetOrAddComponent<PEListener>(go);
    //    listener.onClickUp = clickUpCB;
    //    if (args != null)
    //    {
    //        listener.args = args;
    //    }
    //}
    //protected void OnDrag(GameObject go, Action<PointerEventData, object[]> dragCB, params object[] args)
    //{
    //    PEListener listener = GetOrAddComponent<PEListener>(go);
    //    listener.onDrag = dragCB;
    //    if (args != null)
    //    {
    //        listener.args = args;
    //    }
    //}
}
