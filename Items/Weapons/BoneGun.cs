using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    class BoneGun : ModItem{
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Kranot's Kaliberfracture");
            Tooltip.SetDefault("An artist's weapon of choice\nThank you for supporting Ammobox+!");
        }

        public override void SetDefaults() {
            item.useStyle = 5;
            item.autoReuse = true;
            item.useAnimation = 5;
            item.useTime = 5;
            item.crit += 10;
            item.width = 66;
            item.height = 32;
            item.shoot = 10;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item40;
            item.damage = 77;
            item.shootSpeed = 12f;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.expert = true;
            item.expertOnly = false;
            item.knockBack = 2.5f;
            item.ranged = true;
        }

        public override Vector2? HoldoutOffset() {
            return new Vector2(-10, 0);
        }

        public override bool ConsumeAmmo(Player player) {
            if(Main.rand.Next(2) == 0) {
                return false;
            } else {
                return true;
            }
        }

    }
}
