using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    class RocketIce : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ice Rocket");
            Tooltip.SetDefault("Large blast radius. Will not destroy tiles.\n");
        }

        public override void SetDefaults() {
            item.damage = 35;
            item.ranged = true;
            item.width = 20;
            item.height = 14;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f;
            item.value = 15;
            item.rare = ItemRarityID.Lime;
            item.shootSpeed = 5f;
            item.shoot = mod.ProjectileType("RocketIce");
            item.ammo = AmmoID.Rocket;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IceFeather, 1);
            recipe.AddIngredient(ItemID.RocketIII, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }

    }
}