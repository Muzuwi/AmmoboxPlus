using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Linq;

namespace AmmoboxPlus.Items {
    class ItemAmmoBelt : ModItem{
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ammo Belt");
            Tooltip.SetDefault("15% increased ranged damage\nAllows hotswapping between ammo in your ammo slots.");
        }

        public override void SetDefaults() {
            item.value = Item.buyPrice(0, 30, 0, 0);
            item.rare = ItemRarityID.Green;
            item.maxStack = 1;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            AmmoboxPlayer.apCanUseBeltBasic = true;
            player.rangedDamage += 0.15f;
        }

    }
}
