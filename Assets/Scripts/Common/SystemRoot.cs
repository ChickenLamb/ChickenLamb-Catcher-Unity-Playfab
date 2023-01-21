
using UnityEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

public class SystemRoot : MonoBehaviour
{
    protected GameRoot root;
    protected NetService netService;
    protected ResourceService resourceService;
    protected AudioService audioService;
    public virtual void InitializeSystem()
    {
        root = GameRoot.instance;
        netService = NetService.instance;
        resourceService = ResourceService.instance;
        audioService = AudioService.instance;
    }
}

