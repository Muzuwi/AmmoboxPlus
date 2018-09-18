using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AmmoboxPlus.Items {
    class AmmoBoxPlus : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ammo Box Plus");
            Tooltip.SetDefault("A box containing some ammo\nIt's very heavy and radiating an aura of Power and Conflict\nRight Click to open");
        }

        public override void SetDefaults() {
            item.value = 100;
            item.consumable = true;
            item.rare = ItemRarityID.Orange;
            item.maxStack = 30;
        }

        public override bool CanRightClick() {
            return true;
        }

        public override void RightClick(Player player) {
            //  Spawn a dev weapon
            if(Main.rand.Next(20) == 0) {
                List<int> weaponList = new List<int>();

                if (NPC.downedPlantBoss) {
                    weaponList.Add(mod.ItemType("Marine"));
                    weaponList.Add(mod.ItemType("Boombox"));
                }
                if (NPC.downedMoonlord) {
                    weaponList.Add(mod.ItemType("BoneGun"));
                }
                if (Main.hardMode) {
                    weaponList.Add(mod.ItemType("DartScrapper"));
                }

                int id1 = Item.NewItem(player.position, weaponList[Main.rand.Next(4)], 1, prefixGiven: 0, noGrabDelay: true);
                if (Main.netMode == 1) {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, id1, 1f, 0f, 0f, 0, 0, 0);
                }
            }

            //  Load drop table
            var dropTable = new WeightedRandom<int>();
            foreach (var a in AmmoboxPlus.AmmoboxVanillaHMAmmo) {
                dropTable.Add(a.Key);
            }

            if (AmmoboxPlus.AmmoboxModAmmoHM.Count > 0) {
                foreach (var a in AmmoboxPlus.AmmoboxModAmmoHM) {
                    dropTable.Add(a.Key);
                }
            }

            int randomChoice = dropTable;
            int amount = 0;
            if (AmmoboxPlus.AmmoboxVanillaHMAmmo.ContainsKey((randomChoice))) {
                amount = AmmoboxPlus.AmmoboxVanillaHMAmmo[randomChoice];
            }
            else if (AmmoboxPlus.AmmoboxModAmmoHM.ContainsKey(randomChoice)) {
                amount = AmmoboxPlus.AmmoboxModAmmoHM[randomChoice];
            }

            int id = Item.NewItem(player.position, randomChoice, amount, prefixGiven: 0, noGrabDelay: true);
            if(Main.netMode == 1) {
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, id, 1f, 0f, 0f, 0, 0, 0);
            }
        }
    }
}