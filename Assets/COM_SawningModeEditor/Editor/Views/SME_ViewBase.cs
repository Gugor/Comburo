using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SME_ViewBase
{
    #region Public Variables
    public string viewTitle;
    public Rect viewRect;

    #endregion

    #region Protected Variables
    protected GUISkin viewSkin;
    #endregion

    #region Contructors
    public SME_ViewBase(string title)
    {
        this.viewTitle = title;
    }

    #endregion


    #region Main Methods
    public virtual void UpdateView()
    {
        Debug.Log("Updating base view class");
    }
    public virtual void ProcessEvents() { }
    #endregion

    #region Utility Methods
    protected void GetEditorSkin() { }
    #endregion
}
