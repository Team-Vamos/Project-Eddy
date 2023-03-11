using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIDocumentMono : MonoBehaviour
{
    private UIDocument _document;

    protected VisualElement _root;
    protected virtual void Awake()
    {
        _document = GetComponent<UIDocument>();
    }

    protected virtual void OnEnable() {
        _root = _document.rootVisualElement;
    }

    protected void AppearRootVisualElement()
    {
        _root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
    }

    protected void DisappearRootVisualElement()
    {
        _root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
    }
}
