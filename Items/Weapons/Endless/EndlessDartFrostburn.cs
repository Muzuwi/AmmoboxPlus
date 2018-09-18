using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessDartFrostburn : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Frostburn Pouch");
            Tooltip.SetDefault("Inflicts Frostburn.\n'Ice, Ice, Baby!'");
        }

        public override void SetDefaults() {
            item.damage = 7;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 10;
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("DartFrostburn");
            item.shootSpeed = 5f;
            item.ammo = AmmoID.Dart;
        }
        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DartFrostburn"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player){
            return false;
        }
    }
}
