using UnityEngine;

namespace Assets.Scripts.Components
{
    class Weapon : CustomComponentBase
    {
        GameObject BulletPrefab;

        public void SetBulletPrefab(Bullet bullet)
        {
            if(bullet.gameObject == null)
            {
                Debug.LogWarning("Weapon has no bullet object, remember to pass a prefab with the \"Bullet\" to you're weapon component", _parent.gameObject);
                return;
            }
            BulletPrefab = bullet.gameObject;
        }

        public void Fire()
        {
            GameObject newBullet = Instantiate(BulletPrefab, _parent.position, _parent.rotation) as GameObject;
            newBullet.GetComponent<Bullet>().SetMoveVector(_parent.forward);
        }
    }
}
