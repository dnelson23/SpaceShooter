using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Enemy
{
    class Tank : EnemyBase
    {
        void Start()
        {
            acceleration = 5f;
            damage = 150f;
        }
    }
}
