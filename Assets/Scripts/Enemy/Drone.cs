using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Enemy
{
    class Drone : EnemyBase
    {

        void Awake()
        {
            acceleration = 12f;
            damage = 75f;
        }
    }
}
