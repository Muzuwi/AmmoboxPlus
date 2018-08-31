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

            player.QuickSpawnItem(randomChoice, amount);

        }
    }
}