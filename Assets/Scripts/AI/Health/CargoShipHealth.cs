using UnityEngine;

namespace AI.Health
{
    public class CargoShipHealth: EnemyHealth
    {
        public override void DamageShip(float damage)
        {
            base.DamageShip(damage);

            if (!this.gameObject.activeInHierarchy)
            {
                print("Cargo Ship Health Disabled");
                ManagerScripts.GameManager.Instance.DecCargoShip();
            }
        }
    }
}