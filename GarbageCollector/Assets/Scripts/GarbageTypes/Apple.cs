using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GarbageTypes
{
    public class Apple : Garbage
    {

        public Apple(Sprite spr, GameObject garbSprite, GameObject garbage3D) : base(garbage3D, garbSprite, spr)
        {
            garbType = "Apple";
        }

    }
}
