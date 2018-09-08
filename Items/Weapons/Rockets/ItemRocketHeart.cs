using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    class RocketHeart : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Regeneration Rocket");
            Tooltip.SetDefault("Small blast radius. Will not destroy tiles.\nEnemies have a chance to drop extra hearts and mana upon death");
        }

        public override void SetDefaults() {
            item.damage = 20;
            item.ranged = true;
            item.width = 20;
            item.height = 14;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f;
            item.value = 15;
            item.rare = ItemRarityID.Lime;
            item.shootSpeed = 0.5f;
            item.shoot = mod.ProjectileType("RocketHeart");
            item.ammo = AmmoID.Rocket;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LifeCrystal, 1);
            recipe.AddIngredient(ItemID.ManaCrystal, 1);
            recipe.AddIngredient(ItemID.RocketI, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 40);
            recipe.AddRecipe();
        }

    }
}