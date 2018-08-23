using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus {
    class AmmoBox : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ammo Box (WIP)");
            Tooltip.SetDefault("A box containing some ammo\nRight Click to open");
        }

        public override void SetDefaults() {
            item.value = 100;
            item.consumable = true;
            item.rare = ItemRarityID.Orange;
            item.maxStack = 30;
        }

        public override void RightClick(Player player) {
            //  There's a method like this, neat
        }

    }
}