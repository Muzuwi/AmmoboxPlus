using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AmmoboxPlus.Items
{
    class ItemLihzahrdBelt : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lihzahrd Ammo Belt");
            // Tooltip.SetDefault("15% increased ranged damage\nAllows hotswapping between ammo in your ammo slots.\nAdditionally, it allows for swapping of one additional ammo type from your main inventory.");
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 1;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AmmoboxPlayer>().apCanUseBeltAdvanced = true;
            player.GetDamage(DamageClass.Ranged) += 0.15f;
        }

    }
}
