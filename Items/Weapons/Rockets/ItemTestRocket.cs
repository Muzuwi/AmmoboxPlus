using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items {
    class TestRocket : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Test Rocket");
            Tooltip.SetDefault("");
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
            item.rare = ItemRarityID.Lime;
            item.shoot = mod.ProjectileType("TestRocket");
            item.shootSpeed = 3f;
            item.ammo = AmmoID.Rocket;
        }

    }
}