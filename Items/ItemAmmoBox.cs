using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;
using AmmoboxPlus;

namespace AmmoboxPlus.Items{
    class AmmoBox : ModItem {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ammo Box");
            Tooltip.SetDefault("A box containing some ammo\nIt's heavy and rattling with the unknown\nRight Click to open");
        }

        public override void SetDefaults() {
            item.value = 100;
            item.consumable = true;
            item.rare = ItemRarityID.Blue;
            item.maxStack = 30;
        }

        public override bool CanRightClick() {
            return true;
        }

        public override void RightClick(Player player) {
            //  Load drop table
            var dropTable = new WeightedRandom<int>();
            foreach (var a in AmmoboxPlus.AmmoboxVanillaAmmo) {
                dropTable.Add(a.Key);
            }

            if(AmmoboxPlus.AmmoboxModAmmoPHM.Count > 0) {
                foreach(var a in AmmoboxPlus.AmmoboxModAmmoPHM) {
                    dropTable.Add(a.Key);
                }
            }

            int randomChoice = dropTable;
            int amount = 0;
            if (AmmoboxPlus.AmmoboxVanillaAmmo.ContainsKey((randomChoice))) {
                amount = AmmoboxPlus.AmmoboxVanillaAmmo[randomChoice];
            }else if (AmmoboxPlus.AmmoboxModAmmoPHM.ContainsKey(randomChoice)) {
                amount = AmmoboxPlus.AmmoboxModAmmoPHM[randomChoice];
            }

            int id = Item.NewItem(player.position, randomChoice, amount, prefixGiven: 0, noGrabDelay: true);
            if (Main.netMode == 1) {
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, id, 1f, 0f, 0f, 0, 0, 0);
            }
        }

    }
}