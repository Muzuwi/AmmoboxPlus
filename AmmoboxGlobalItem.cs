using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using AmmoboxPlus.UI;
using AmmoboxPlus.Items;

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

        public override bool Autoload(ref string name)
        {
            return true;
        }

        //  Id of ammo used by launcher
        internal int apAmmoUsedID;

        /*
         *  Adding ammoboxes to vanilla boss bags
         */
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

        /*
         *  Synchronize the shotFrom field across the clients for proper syncing of shot rockets
         */
        internal void broadcastShotFromValue(short shotFrom, int projType, int identity, int playerID) {
            //Main.NewText("Sending projectile id " + identity + " shot from " + shotFrom);
            Main.projectile[identity].netUpdate = true;
            ModPacket packet;
            packet = mod.GetPacket();
            packet.Write((byte)AmmoboxMsgType.AmmoboxBroadcastShotFrom);
            //  Owner id
            packet.Write(playerID);
            //  Projectile identity
            packet.Write(identity);
            //  Shot from which item?
            packet.Write(shotFrom);
            //  Projectile type
            packet.Write(projType);
            packet.Send();
        }

        /*
         *  Override vanilla shooting for rockets when custom ammo is in use
         */
        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
            //  Depending on the launcher, spawn projectile and set its' apShotFromLauncher to it's ID
            //Main.NewText("item " + item.type + ", " + type + " ammousedid " + apAmmoUsedID);
            if (item.type == ItemID.GrenadeLauncher) {
                foreach (var entry in AmmoboxPlus.RocketNameTypes) {
                    if (type == ProjectileID.GrenadeI + entry.Value.Item2) {
                        int id = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, entry.Value.Item2, damage, knockBack, player.whoAmI);
                        Main.projectile[id].GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID = ItemID.GrenadeLauncher;

                        if(Main.myPlayer == Main.projectile[id].owner && Main.netMode != NetmodeID.SinglePlayer) {
                            Main.projectile[id].netUpdate = true;
                            broadcastShotFromValue(ItemID.GrenadeLauncher, entry.Value.Item2, Main.projectile[id].identity, Main.myPlayer);
                        }

                        return false;
                    }
                }
            } else if (item.type == ItemID.RocketLauncher || item.type == mod.ItemType("Boombox")) {
                foreach (var entry in AmmoboxPlus.RocketNameTypes) {
                    if (type == ProjectileID.RocketI + entry.Value.Item2) {
                        Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), entry.Value.Item2, damage, knockBack, player.whoAmI);
                        Main.projectile[proj.identity].GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID = ItemID.RocketLauncher;

                        if (Main.myPlayer == Main.projectile[proj.identity].owner && Main.netMode != NetmodeID.SinglePlayer) {
                            Main.projectile[proj.identity].netUpdate = true;
                            broadcastShotFromValue(ItemID.RocketLauncher, entry.Value.Item2, proj.identity, Main.myPlayer);
                        }
                        return false;
                    }
                }
            } else if (item.type == ItemID.ProximityMineLauncher) {
                foreach (var entry in AmmoboxPlus.RocketNameTypes) {
                    if (type == ProjectileID.ProximityMineI + entry.Value.Item2) {
                        int id = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, entry.Value.Item2, damage, knockBack, player.whoAmI);
                        Main.projectile[id].GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID = ItemID.ProximityMineLauncher;

                        if (Main.myPlayer == Main.projectile[id].owner && Main.netMode != NetmodeID.SinglePlayer) {
                            Main.projectile[id].netUpdate = true;
                            broadcastShotFromValue(ItemID.ProximityMineLauncher, entry.Value.Item2, Main.projectile[id].identity, Main.myPlayer);
                        }

                        return false;
                    }
                }
            } else if (item.type == ItemID.SnowmanCannon) {
                foreach (var entry in AmmoboxPlus.RocketNameTypes) {
                    if (apAmmoUsedID != -1 && apAmmoUsedID == entry.Value.Item1) {
                        int id = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, entry.Value.Item2, damage, knockBack, player.whoAmI);
                        Main.projectile[id].GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID = ItemID.SnowmanCannon;

                        if (Main.myPlayer == Main.projectile[id].owner && Main.netMode != NetmodeID.SinglePlayer) {
                            Main.projectile[id].netUpdate = true;
                            broadcastShotFromValue(ItemID.SnowmanCannon, entry.Value.Item2, Main.projectile[id].identity, Main.myPlayer);
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        /*
         *  Record when one of the rocket ammos is used in the GlobalItem of the weapon
         */
        public override void PickAmmo(Item weapon, Item ammo, Player player, ref int type, ref float speed, ref int damage, ref float knockback) {
            foreach (var entry in AmmoboxPlus.RocketNameTypes) {
                if (ammo.type == entry.Value.Item1) {
                    apAmmoUsedID = ammo.type;
                    //Main.NewText("Ammo picked: " + apAmmoUsedID);
                }
            }
        }

        /*
         *  Update ammo hotswap when an item is held out
         */
        public override void HoldItem(Item item, Player player) {
            player.GetModPlayer<AmmoboxPlayer>().heldItemType = item.type;
            if(item.ranged && item.useAmmo != 0) {
                player.GetModPlayer<AmmoboxPlayer>().itemAllowed = true;
            } else {
                player.GetModPlayer<AmmoboxPlayer>().itemAllowed = false;
            }

            if (AmmoIconUI.vis) {
                if (item.ranged) {
                    SearchForAmmo(item.useAmmo, ref player);
                } else {
                    player.GetModPlayer<AmmoboxPlayer>().ammoDisplayItemID = -1;
                    player.GetModPlayer<AmmoboxPlayer>().ammoDisplayItemName = "";
                }
            }
        }

        /*
         *  Updates the ModPlayer with info on ammo type that will be used next when the player shoots a weapon
         */
        public static void SearchForAmmo(int ammoType, ref Player player) {
            //  Look in ammo slots first
            for (int i = 54; i <= 57; i++) {
                if (player.inventory[i].ammo == ammoType && player.inventory[i].active) {
                    //  TODO: Just pass an item instead of this
                    player.GetModPlayer<AmmoboxPlayer>().ammoDisplayItemID = player.inventory[i].type;
                    player.GetModPlayer<AmmoboxPlayer>().ammoDisplayItemName = player.inventory[i].HoverName;
                    player.GetModPlayer<AmmoboxPlayer>().ammoDisplayItemRarity = player.inventory[i].rare;
                    return;
                }
            }
            //  ...Then in inventory
            foreach (Item it in player.inventory) {
                if (it.ammo == ammoType && it.active) {
                    //  TODO: Just pass an item instead of this
                    player.GetModPlayer<AmmoboxPlayer>().ammoDisplayItemID = it.type;
                    player.GetModPlayer<AmmoboxPlayer>().ammoDisplayItemName = it.HoverName;
                    player.GetModPlayer<AmmoboxPlayer>().ammoDisplayItemRarity = it.rare;
                    return;
                }
            }
        }
    }
}
