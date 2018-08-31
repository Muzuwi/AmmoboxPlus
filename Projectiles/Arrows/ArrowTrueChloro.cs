using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AmmoboxPlus.Projectiles {
    public class ArrowTrueChloro : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("True Chlorophyte Arrow");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults() {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = 1;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.alpha = 1;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
        }

        public override void AI() {

            //  This entire function is adapted from vanilla
            //  Variable names might not be true to their function

            for (int index1 = 0; index1 < 10; ++index1) {
                float num1 = (float)(projectile.position.X - projectile.velocity.X / 10.0 * (double)index1);
                float num2 = (float)(projectile.position.Y - projectile.velocity.Y / 10.0 * (double)index1);
                int index2 = Dust.NewDust(new Vector2(num1, num2), 1, 1, 75, 0.0f, 0.0f, 0, Color.Lime, 1f);
                Main.dust[index2].alpha = projectile.alpha;
                Main.dust[index2].position.X = (float)num1;
                Main.dust[index2].position.Y = (float)num2;
                Main.dust[index2].velocity = new Vector2(0,0);
                Main.dust[index2].noGravity = true;
            }

            float velVector = (float)Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y));
            float locAIzero = projectile.localAI[0];
            if ((double)locAIzero == 0.0) {
                projectile.localAI[0] = velVector;
                locAIzero = velVector;
            }
            if (projectile.alpha > 0)
                projectile.alpha = projectile.alpha - 25;
            if (projectile.alpha < 0)
                projectile.alpha = 0;


            float projX = (float)projectile.position.X;
            float projY = (float)projectile.position.Y;
            float maxDistance = 300f;
            bool foundValidTarget = false;
            int detectedTargetID = 0;
            if ((double)projectile.ai[1] == 0.0) {
                for (int index = 0; index < 200; ++index) {
                    if (Main.npc[index].CanBeChasedBy((object)projectile, false) && ((double)projectile.ai[1] == 0.0 || (double)projectile.ai[1] == (double)(index + 1))) {
                        float npcCenterX = (float)Main.npc[index].position.X + (float)(Main.npc[index].width / 2);
                        float npcCenterY = (float)Main.npc[index].position.Y + (float)(Main.npc[index].height / 2);
                        float npcProjDist = Math.Abs((float)projectile.position.X + (float)(projectile.width / 2) - npcCenterX) + Math.Abs((float)projectile.position.Y + (float)(projectile.height / 2) - npcCenterY);
                        if ((double)npcProjDist < (double)maxDistance && Collision.CanHit(new Vector2((float)projectile.position.X + (float)(projectile.width / 2), (float)projectile.position.Y + (float)(projectile.height / 2)), 1, 1, Main.npc[index].position, Main.npc[index].width, Main.npc[index].height)) {
                            maxDistance = npcProjDist;
                            projX = npcCenterX;
                            projY = npcCenterY;
                            foundValidTarget = true;
                            detectedTargetID = index;
                        }
                    }
                }
                if (foundValidTarget)
                    projectile.ai[1] = (float)(detectedTargetID + 1);
                foundValidTarget = false;
            }


            if ((double)projectile.ai[1] > 0.0) {
                int index = (int)((double)projectile.ai[1] - 1.0);
                if (Main.npc[index].active && Main.npc[index].CanBeChasedBy((object)projectile, true) && !Main.npc[index].dontTakeDamage) {
                    if ((double)Math.Abs((float)projectile.position.X + (float)(projectile.width / 2) - ((float)Main.npc[index].position.X + (float)(Main.npc[index].width / 2))) + (double)Math.Abs((float)projectile.position.Y + (float)(projectile.height / 2) - ((float)Main.npc[index].position.Y + (float)(Main.npc[index].height / 2))) < 1000.0) {
                        foundValidTarget = true;
                        projX = (float)Main.npc[index].position.X + (float)(Main.npc[index].width / 2);
                        projY = (float)Main.npc[index].position.Y + (float)(Main.npc[index].height / 2);
                    }
                }
                else
                    projectile.ai[1] = 0.0f;
            }
            if (!projectile.friendly)
                foundValidTarget = false;
            if (foundValidTarget) {
                double projVelOrSomethingElse = (double)locAIzero;
                Vector2 projCenter = new Vector2((float)(projectile.position.X + (double)projectile.width * 0.5), (float)(projectile.position.Y + (double)projectile.height * 0.5));
                float distCenterX = projX - (float)projCenter.X;
                float distCenterY = projY - (float)projCenter.Y;
                double distCenter = Math.Sqrt((double)distCenterX * (double)distCenterX + (double)distCenterY * (double)distCenterY);
                float num11 = (float)(projVelOrSomethingElse / distCenter);
                float num12 = distCenterX * num11;
                float num13 = distCenterY * num11;
                int num14 = 8;
                projectile.velocity.X = (float)((projectile.velocity.X * (double)(num14 - 1) + (double)num12) / (double)num14);
                projectile.velocity.Y = (float)((projectile.velocity.Y * (double)(num14 - 1) + (double)num13) / (double)num14);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }

    }
}