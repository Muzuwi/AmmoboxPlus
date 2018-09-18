using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessArrowSand : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Sandy Quiver");
            Tooltip.SetDefault("Applies 'Clouded Vision'\nEnemies have a very low chance to miss their attacks.");
        }

        public override void SetDefaults() {
            item.damage = 9;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.consumable = true;
            item.knockBack = 2f;
            item.value = 15;
            item.rare = ItemRarityID.Green;
            item.shoot = mod.ProjectileType("ArrowSand");
            item.shootSpeed = 5f;
            item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ArrowSand"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player){
            return false;
        }
    }
}
