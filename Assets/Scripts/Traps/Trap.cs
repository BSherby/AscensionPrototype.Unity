using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    //This is the trap base class
    public abstract void ActivateTrap();

    public virtual void DeactivateTrap()

    {
        Debug.Log($"{gameObject.name} deactivated.");
    }
}
