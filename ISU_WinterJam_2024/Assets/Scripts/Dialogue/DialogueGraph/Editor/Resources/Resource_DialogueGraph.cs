using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Resource_DialogueGraph : EditorWindow
{
    [SerializeField]
    private StyleSheet m_StyleSheet = default;

    [MenuItem("Window/UI Toolkit/Resource_DialogueGraph")]
    public static void ShowExample()
    {
        Resource_DialogueGraph wnd = GetWindow<Resource_DialogueGraph>();
        wnd.titleContent = new GUIContent("Resource_DialogueGraph");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // Add label
        VisualElement labelWithStyle = new Label("Hello World! With Style");
        labelWithStyle.AddToClassList("custom-label");
        labelWithStyle.styleSheets.Add(m_StyleSheet);
        root.Add(labelWithStyle);
    }
}
