using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class ArrowCactus : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Cactus Arrow");
            Tooltip.SetDefault("Spikey!\nInflicts a thorns-like effect that hurts other enemies.");
        }

        public override void SetDefaults() {
            item.damage = 9;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f;
            item.value = 15;
            item.rare = ItemRarityID.White;
            item.shoot = mod.ProjectileType("ArrowCactus");
            item.shootSpeed = 4f;
            item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cactus, 5);
            recipe.AddIngredient(ItemID.WoodenArrow, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }
    }

}
