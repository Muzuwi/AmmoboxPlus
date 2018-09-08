using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    class RocketFrostburn : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Frostburn Rocket");
            Tooltip.SetDefault("Small blast radius. Will not destroy tiles.\nInflicts 'Frostburn'");
        }

        public override void SetDefaults() {
            item.damage = 30;
            item.ranged = true;
            item.width = 20;
            item.height = 14;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f;
            item.value = 15;
            item.rare = ItemRarityID.Lime;
            item.shootSpeed = 4f;
            item.shoot = mod.ProjectileType("RocketFrostburn");
            item.ammo = AmmoID.Rocket;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IceTorch, 10);
            recipe.AddIngredient(ItemID.RocketI, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }

    }
}