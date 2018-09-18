using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    class DartScrapper : ModItem{
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Supporter's Dartscrapper");
            Tooltip.SetDefault("Thank you for supporting Ammobox+!");
        }

        public override void SetDefaults() {
            item.damage = 52;
            item.autoReuse = true;
            item.useStyle = 5;
            item.useAnimation = 38;
            item.useTime = 38;
            item.width = 60;
            item.height = 22;
            item.shoot = 10;
            item.useAmmo = AmmoID.Dart;
            item.UseSound = SoundID.Item99;
            item.shootSpeed = 14.5f;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.knockBack = 5.5f;
            item.useAmmo = AmmoID.Dart;
            item.ranged = true;
            item.expert = true;
            item.expertOnly = false;
        }

        public override Vector2? HoldoutOffset() {
            return new Vector2(-17, 0);
        }

    }
}
