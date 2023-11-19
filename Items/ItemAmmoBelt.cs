using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AmmoboxPlus.Items
{
    class ItemAmmoBelt : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ammo Belt");
            // Tooltip.SetDefault("15% increased ranged damage\nAllows hotswapping between ammo in your ammo slots.");
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 1;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AmmoboxPlayer>().apCanUseBeltBasic = true;
            player.GetDamage(DamageClass.Ranged) += 0.15f;
        }

    }
}
