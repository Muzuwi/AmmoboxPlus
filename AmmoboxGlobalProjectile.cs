using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus;
using Microsoft.Xna.Framework;

namespace AmmoboxPlus {
    class AmmoboxGlobalProjectile : GlobalProjectile {
        public override bool InstancePerEntity {
            get {
                return true;
            }
        }

        //  Id of the launcher the projectile was shot from
        public int apShotFromLauncherID = -1;

        /*public override bool CanHitPlayer(Projectile projectile, Player target) {
            if(projectile.type == ProjectileID.HarpyFeather && projectile.friendly && !projectile.hostile) {
                return false;
            }
            return true;
        }*/


        /*public override void AI(Projectile projectile) {
            if(projectile.type == ProjectileID.Electrosphere || projectile.type == ProjectileID.ElectrosphereMissile || projectile.type == ProjectileID.ProximityMineI) {
                Main.NewText(projectile.ai[0] + " " + projectile.ai[1]);
            }
        }*/

    }
}
