using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using AmmoboxPlus.UI;

namespace AmmoboxPlus {
    class AmmoboxGlobalItem : GlobalItem {

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
                foreach (var entry in AmmoboxPlus.RocketNameTypes) {
                    if (type == ProjectileID.GrenadeI + entry.Value.Item2) {
                        int id = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, entry.Value.Item2, damage, knockBack, player.whoAmI);
                        Main.projectile[id].GetGlobalProjectile<AmmoboxGlobalProjectile>(mod).apShotFromLauncherID = ItemID.GrenadeLauncher;
                        return false;
                    }
                }
            } else if (item.type == ItemID.RocketLauncher || item.type == mod.ItemType("Marine") || item.type == mod.ItemType("Boombox")) {
                foreach (var entry in AmmoboxPlus.RocketNameTypes) {
                    if (type == ProjectileID.RocketI + entry.Value.Item2) {
                        Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), entry.Value.Item2, damage, knockBack, player.whoAmI);
                        Main.projectile[proj.identity].GetGlobalProjectile<AmmoboxGlobalProjectile>(mod).apShotFromLauncherID = ItemID.RocketLauncher;
                        return false;
                    }
                }
            } else if (item.type == ItemID.ProximityMineLauncher) {
                foreach (var entry in AmmoboxPlus.RocketNameTypes) {
                    if (type == ProjectileID.ProximityMineI + entry.Value.Item2) {
                        int id = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, entry.Value.Item2, damage, knockBack, player.whoAmI);
                        Main.projectile[id].GetGlobalProjectile<AmmoboxGlobalProjectile>(mod).apShotFromLauncherID = ItemID.ProximityMineLauncher;
                        return false;
                    }
                }
            } else if (item.type == ItemID.SnowmanCannon) {
                foreach (var entry in AmmoboxPlus.RocketNameTypes) {
                    if (apAmmoUsedID != -1 && apAmmoUsedID == entry.Value.Item2) {
                        int id = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, entry.Value.Item2, damage, knockBack, player.whoAmI);
                        Main.projectile[id].GetGlobalProjectile<AmmoboxGlobalProjectile>(mod).apShotFromLauncherID = ItemID.SnowmanCannon;
                        return false;
                    }
                }
            }
            return true;
        }

        public override void PickAmmo(Item item, Player player, ref int type, ref float speed, ref int damage, ref float knockback) {
            foreach (var entry in AmmoboxPlus.RocketNameTypes) {
                if (item.type == entry.Value.Item1) {
                    apAmmoUsedID = item.type;
                }
            }
        }

        public override void HoldItem(Item item, Player player) {
            if (!AmmoIconUI.vis) return;

            if (item.ranged && item.useAmmo == AmmoID.Bullet) { // Guns\
                SearchForAmmo(AmmoID.Bullet, ref player);
            } else if (item.ranged && item.useAmmo == AmmoID.Arrow) {  //  Bows and repeaters
                SearchForAmmo(AmmoID.Arrow, ref player);
            } else if (item.ranged && item.useAmmo == AmmoID.Rocket) { //  Rockets
                SearchForAmmo(AmmoID.Rocket, ref player);
            } else if (item.ranged && item.useAmmo == AmmoID.Dart) { //  Darts
                SearchForAmmo(AmmoID.Dart, ref player);
            } else if (item.ranged && item.useAmmo == AmmoID.Solution) { //  Rockets
                SearchForAmmo(AmmoID.Solution, ref player);
            } else {
                AmmoboxPlus.AmmoboxAmmoUI.icon.itemID = -1;
                AmmoboxPlus.AmmoboxAmmoUI.icon.mouseText = "";
            }
        }

        public static void SearchForAmmo(int ammoType, ref Player player) {
            //  Look in ammo slots first
            for (int i = 54; i <= 57; i++) {
                if (player.inventory[i].ammo == ammoType && player.inventory[i].active) {
                    //Main.NewText("Found a matching item " + player.inventory[i].type + " " + player.inventory[i].HoverName);
                    AmmoboxPlus.AmmoboxAmmoUI.icon.itemID = player.inventory[i].type;
                    AmmoboxPlus.AmmoboxAmmoUI.icon.mouseText = player.inventory[i].HoverName;
                    AmmoboxPlus.AmmoboxAmmoUI.icon.rarity = player.inventory[i].rare;
                    return;
                }
            }
            //  ...Then in inventory
            foreach (Item it in player.inventory) {
                if (it.ammo == ammoType && it.active) {
                    //Main.NewText("Found a matching item " + it.type + " " + it.HoverName);
                    AmmoboxPlus.AmmoboxAmmoUI.icon.itemID = it.type;
                    AmmoboxPlus.AmmoboxAmmoUI.icon.mouseText = it.HoverName;
                    AmmoboxPlus.AmmoboxAmmoUI.icon.rarity = it.rare;
                    return;
                }
            }
        }

    }
}
