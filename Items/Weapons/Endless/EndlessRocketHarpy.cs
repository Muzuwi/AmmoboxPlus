using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    class EndlessRocketHarpy : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Harpy Crate");
            Tooltip.SetDefault("Small blast radius. Will not destroy tiles\nExplodes into a barrage of feathers");
        }

        public override void SetDefaults() {
            item.damage = 20;
            item.ranged = true;
            item.width = 20;
            item.height = 14;
            item.maxStack = 1;
            item.consumable = true;
            item.knockBack = 2f;
            item.value = 15;
            item.rare = ItemRarityID.Lime;
            item.shootSpeed = 6f;
            item.shoot = mod.ProjectileType("RocketHarpy");
            item.ammo = AmmoID.Rocket;
        }
        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("RocketHarpy"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player){
            return false;
        }
    }
}
