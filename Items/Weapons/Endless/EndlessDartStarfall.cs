using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessDartStarfall : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Starfall Pouch");
            Tooltip.SetDefault("Has a low chance of turning into an enemy-piercing star.");
        }

        public override void SetDefaults() {
            item.damage = 8;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 15;
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("DartStarfall");
            item.shootSpeed = 3f;
            item.ammo = AmmoID.Dart;
        }        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DartStarfall"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player){
            return false;
        }
    }
}
