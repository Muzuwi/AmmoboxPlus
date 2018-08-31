using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class ArrowBunny : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Peculiar Arrow");
            Tooltip.SetDefault("Has a very low chance of turning an enemy into a bunny.");
        }

        public override void SetDefaults() {
            item.damage = 4;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f;
            item.value = 10;
            item.rare = ItemRarityID.Blue;
            item.shoot = mod.ProjectileType("ArrowBunny");
            item.shootSpeed = 2f;
            item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bunny, 1);
            recipe.AddIngredient(ItemID.WoodenArrow, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }
    }

}
