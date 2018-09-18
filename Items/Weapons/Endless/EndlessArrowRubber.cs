using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessArrowRubber : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Rubber Quiver");
            Tooltip.SetDefault("Very strong knockback, perfect for crowd control.");
        }

        public override void SetDefaults() {
            item.damage = 5;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.consumable = true;
            item.knockBack = 10f; 
            item.value = 15;
            item.rare = ItemRarityID.Green;
            item.shoot = mod.ProjectileType("ArrowRubber");
            item.shootSpeed = 5f;
            item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ArrowRubber"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player){
            return false;
        }
    }
}
