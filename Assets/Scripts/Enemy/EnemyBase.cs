using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Enemy
{
    public abstract class EnemyBase : Components.CustomComponentBase
    {
        Components.HitPoints _health;
        protected float maxHP = 100f;
        public float CurrentHP
        {
            get { return _health.curHitPoints; }
            private set { _health.SetMaxHitPoints(value); }
        }

        Components.RigidbodyMovement2D _movement;
        protected float acceleration = 10f;
        protected float maxSpeed = 10f;

        protected override void Awake()
        {
            base.Awake();

            _movement = _parent.gameObject.AddComponent<Components.RigidbodyMovement2D>();
            _movement.acceleration = acceleration;

            _health = _parent.gameObject.AddComponent<Components.HitPoints>();
            CurrentHP = maxHP;
        }

        void Update()
        {
            Vector3 toPlayer = (Character.ShipController.Instance.transform.position - _parent.transform.position).normalized;
            _movement.SetMoveVector(toPlayer);
            _movement.Rotate(toPlayer, 2f);
        }
    }
}
