using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus;
using Microsoft.Xna.Framework;

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

        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
            if (item.type == ItemID.GrenadeLauncher && type == ProjectileID.GrenadeI + mod.ProjectileType("TestRocket")) {
                //Main.NewText("" + type + " " + mod.ProjectileType("TestRocket") + " " + mod.ItemType("TestRocket"));
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), mod.ProjectileType("TestRocket"), damage, knockBack, player.whoAmI);
                return false;
            } else if(item.type == ItemID.RocketLauncher && type == ProjectileID.RocketI + mod.ProjectileType("TestRocket")) {
                //Main.NewText("" + type + " " + mod.ProjectileType("TestRocket") + " " + mod.ItemType("ItemTestRocket"));
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), mod.ProjectileType("TestRocket"), damage, knockBack, player.whoAmI);
                return false;
            }
            return true;
        }

    }
}
