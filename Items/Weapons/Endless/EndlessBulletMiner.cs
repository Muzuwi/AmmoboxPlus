using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessBulletMiner : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Miner's Magazine");
            Tooltip.SetDefault("Has a low chance of dropping a random amount of ore from enemies");
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
            item.shoot = mod.ProjectileType("BulletMiner");
            item.shootSpeed = 2f;
            item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BulletMiner"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }
    }
}
