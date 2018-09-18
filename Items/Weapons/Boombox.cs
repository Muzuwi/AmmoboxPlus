using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    class Boombox : ModItem{
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Bunnyz Boombox");
            Tooltip.SetDefault("'Music to my ears, death to my enemies'\nThank you for supporting Ammobox+!");
        }

        public override void SetDefaults() {
            item.damage = 50;
            item.useStyle = 5;
            item.autoReuse = true;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useAmmo = AmmoID.Rocket;
            item.width = 52;
            item.height = 28;
            item.shoot = 134;
            item.UseSound = SoundID.Item11;
            item.shootSpeed = 5f;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.knockBack = 4f;
            item.expert = true;
            item.expertOnly = false;
            item.ranged = true;
        }

        public override Vector2? HoldoutOffset() {
            return new Vector2(-17, -5);
        }
    }
}
