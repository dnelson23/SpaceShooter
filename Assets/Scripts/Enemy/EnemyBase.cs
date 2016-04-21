using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Enemy
{
    public abstract class EnemyBase : Components.ControllerBase
    {
        Components.HitPoints _health;
        protected float maxHP = 100f;
        public float CurrentHP
        {
            get { return _health.curHitPoints; }
            private set { _health.SetMaxHitPoints(value); }
        }

        Components.RigidbodyMovement2D _movement;
        RigidbodyConstraints constraints = RigidbodyConstraints.FreezeRotation |
                                          RigidbodyConstraints.FreezePositionY;
        protected float acceleration = 10f;
        protected float maxSpeed = 10f;

        protected float damage = 100f;

        Character.ShipController player;

        protected override void Awake()
        {
            base.Awake();

            _movement = _parent.gameObject.AddComponent<Components.RigidbodyMovement2D>();
            _movement.rBody.constraints = constraints;
            _movement.acceleration = acceleration;

            _health = _parent.gameObject.AddComponent<Components.HitPoints>();
            CurrentHP = maxHP;

            player = GameObject.FindObjectOfType<Character.ShipController>();
        }
        
        void Update()
        {
            Vector3 toPlayer = (player.transform.position - transform.position).normalized;
            _movement.SetMoveVector(toPlayer);
            _movement.Rotate(toPlayer, 2f);
        }

        void OnCollisionEnter(Collision col)
        {
            Character.ShipController player = col.gameObject.GetComponent<Character.ShipController>();
            if (player != null)
            {
                player.GetComponent<Components.HitPoints>().TakeDamage(damage);
            }
        }

        public override void Destroy()
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                PowerUps.Powerup.DropRandomPowerup(_parent.position);
            }
            GameObject.Destroy(gameObject);
        }
    }
}
