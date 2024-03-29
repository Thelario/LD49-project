﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CanvasType
{
    MainMenu,
    DifficultyPanel,
    OptionsMenu,
    InGameMenu,
    PauseGameMenu,
    DeadGameMenu
}

/// This is the class that will control the child objects and allow us to open and close each child
public class CanvasManager : Singleton<CanvasManager>
{
    List<CanvasController> canvasControllerList;
    public CanvasController lastActiveCanvas;

    protected void Awake()
    {
        //base.Awake();
        
        canvasControllerList = GetComponentsInChildren<CanvasController>(true).ToList();
        // The operation of transforming an array into a list using linq, as in the previous line of code, is a huge
        // costly operation, but as we are only going to do it once at the beginning of the game and for tiny operations
        // like this is not that horrible to do it.

        // This line iterates all the menus and deactivates them, using linq.
        canvasControllerList.ForEach(x => x.gameObject.SetActive(false));

        SwitchCanvas(CanvasType.MainMenu);

        Time.timeScale = 0f;
    }

    public void SwitchCanvas(CanvasType _type)
    {
        if (lastActiveCanvas != null)
        {
            lastActiveCanvas.gameObject.SetActive(false);
        }

        CanvasController desiredCanvas = canvasControllerList.Find(x => x.canvasType == _type);
        if (desiredCanvas != null)
        {
            desiredCanvas.gameObject.SetActive(true);
            lastActiveCanvas = desiredCanvas;
        }
        else { /* Debug.LogWarning("The desired menu canvas was not found!"); */ }
    }
}
