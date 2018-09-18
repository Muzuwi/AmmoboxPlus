using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    class RocketStarburst : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Starburst Rocket");
            Tooltip.SetDefault("Small blast radius. Will not destroy tiles.\nSometimes explodes into a barrage of stars");
        }

        public override void SetDefaults() {
            item.damage = 8;
            item.ranged = true;
            item.width = 20;
            item.height = 14;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f;
            item.value = 15;
            item.rare = ItemRarityID.Lime;
            item.shootSpeed = 3f;
            item.shoot = mod.ProjectileType("RocketStarburst");
            item.ammo = AmmoID.Rocket;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddIngredient(ItemID.RocketI, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }

    }
}