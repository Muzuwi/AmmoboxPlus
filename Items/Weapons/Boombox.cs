using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    class Boombox : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bunnyz Boombox");
            // Tooltip.SetDefault("'Music to my ears, death to my enemies'\nThank you for supporting Ammobox+!");
            Item.damage = 50;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useAmmo = AmmoID.Rocket;
            Item.width = 52;
            Item.height = 28;
            Item.shoot = ProjectileID.RocketI;
            Item.UseSound = SoundID.Item11;
            Item.shootSpeed = 5f;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.knockBack = 4f;
            Item.expert = true;
            Item.expertOnly = false;
            Item.DamageType = DamageClass.Ranged;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-17, -5);
        }
    }
}
