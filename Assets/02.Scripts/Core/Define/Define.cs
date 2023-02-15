using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum FADECHILDS
{
    FADEOBJECT,
    TOPBAR,
    BOTTOMBAR,
}

[System.Serializable]
public class Bound
{
    [SerializeField]
    private Vector2 _lt;
    [SerializeField]
    private Vector2 _rb;

    public Vector2 Center => new Vector2(_lt.x + (_rb.x - _lt.x) * 0.5f, _lt.y + (_rb.y - _lt.y) * 0.5f);
    public Vector2 Size => new Vector2(_rb.x - _lt.x, _rb.y - _lt.y);

    public Vector2 GetRandomPos()
    {
        float x = Random.Range(_lt.x, _rb.x);
        float y = Random.Range(_lt.y, _rb.y);
        return new Vector2(x, y);
    }
}
public static class Define
{
    
    public static Camera MainCam
    {
        get
        {
            if (_mainCam == null)
            {
                _mainCam = Camera.main;
            }
            return _mainCam;
        }

    }

    private static Camera _mainCam;

    public static Camera UICam
    {
        get
        {
            return _uiCam;
        }
        set
        {
            if (_uiCam == null)
            {
                _uiCam = value;
            }
            else
            {
                return;
            }
        }
    }

    private static Camera _uiCam;

    public static Transform FadeParent
    {
        get
        {
            if (_fadeParent == null)
            {
                _fadeParent = GameObject.Find("FadeParent").transform;
            }
            return _fadeParent;
        }
    }

    private static Transform _fadeParent;



    public static Vector2 MousePos => MainCam.ScreenToWorldPoint(Input.mousePosition);

    public static T GetRandomEnum<T>(int startPos = 0,int? length = null ) where T : System.Enum
    {
        Array values = Enum.GetValues(typeof(T));
        if(length == null)
        {
            return (T)values.GetValue(Random.Range(startPos, values.Length));
        }
        else
            return (T)values.GetValue(Random.Range(startPos,length.Value));
    }

    public static Bound GetRandomBound(params Bound[] bounds)
    {

        float random = Random.Range(0f, 1f);
        float sizeAmount = 0f;

        float currentSizeRandom = 0f;

        for (int i = 0; i < bounds.Length; ++i)
        {
            sizeAmount += bounds[i].Size.magnitude;
        }
        for (int i = 0; i < bounds.Length; ++i)
        {
            if (random <= bounds[i].Size.magnitude / sizeAmount + currentSizeRandom)
            {
                return bounds[i];
            }
            else
            {
                currentSizeRandom += bounds[i].Size.magnitude / sizeAmount;
            }
        }

        int rand = Random.Range(0, bounds.Length);
        return bounds[rand];
    }
}
