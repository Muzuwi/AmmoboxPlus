using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessBulletStarfall : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Starfall Magazine");
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
            item.value = 10;
            item.rare = ItemRarityID.Green;
            item.shoot = mod.ProjectileType("BulletStarfall");
            item.shootSpeed = 4f;
            item.ammo = AmmoID.Bullet;
        }        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BulletStarfall"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player){
            return false;
        }
    }
}
