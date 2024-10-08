﻿using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using VitoBarra.GridSystem.Square;

namespace VitoBarra.GridSystem.Editor
{
    [CustomEditor(typeof(SquareGridSnappable))]

    public class SquareGridPlaceableEditor : UnityEditor.Editor
    {

        public VisualTreeAsset InspectorXMLFile;

        public override VisualElement CreateInspectorGUI()
        {
            // Create a new VisualElement to be the root of our inspector UI
            VisualElement finalInspector = new VisualElement();

            InspectorElement.FillDefaultInspector(finalInspector, serializedObject, this);


            if (InspectorXMLFile == null)
                InspectorXMLFile = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>( "Packages/com.vitobarra.gridsystem/Editor/UI/GridPlaceable.uxml");



            if (target is not SquareGridSnappable gridManager)
                return finalInspector;

            InspectorXMLFile.CloneTree(finalInspector);
            return finalInspector;
        }
    }
}