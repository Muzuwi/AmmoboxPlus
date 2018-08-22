using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class DartYang : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Yang Dart");
            Tooltip.SetDefault("Deals great damage when paired with the Ying Dart.\n'All good contains a modicum of evil'");
        }

        public override void SetDefaults() {
            item.damage = 2;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 150; 
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("DartYang");
            item.shootSpeed = 4f;
            item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DarkShard, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 3);
            recipe.AddRecipe();
        }

    }

}
