using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    class DartScrapper : ModItem
    {
        public override void SetDefaults()
        {
            // DisplayName.SetDefault("Supporter's Dartscrapper");
            // Tooltip.SetDefault("Thank you for supporting Ammobox+!");
            Item.damage = 52;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 38;
            Item.useTime = 38;
            Item.width = 60;
            Item.height = 22;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Dart;
            Item.UseSound = SoundID.Item99;
            Item.shootSpeed = 14.5f;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.knockBack = 5.5f;
            Item.useAmmo = AmmoID.Dart;
            Item.DamageType = DamageClass.Ranged;
            Item.expert = true;
            Item.expertOnly = false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-17, 0);
        }

    }
}
