using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessArrowCactus : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Cactus Quiver");
            Tooltip.SetDefault("Spikey!\nInflicts a thorns-like effect that hurts other enemies.");
        }

        public override void SetDefaults() {
            item.damage = 9;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.knockBack = 2f;
            item.value = 15;
            item.rare = ItemRarityID.Green;
            item.shoot = mod.ProjectileType("ArrowCactus");
            item.shootSpeed = 4f;
            item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ArrowCactus"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }
    }
}
