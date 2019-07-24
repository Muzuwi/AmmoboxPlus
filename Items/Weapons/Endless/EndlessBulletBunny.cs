using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessBulletBunny : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Peculiar Magazine");
            Tooltip.SetDefault("Has a very low chance of turning an enemy into a bunny.");
        }

        public override void SetDefaults() {
            item.damage = 8;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.knockBack = 2f; 
            item.value = 25;
            item.rare = ItemRarityID.Green;
            item.shoot = mod.ProjectileType("BulletBunny");
            item.shootSpeed = 2f;
            item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BulletBunny"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }
    }
}
