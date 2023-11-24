using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using AmmoboxPlus.UI;
using AmmoboxPlus.Items;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;

namespace AmmoboxPlus
{
    class AmmoboxGlobalItem : GlobalItem
    {

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public override GlobalItem NewInstance(Item item)
        {
            apAmmoUsedID = -1;
            return this;
        }

        // FIXME: Is this even required?
        public override bool IsLoadingEnabled(Mod mod)
        {
            return true;
        }

        //  Id of ammo used by launcher
        internal int apAmmoUsedID;

        /*
         *  Adding ammoboxes to vanilla boss bags
         */
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if (AmmoboxPlus.AmmoboxBagAllowedPHM.Contains(item.type) || AmmoboxPlus.AmmoboxBagModdedPHM.Contains(item.type))
            {
                itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("AmmoBox").Type));
            }
            else if (AmmoboxPlus.AmmoboxBagAllowedHM.Contains(item.type) || AmmoboxPlus.AmmoboxBagModdedHM.Contains(item.type))
            {
                itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("AmmoBoxPlus").Type));
            }
        }

        /*
         *  Synchronize the shotFrom field across the clients for proper syncing of shot rockets
         */
        internal void broadcastShotFromValue(short shotFrom, int projType, int identity, int playerID)
        {
            //Main.NewText("Sending projectile id " + identity + " shot from " + shotFrom);
            Main.projectile[identity].netUpdate = true;
            ModPacket packet;
            packet = Mod.GetPacket();
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
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //  Depending on the launcher, spawn projectile and set its' apShotFromLauncher to it's ID
            //Main.NewText("item " + item.type + ", " + type + " ammousedid " + apAmmoUsedID);
            if (item.type == ItemID.GrenadeLauncher)
            {
                foreach (var entry in AmmoboxPlus.RocketNameTypes)
                {
                    if (type == ProjectileID.GrenadeI + entry.Value.Item2)
                    {
                        int id = Projectile.NewProjectile(source, position, velocity, entry.Value.Item2, damage, knockback, player.whoAmI);
                        Main.projectile[id].GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID = ItemID.GrenadeLauncher;

                        if (Main.myPlayer == Main.projectile[id].owner && Main.netMode != NetmodeID.SinglePlayer)
                        {
                            Main.projectile[id].netUpdate = true;
                            broadcastShotFromValue(ItemID.GrenadeLauncher, entry.Value.Item2, Main.projectile[id].identity, Main.myPlayer);
                        }

                        return false;
                    }
                }
            }
            else if (item.type == ItemID.RocketLauncher || item.type == Mod.Find<ModItem>("Boombox").Type)
            {
                foreach (var entry in AmmoboxPlus.RocketNameTypes)
                {
                    if (type == ProjectileID.RocketI + entry.Value.Item2)
                    {
                        Projectile proj = Projectile.NewProjectileDirect(source, position, velocity, entry.Value.Item2, damage, knockback, player.whoAmI);
                        Main.projectile[proj.identity].GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID = ItemID.RocketLauncher;

                        if (Main.myPlayer == Main.projectile[proj.identity].owner && Main.netMode != NetmodeID.SinglePlayer)
                        {
                            Main.projectile[proj.identity].netUpdate = true;
                            broadcastShotFromValue(ItemID.RocketLauncher, entry.Value.Item2, proj.identity, Main.myPlayer);
                        }
                        return false;
                    }
                }
            }
            else if (item.type == ItemID.ProximityMineLauncher)
            {
                foreach (var entry in AmmoboxPlus.RocketNameTypes)
                {
                    if (type == ProjectileID.ProximityMineI + entry.Value.Item2)
                    {
                        int id = Projectile.NewProjectile(source, position, velocity, entry.Value.Item2, damage, knockback, player.whoAmI);
                        Main.projectile[id].GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID = ItemID.ProximityMineLauncher;

                        if (Main.myPlayer == Main.projectile[id].owner && Main.netMode != NetmodeID.SinglePlayer)
                        {
                            Main.projectile[id].netUpdate = true;
                            broadcastShotFromValue(ItemID.ProximityMineLauncher, entry.Value.Item2, Main.projectile[id].identity, Main.myPlayer);
                        }

                        return false;
                    }
                }
            }
            else if (item.type == ItemID.SnowmanCannon)
            {
                foreach (var entry in AmmoboxPlus.RocketNameTypes)
                {
                    if (apAmmoUsedID != -1 && apAmmoUsedID == entry.Value.Item1)
                    {
                        int id = Projectile.NewProjectile(source, position, velocity, entry.Value.Item2, damage, knockback, player.whoAmI);
                        Main.projectile[id].GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID = ItemID.SnowmanCannon;

                        if (Main.myPlayer == Main.projectile[id].owner && Main.netMode != NetmodeID.SinglePlayer)
                        {
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
        public override void PickAmmo(Item weapon, Item ammo, Player player, ref int type, ref float speed, ref StatModifier damage, ref float knockback)
        {
            foreach (var entry in AmmoboxPlus.RocketNameTypes)
            {
                if (ammo.type == entry.Value.Item1)
                {
                    apAmmoUsedID = ammo.type;
                    //Main.NewText("Ammo picked: " + apAmmoUsedID);
                }
            }
        }

        /*
         *  Update ammo hotswap when an item is held out
         */
        public override void HoldItem(Item item, Player player)
        {
            player.GetModPlayer<AmmoboxPlayer>().heldItemType = item.type;
            if (item.DamageType == DamageClass.Ranged && item.useAmmo != 0)
            {
                player.GetModPlayer<AmmoboxPlayer>().itemAllowed = true;
            }
            else
            {
                player.GetModPlayer<AmmoboxPlayer>().itemAllowed = false;
            }

            if (AmmoIconUI.vis)
            {
                if (item.DamageType == DamageClass.Ranged)
                {
                    SearchForAmmo(item.useAmmo, ref player);
                }
                else
                {
                    player.GetModPlayer<AmmoboxPlayer>().ammoDisplayItemID = -1;
                    player.GetModPlayer<AmmoboxPlayer>().ammoDisplayItemName = "";
                }
            }
        }

        /*
         *  Updates the ModPlayer with info on ammo type that will be used next when the player shoots a weapon
         */
        public static void SearchForAmmo(int ammoType, ref Player player)
        {
            //  Look in ammo slots first
            for (int i = 54; i <= 57; i++)
            {
                if (player.inventory[i].ammo == ammoType && player.inventory[i].active)
                {
                    //  TODO: Just pass an item instead of this
                    player.GetModPlayer<AmmoboxPlayer>().ammoDisplayItemID = player.inventory[i].type;
                    player.GetModPlayer<AmmoboxPlayer>().ammoDisplayItemName = player.inventory[i].HoverName;
                    player.GetModPlayer<AmmoboxPlayer>().ammoDisplayItemRarity = player.inventory[i].rare;
                    return;
                }
            }
            //  ...Then in inventory
            foreach (Item it in player.inventory)
            {
                if (it.ammo == ammoType && it.active)
                {
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
