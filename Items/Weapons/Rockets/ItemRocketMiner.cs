using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    class RocketMiner : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Miner's Rocket");
            Tooltip.SetDefault("Large blast radius. Will destroy tiles.\nEnemies have a chance to drop random amounts of ore.\nMining ore has a chance to drop extra ore");
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
            item.shootSpeed = 2f;
            item.shoot = mod.ProjectileType("RocketMiner");
            item.ammo = AmmoID.Rocket;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Dynamite, 1);
            recipe.AddIngredient(ItemID.RocketII, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }

    }
}