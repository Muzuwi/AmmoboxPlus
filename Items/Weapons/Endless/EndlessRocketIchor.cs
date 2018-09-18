using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    class EndlessRocketIchor : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Ichor Crate");
            Tooltip.SetDefault("Small blast radius. Will not destroy tiles.\nInflicts 'Ichor'");
        }

        public override void SetDefaults() {
            item.damage = 40;
            item.ranged = true;
            item.width = 20;
            item.height = 14;
            item.maxStack = 1;
            item.consumable = true;
            item.knockBack = 2f;
            item.value = 15;
            item.rare = ItemRarityID.Lime;
            item.shootSpeed = 4f;
            item.shoot = mod.ProjectileType("RocketIchor");
            item.ammo = AmmoID.Rocket;
        }

            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("RocketIchor"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player){
            return false;
        }
    }
}