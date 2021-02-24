using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Comburo.Editor.SME
{
    public static class SME_Menus
    {
        [MenuItem("Spawning Editor/Open Editor")]
        public static void InitSMEditor()
        {
            SME_EditorWindow.InitEditorWindow();
        }
    }
}
