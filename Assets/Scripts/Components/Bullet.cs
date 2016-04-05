using UnityEngine;

namespace Assets.Scripts.Components
{
    class Bullet : CustomComponentBase
    {
        Vector3 moveVector = Vector3.zero;
        public float damage = 100f;
        public float speed = 1f;
        public float maxTimeAlive = 5f;
        float timeCounter = 0f;

        public void SetMoveVector(Vector3 vect)
        {
            moveVector = vect * speed;
        }

        void Update()
        {
            _parent.position = _parent.position + moveVector;
            if(timeCounter >= maxTimeAlive)
            {
                Destroy(gameObject);
            }
            timeCounter += Time.deltaTime;
        }

        void OnCollisionEnter(Collision col)
        {
            Debug.Log("collided");
            HitPoints collidedHealth = col.gameObject.GetComponent<HitPoints>();

            if (collidedHealth)
            {
                collidedHealth.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
