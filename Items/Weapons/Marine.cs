using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    class Marine : ModItem{
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Muzuwi's Marine");
            Tooltip.SetDefault("It gives off an aura of Doom\nThank you for supporting Ammobox+!");
        }

        public override void SetDefaults() {
            item.autoReuse = true;
            item.knockBack = 7f;
            item.useStyle = 5;
            item.useAnimation = 34;
            item.useTime = 34;
            item.width = 50;
            item.height = 14;
            item.shoot = 10;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item38;
            item.damage = 29;
            item.shootSpeed = 6f;
            item.noMelee = true;
            item.value = 700000;
            item.expert = true;
            item.expertOnly = false;
            item.ranged = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
            for (int i = 0; i < 6; i++) {
                float velX = speedX;
                float velY = speedY;
                velX += (float)Main.rand.Next(-40, 41) * 0.05f;
                velY += (float)Main.rand.Next(-40, 41) * 0.05f;
                Projectile.NewProjectile(position, new Vector2(velX, velY), type, damage, knockBack, player.whoAmI);
            }
            return false;
        }

        public override Vector2? HoldoutOffset() {
            return new Vector2(-6, 0);
        }

    }
}
