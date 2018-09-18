using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    class RocketGolemfist : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Golemfist Rocket");
            Tooltip.SetDefault("Small blast radius. Will not destroy tiles.\nGreatly knocks back enemies");
        }

        public override void SetDefaults() {
            item.damage = 8;
            item.ranged = true;
            item.width = 20;
            item.height = 14;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f;
            item.value = 15;
            item.rare = ItemRarityID.Lime;
            item.shootSpeed = 3f;
            item.shoot = mod.ProjectileType("RocketGolemfist");
            item.ammo = AmmoID.Rocket;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LihzahrdBrick, 10);
            recipe.AddIngredient(ItemID.RocketIII, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }

    }
}