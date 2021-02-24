using UnityEditor;
using System.Collections;
using UnityEngine;
using System;

namespace Comburo.Editor.SME
{
    public class SME_EditorWindow : EditorWindow
    {
        #region Variables
        public static SME_EditorWindow win;
        public SME_PropertyView propertyView;
        public SME_WorkView workView;

        #endregion

        #region Main Methids
        public static void InitEditorWindow()
        {
            win = (SME_EditorWindow)EditorWindow.GetWindow<SME_EditorWindow>();
            win.titleContent.text = "Spawning Mode Editor";

            CreateViews();
        }

        

        private void OnEnable()
        {
            //Enable window.
        }

        private void OnDestroy()
        {
           //Disable window.
        }

        private void Update()
        {
            //Update content in window.
        }

        //Run twice for every update.
        private void OnGUI()
        {
            if (propertyView != null)
            {
                CreateViews();
                return;
            }
            EditorGUILayout.LabelField("Spawning mode editor content");
           // propertyView.UpdateView();
        }
        #endregion

        #region Utility Methods
        private static void CreateViews()
        {
            if (win != null)
            {
                win.propertyView = new SME_PropertyView();
            }
            else
            {
                win = (SME_EditorWindow)EditorWindow.GetWindow<SME_EditorWindow>();
            }
        }
        #endregion
    }
}