using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Palette;

public interface LevelInterface
{
    void addBreak(ColorName color);

    float getRespawnOffset();

    Palette getPalette();

    List<ColorName> getAvailable();  

    int[] getPoints();  
}

