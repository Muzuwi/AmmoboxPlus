using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class ArrowRubber : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Rubber Arrow");
            Tooltip.SetDefault("Very strong knockback, perfect for crowd control.");
        }

        public override void SetDefaults() {
            item.damage = 5;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 10f; 
            item.value = 15; 
            item.rare = ItemRarityID.White;
            item.shoot = mod.ProjectileType("ArrowRubber");
            item.shootSpeed = 5f;
            item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WoodenArrow, 10);
            recipe.AddIngredient(ItemID.PinkGel, 5);
            recipe.SetResult(this, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.AddRecipe();
        }
    }

}
