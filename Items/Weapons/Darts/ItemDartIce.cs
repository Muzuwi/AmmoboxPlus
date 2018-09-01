using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class DartIce : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ice Dart");
            Tooltip.SetDefault("Applies 'Cold' debuff (Like 'Chilled')\nHas a chance to freeze enemies in place.\nChance to freeze is doubled when enemy is in water.");
        }

        public override void SetDefaults() {
            item.damage = 13;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 20; 
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("DartIce");
            item.shootSpeed = 4f;
            item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IceFeather, 1);
            recipe.AddRecipeGroup("IronBar", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }

}
