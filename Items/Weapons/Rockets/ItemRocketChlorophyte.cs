using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    class RocketChlorophyte : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Chlorophyte Rocket");
            Tooltip.SetDefault("Small blast radius. Will not destroy tiles.\nChases after enemies");
        }

        public override void SetDefaults() {
            item.damage = 40;
            item.ranged = true;
            item.width = 20;
            item.height = 14;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f;
            item.value = 15;
            item.rare = ItemRarityID.Lime;
            item.shootSpeed = 4f;
            item.shoot = mod.ProjectileType("RocketChlorophyte");
            item.ammo = AmmoID.Rocket;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 1);
            recipe.AddIngredient(ItemID.RocketI, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }

    }
}