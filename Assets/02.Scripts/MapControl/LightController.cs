using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    private Light2D _light2D; // Global light
    
    private void Awake()
    {
        _light2D = GetComponent<Light2D>();
    }
    
    public void SetIntensity(float intensity)
    {
        _light2D.intensity = intensity;
    }
    
    public void SetIntensity(float intensity, float duration)
    {
        DOTween.To(() => _light2D.intensity, x => _light2D.intensity = x, intensity, duration);
    }
    
    public void SetColor(Color color)
    {
        _light2D.color = color;
    }
    
    public void SetColor(Color color, float duration)
    {
        DOTween.To(() => _light2D.color, x => _light2D.color = x, color, duration);
    }
}