using UnityEngine;

namespace AI.Health
{
    /// <summary>
    /// The health of the Cargo ships, derived from EnemyHealth class.
    /// </summary>
    public class CargoShipHealth: EnemyHealth
    {
        /// <summary>
        /// Takes damage if torpedo hit and notifies GameManager when
        /// ship is inactive to determine if player won the game.
        /// </summary>
        /// <param name="damage"></param>
        public override void DamageShip(float damage)
        {
            base.DamageShip(damage);

            if (!this.gameObject.activeInHierarchy)
            {
                ManagerScripts.GameManager.Instance.LostCargoShip();
            }
        }
    }
}