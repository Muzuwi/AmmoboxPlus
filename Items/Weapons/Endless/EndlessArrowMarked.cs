using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessArrowMarked : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Marker Quiver");
            Tooltip.SetDefault("Inflicts 'Marked for Death'.\nInflicted enemies receive 15% more damage.");
        }

        public override void SetDefaults() {
            item.damage = 2;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.consumable = true;
            item.knockBack = 1f;
            item.value = 250;
            item.rare = ItemRarityID.Lime;
            item.shoot = mod.ProjectileType("ArrowMarked");
            item.shootSpeed = 10f;
            item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ArrowMarked"), 50);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player){
            return false;
        }
    }
}
