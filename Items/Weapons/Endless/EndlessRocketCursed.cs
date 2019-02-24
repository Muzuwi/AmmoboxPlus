using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    class EndlessRocketCursed : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Cursed Crate");
            Tooltip.SetDefault("Small blast radius. Will not destroy tiles.\nInflicts 'Cursed'");
        }

        public override void SetDefaults() {
            item.damage = 35;
            item.ranged = true;
            item.width = 20;
            item.height = 14;
            item.maxStack = 1;
            item.knockBack = 2f;
            item.value = 15;
            item.rare = ItemRarityID.Lime;
            item.shootSpeed = 4f;
            item.shoot = mod.ProjectileType("RocketCursed");
            item.ammo = AmmoID.Rocket;
        }
        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("RocketCursed"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }
    }
}
