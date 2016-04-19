using UnityEngine;

namespace Assets.Scripts.Components
{
    class Weapon : CustomComponentBase
    {
        GameObject BulletPrefab;
        
        public void Fire()
        {
            GameObject newBullet = Instantiate(BulletPrefab, _parent.position, _parent.rotation) as GameObject;
            newBullet.GetComponent<Bullet>().SetMoveVector(_parent.forward);
        }

        public void SetBullet(GameObject newBullet)
        {
            if(newBullet == null)
            {
                throw new System.ArgumentNullException("Weapon Bullet", "Cannot set weapon bullet to null");
            }

            BulletPrefab = newBullet;
        }
    }
}
