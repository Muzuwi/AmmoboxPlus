using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessArrowSlime : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Slime Quiver");
            Tooltip.SetDefault("Slows down shot enemies.");
        }

        public override void SetDefaults() {
            item.damage = 6;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.consumable = true;
            item.knockBack = 2f;
            item.value = 15;
            item.rare = ItemRarityID.Green;
            item.shoot = mod.ProjectileType("ArrowSlime");
            item.shootSpeed = 4f;
            item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ArrowSlime"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player){
            return false;
        }
    }
}
