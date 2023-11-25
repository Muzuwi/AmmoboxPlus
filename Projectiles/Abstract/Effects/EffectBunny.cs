using AmmoboxPlus.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles.Abstract.Effects
{
    public class EffectBunny : IEffect
    {
        public static EffectBunny Instance = new EffectBunny();

        public void Proc(ModProjectile cause, NPC targetNpc)
        {
            // This could probably be expanded..
            if (targetNpc.boss || targetNpc.type == NPCID.TargetDummy)
            {
                return;
            }

            if (!WorldGen.genRand.NextBool(AmmoboxConfig.BunnyEffectChance1inX))
            {
                return;
            }

            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(targetNpc.position, 16, 16, DustID.Confetti);
            }

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Vector2 pos = targetNpc.position;
                targetNpc.active = false;
                NPC.NewNPC(cause.Projectile.GetSource_FromThis(), (int)pos.X, (int)pos.Y, NPCID.Bunny);
                SoundEngine.PlaySound(SoundID.DoubleJump, pos);
            }
            else
            {
                var packet = cause.Mod.GetPacket();
                packet.Write((byte)AmmoboxMsgType.AmmoboxBunny);
                packet.Write(targetNpc.whoAmI);
                packet.Send();
            }
        }
    }
}
