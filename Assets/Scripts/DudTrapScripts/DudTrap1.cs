using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudTrap1 : Trap
{
    public override void ActivateTrap()
    {
        gameObject.SetActive(true);
    }

    public override void DeactivateTrap()
    {
        gameObject.SetActive(false);
    }
}
