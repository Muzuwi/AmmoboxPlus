using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessDartCactus : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Cactus Pouch");
            Tooltip.SetDefault("Spikey!\nApplies a thorns-like effect that hurts other enemies.");
        }

        public override void SetDefaults() {
            item.damage = 5;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 10;
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("DartCactus");
            item.shootSpeed = 5f;
            item.ammo = AmmoID.Dart;
        }
        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DartCactus"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player){
            return false;
        }
    }
}
