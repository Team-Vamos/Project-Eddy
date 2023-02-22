using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/NPC", fileName = "NPC")]
public class NPCSO : ScriptableObject
{
    public new string name;
    
    [Multiline]
    public string description;

    [Header("스텟")]
    public NPCStatController statController;
}
