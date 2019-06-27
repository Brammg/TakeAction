using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GarbageTypes
{
    public class Ball : Garbage
    {

        public Ball(Sprite spr, GameObject garbSprite, GameObject garbage3D) : base(garbage3D, garbSprite, spr)
        {
            garbType = "Ball";
        }

    }
}
