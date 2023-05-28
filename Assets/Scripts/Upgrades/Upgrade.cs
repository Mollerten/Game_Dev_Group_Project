using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Game/Upgrade")]
public class Upgrade : ScriptableObject
{
    public new string name;
    public string description;
    public int power;
    
    // Add more properties specific to your upgrade

    // You can also add methods or functions to perform specific actions related to the upgrade
    public virtual void Activate()
    {

    }
}
