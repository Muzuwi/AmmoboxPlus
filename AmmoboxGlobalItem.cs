using Terraria;
using Terraria.ModLoader;

namespace AmmoboxPlus {
    class AmmoboxGlobalItem :  GlobalItem{

        public override void OpenVanillaBag(string context, Player player, int arg) {
            if(context == "bossBag") {
                if (AmmoboxPlus.AmmoboxBagAllowedPHM.Contains(arg)) {
                    player.QuickSpawnItem(mod.ItemType("AmmoBox"));
                }else if (AmmoboxPlus.AmmoboxBagAllowedHM.Contains(arg)) {
                    player.QuickSpawnItem(mod.ItemType("AmmoBoxPlus"));
                } else {
                    return;
                }
            }
        }
    }
}
