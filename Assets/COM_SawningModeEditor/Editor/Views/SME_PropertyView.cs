using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Comburo.Editor.SME
{
    public class SME_PropertyView : SME_ViewBase
    {
        #region Public Variables

        #endregion

        #region Protected Variables

        #endregion

        #region Constructors
        public SME_PropertyView() : base("Propery View") { }
        #endregion

        #region Main Methods
        public override void UpdateView()
        {
            base.UpdateView();
            Debug.Log("Updating child view class");
        }

        #endregion

        #region Utility Methods

        #endregion
    }
}
