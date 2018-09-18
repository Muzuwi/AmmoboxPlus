using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessBulletRubber : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Rubber Magazine");
            Tooltip.SetDefault("Very strong knockback, perfect for crowd control.");
        }

        public override void SetDefaults() {
            item.damage = 9;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.consumable = true;
            item.knockBack = 10f; 
            item.value = 25; 
            item.rare = ItemRarityID.Green;
            item.shoot = mod.ProjectileType("BulletRubber");
            item.shootSpeed = 5f;
            item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BulletRubber"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player){
            return false;
        }
    }
}
