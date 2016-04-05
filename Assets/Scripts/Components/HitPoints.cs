namespace Assets.Scripts.Components
{
    class HitPoints : CustomComponentBase
    {
        public float curHitPoints { get; private set; }
        float maxHitPoints;

        public void SetHitPoints(float maxHP)
        {
            curHitPoints = maxHitPoints = maxHP;
        }

        public void TakeDamage(float amount)
        {
            curHitPoints -= amount;
        }

        public void Heal(float amount)
        {
            curHitPoints += amount;
            if(curHitPoints < maxHitPoints)
            {
                curHitPoints = maxHitPoints;
            }
        }
    }
}
