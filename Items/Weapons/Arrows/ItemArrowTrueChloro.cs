using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class ArrowTrueChloro : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("True Chlorophyte Arrow");
            Tooltip.SetDefault("Chases after enemies.");
        }

        public override void SetDefaults() {
            item.damage = 16;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 1f;
            item.value = 20;
            item.rare = ItemRarityID.Lime;
            item.shoot = mod.ProjectileType("ArrowTrueChloro");
            item.shootSpeed = 4f;
            item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 20);
            recipe.AddRecipe();
        }
    }

}
