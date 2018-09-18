using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessBulletSpectral : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Phantasmal Magazine");
            Tooltip.SetDefault("Penetrates walls up to 4 blocks.\nAccuracy decreases significantly with each penetrated block.");
        }

        public override void SetDefaults() {
            item.damage = 20;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 50;
            item.rare = ItemRarityID.Lime;
            item.shoot = mod.ProjectileType("BulletSpectral");
            item.shootSpeed = 2f;
            item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BulletSpectral"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player){
            return false;
        }
    }
}
