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

        public override bool Autoload(ref string name)
        {
            return true;
        }

        //  Id of the launcher the projectile was shot from
        public int apShotFromLauncherID = -1;

    }
}
