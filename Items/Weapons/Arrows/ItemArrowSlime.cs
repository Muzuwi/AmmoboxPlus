using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class ArrowSlime : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Slime Arrow");
            Tooltip.SetDefault("Slows down shot enemies.");
        }

        public override void SetDefaults() {
            item.damage = 6;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f;
            item.value = 15;
            item.rare = ItemRarityID.White;
            item.shoot = mod.ProjectileType("ArrowSlime");
            item.shootSpeed = 4f;
            item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Gel, 10);
            recipe.AddIngredient(ItemID.WoodenArrow, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }

    }

}
