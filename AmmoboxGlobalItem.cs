using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace AmmoboxPlus {
    class AmmoboxGlobalItem : GlobalItem {

        internal static List<string> projectileNameList = new List<string>() {
            "RocketStarburst",
            "RocketHarpy",
            "RocketSand",
            "RocketGolemfist",
            "RocketBunny",
            "RocketCursed",
            "RocketIchor",
            "RocketFrostburn",
            "RocketIce",
            "RocketMiner",
            "RocketBlackhole",
            "RocketChlorophyte",
            "RocketHeart"
        };

        public override bool InstancePerEntity {
            get {
                return true;
            }
        }

        public override GlobalItem NewInstance(Item item) {
            apAmmoUsedID = -1;
            return this;
        }

        //  Id of ammo used by launcher
        internal int apAmmoUsedID;

        public override void OpenVanillaBag(string context, Player player, int arg) {
            if (context == "bossBag") {
                if (AmmoboxPlus.AmmoboxBagAllowedPHM.Contains(arg) || AmmoboxPlus.AmmoboxBagModdedPHM.Contains(arg)) {
                    player.QuickSpawnItem(mod.ItemType("AmmoBox"));
                } else if (AmmoboxPlus.AmmoboxBagAllowedHM.Contains(arg) || AmmoboxPlus.AmmoboxBagModdedHM.Contains(arg)) {
                    player.QuickSpawnItem(mod.ItemType("AmmoBoxPlus"));
                } else {
                    return;
                }
            }
        }

        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
            //  Depending on the launcher, spawn projectile and set its' apShotFromLauncher to it's ID
            //Main.NewText("item " + item.type + ", " + type + " star" + mod.ItemType("RocketStarburst") + " proj" + mod.ProjectileType("RocketStarburst"));
            if (item.type == ItemID.GrenadeLauncher) {
                foreach (string name in projectileNameList) {
                    if (type == ProjectileID.GrenadeI + mod.ProjectileType(name)) {
                        int id = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType(name), damage, knockBack, player.whoAmI);
                        Main.projectile[id].GetGlobalProjectile<AmmoboxGlobalProjectile>(mod).apShotFromLauncherID = ItemID.GrenadeLauncher;
                        return false;
                    }
                }
            } else if (item.type == ItemID.RocketLauncher || item.type == mod.ItemType("Marine") || item.type == mod.ItemType("Boombox")) {
                foreach (string name in projectileNameList) {
                    if (type == ProjectileID.RocketI + mod.ProjectileType(name)) {
                        Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), mod.ProjectileType(name), damage, knockBack, player.whoAmI);
                        Main.projectile[proj.identity].GetGlobalProjectile<AmmoboxGlobalProjectile>(mod).apShotFromLauncherID = ItemID.RocketLauncher;
                        return false;
                    }
                }
            } else if (item.type == ItemID.ProximityMineLauncher) {
                foreach (string name in projectileNameList) {
                    if (type == ProjectileID.ProximityMineI + mod.ProjectileType(name)) {
                        int id = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType(name), damage, knockBack, player.whoAmI);
                        Main.projectile[id].GetGlobalProjectile<AmmoboxGlobalProjectile>(mod).apShotFromLauncherID = ItemID.ProximityMineLauncher;
                        return false;
                    }
                }
            } else if (item.type == ItemID.SnowmanCannon) {
                foreach (string name in projectileNameList) {
                    if (apAmmoUsedID != -1 && apAmmoUsedID == mod.ItemType(name)) {
                        int id = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType(name), damage, knockBack, player.whoAmI);
                        Main.projectile[id].GetGlobalProjectile<AmmoboxGlobalProjectile>(mod).apShotFromLauncherID = ItemID.SnowmanCannon;
                        return false;
                    }
                }
            }
            
            /* else if (item.type == ItemID.ElectrosphereLauncher) {
                foreach (string name in projectileNameList) {
                    if (apAmmoUsedID != -1 && apAmmoUsedID == mod.ItemType(name)) {
                        int id = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType(name), damage, knockBack, player.whoAmI, (Main.MouseWorld.X / 16f), (Main.MouseWorld.Y / 16f));
                        Main.projectile[id].GetGlobalProjectile<AmmoboxGlobalProjectile>(mod).apShotFromLauncherID = ItemID.ElectrosphereLauncher;
                        Main.projectile[id].GetGlobalProjectile<AmmoboxGlobalProjectile>(mod).apTargetLocation = Main.MouseWorld;
                        return false;
                    }
                }
            }*/

            return true;
        }

        public override void PickAmmo(Item item, Player player, ref int type, ref float speed, ref int damage, ref float knockback) {
            foreach(string name in projectileNameList) {
                if(item.type == mod.ItemType(name)) {
                    apAmmoUsedID = item.type;
                }
            }
        }
    }
}
