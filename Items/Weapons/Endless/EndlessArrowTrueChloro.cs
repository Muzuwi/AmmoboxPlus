using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessArrowTrueChloro : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless True Chlorophyte Quiver");
            Tooltip.SetDefault("Chases after enemies.");
        }

        public override void SetDefaults() {
            item.damage = 16;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.knockBack = 1f;
            item.value = 20;
            item.rare = ItemRarityID.Lime;
            item.shoot = mod.ProjectileType("ArrowTrueChloro");
            item.shootSpeed = 4f;
            item.ammo = AmmoID.Arrow;
        }        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ArrowTrueChloro"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }
    }
}
