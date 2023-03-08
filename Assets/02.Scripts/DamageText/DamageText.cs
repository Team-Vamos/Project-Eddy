using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TMP_Text Text { get; private set; }

    private void Awake() {
        Text = GetComponentInChildren<TMP_Text>();
    }

}
