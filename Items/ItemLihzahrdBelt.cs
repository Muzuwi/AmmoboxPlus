using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Linq;

namespace AmmoboxPlus.Items {
    class ItemLihzahrdBelt : ModItem{
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Lihzahrd Ammo Belt");
            Tooltip.SetDefault("15% increased ranged damage\nAllows hotswapping between ammo in your ammo slots.\nAdditionally, it allows for swapping of one additional ammo type from your main inventory.");
        }

        public override void SetDefaults() {
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = ItemRarityID.Green;
            item.maxStack = 1;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            player.GetModPlayer<AmmoboxPlayer>().apCanUseBeltAdvanced = true;
            player.rangedDamage += 0.15f;
        }

    }
}
