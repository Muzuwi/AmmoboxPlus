using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessBulletDrugged : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Drugged Magazine");
            Tooltip.SetDefault("Creates an aura around an enemy that damages nearby enemies");
        }

        public override void SetDefaults() {
            item.damage = 9;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 50;
            item.rare = ItemRarityID.Green;
            item.shoot = mod.ProjectileType("BulletDrugged");
            item.shootSpeed = 2f;
            item.ammo = AmmoID.Bullet;
        }        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BulletDrugged"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player){
            return false;
        }
    }
}
