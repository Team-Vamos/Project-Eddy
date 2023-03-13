using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Fillbar : VisualElement
{

    public new class UxmlFactory : UxmlFactory<Fillbar, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        private readonly UxmlFloatAttributeDescription _currentValue = new UxmlFloatAttributeDescription
        {
            name = "CurrentValue",
            defaultValue = 100
        };

        private readonly UxmlFloatAttributeDescription _maximumValue = new UxmlFloatAttributeDescription
        {
            name = "MaximumValue",
            defaultValue = 100
        };

        private readonly UxmlStringAttributeDescription _valueText = new UxmlStringAttributeDescription
        {
            name = "ValueText",
            defaultValue = string.Empty,
        };

        private readonly UxmlColorAttributeDescription _fillColor = new UxmlColorAttributeDescription
        {
            name = "FillTintColor",
            defaultValue = Color.white,
        };


        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);

            Fillbar fillbar = (Fillbar)ve;

            fillbar.CurrentValue = _currentValue.GetValueFromBag(bag, cc);
            fillbar.MaximumValue = _maximumValue.GetValueFromBag(bag, cc);
            fillbar.ValueText = _valueText.GetValueFromBag(bag, cc);
            fillbar.FillTintColor = _fillColor.GetValueFromBag(bag, cc);
        }
    }

    public float MaximumValue
    {
        get => _maximumValue;
        set
        {
            if (_maximumValue.Equals(value)) return;
            _maximumValue = value;
            UpdateInfo();
        }
    }
    private float _maximumValue;

    public float CurrentValue
    {
        get => _currentValue;
        set
        {
            if (_currentValue.Equals(value)) return;
            _currentValue = value;
            UpdateInfo();
        }
    }

    private float _currentValue;


    public string ValueText
    {
        get => _valueText.text;
        set => _valueText.text = value;
    }

    public Color FillTintColor
    {
        get => _fill.style.unityBackgroundImageTintColor.value;
        set
        {
            _fill.style.unityBackgroundImageTintColor = value;
        }
    }


    private readonly VisualElement _mask;
    private readonly VisualElement _fill;
    private readonly Label _valueText;

    public Fillbar()
    {
        _mask = new VisualElement { name = "FillbarMask" };
        _mask.style.overflow = new StyleEnum<Overflow>(Overflow.Hidden);
        Add(_mask);

        _fill = new VisualElement { name = "Fill" };

        _mask.Add(_fill);

        _valueText = new Label { name = "ValueText", text = string.Empty };
        Add(_valueText);
    }

    private void UpdateInfo()
    {
        FormatText();
        UpdateValue();
    }

    protected virtual void FormatText()
    {
        _valueText.text = $"{_currentValue.ToString()}/{_maximumValue.ToString()}";
    }

    protected virtual void UpdateValue()
    {
        float percent = _currentValue / _maximumValue * 100f;
        _mask.style.width = new StyleLength(Length.Percent(percent));
    }

}
