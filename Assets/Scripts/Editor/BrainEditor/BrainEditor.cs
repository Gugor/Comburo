using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Comburo.ScriptableObjects;

namespace Comburo
{
    [CustomEditor(typeof(Brain))]
    [CanEditMultipleObjects]
    public class BrainEditor : UnityEditor.Editor
    {
        private Brain _brain;
        private VisualElement rootElement;
        public VisualTreeAsset visualTreeAsset;
        private void OnEnable()
        {
            _brain = target as Brain;
            rootElement = new VisualElement(); //Parent element of the layout

            visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/BrainEditor/BrainEditor.uxml");
            StyleSheet uss = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/BrainEditor/BrainEditor.uss");
            AssetDatabase.Refresh();
            rootElement.styleSheets.Add(uss);
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = rootElement;
            root.Clear();

            visualTreeAsset.CloneTree(root);


            //var randomizeDifficulty = root.Q<>(name: "RandomizeDifficulty");
            Slider currentDifficultySlider = root.Q<Slider>(name: "CurrentDifficulty");
            currentDifficultySlider.value = _brain.difficulty;

            Label currentDifficultyDisplay = root.Q<Label>(name: "CurrentDifficultyDisplay");
            currentDifficultyDisplay.text = _brain.difficulty.ToString();

            ObjectField defaultDifficultySO = root.Q<ObjectField>(name: "DefaultDifficultyPool");
           // defaultDifficultySO.objectType = typeof(SpawningModesPoolSO);

            Foldout restfullFoldout = root.Q<Foldout>(name: "RestFullModesPool");

            return root;
        }


    }

}
