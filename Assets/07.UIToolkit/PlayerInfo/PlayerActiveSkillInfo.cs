

using UnityEngine;
using UnityEngine.UIElements;

public class PlayerActiveSkillInfo
{
    private VisualElement _cooldownMask;
    private VisualElement _skillImageMask;
    private VisualElement _skillImage;

    public PlayerActiveSkillInfo(VisualElement root)
    {
        _cooldownMask = root.Q<VisualElement>("CooldownMask");

        VisualElement skillImageGray = root.Q<VisualElement>("SkillImageGray");
        _skillImageMask = skillImageGray.Q<VisualElement>("SkillImageMask");
        _skillImage = _skillImageMask.Q<VisualElement>("SkillImage");
    }

    public void SetMaskValue(float currentValue, float maxValue)
    {
        float percent = currentValue / maxValue * 100f;

        _cooldownMask.style.width = new StyleLength(Length.Percent(percent));
        _skillImageMask.style.width = new StyleLength(Length.Percent(percent));
    }

    public void SetSkillImage(Sprite sprite)
    {
        _skillImage.style.backgroundImage = new StyleBackground(sprite);
    }
}