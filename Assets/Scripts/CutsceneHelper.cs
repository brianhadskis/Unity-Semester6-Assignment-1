using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneHelper : MonoBehaviour
{
    public delegate void BeginGame();
    public event BeginGame OnBeginGame;

    public delegate void StartZoom();
    public event StartZoom OnStartZoom;

    public delegate void StartFlyover();
    public event StartFlyover OnStartFlyover;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoBeginGame()
    {
        if (OnBeginGame != null) OnBeginGame();
    }

    public void StartPlayerZoomCutscene()
    {
        if (OnStartZoom != null) OnStartZoom();
    }

    public void StartFlyoverCutscene()
    {
        if (OnStartFlyover != null) OnStartFlyover();
    }
}
