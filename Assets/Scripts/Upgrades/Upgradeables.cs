using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Upgradeables", order = 1)]
public class Upgradeables : ScriptableObject
{
    public Texture2D Sprite = null;
    public float boostValue = 0;
}
