using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessArrowBunny : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Peculiar Quiver");
            Tooltip.SetDefault("Has a very low chance of turning an enemy into a bunny.");
        }

        public override void SetDefaults() {
            item.damage = 4;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.knockBack = 2f;
            item.value = 10;
            item.rare = ItemRarityID.Green;
            item.shoot = mod.ProjectileType("ArrowBunny");
            item.shootSpeed = 2f;
            item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ArrowBunny"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }
    }
}
