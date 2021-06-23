using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friendly : Bug
{
    protected override void OnMouseDown()
    {
        level.incorrect++;
        level.LoseHealth();

        base.OnMouseDown();
    }
}
