using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private bool isCursorLocked = false;
    
    // Start is called before the first frame update
    void Start()
    {
        LockCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isCursorLocked)
        {
            LockCursor();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isCursorLocked)
        {
            UnlockCursor();
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCursorLocked = true;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCursorLocked = false;
    }
}
