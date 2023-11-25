using AmmoboxPlus.Globals;
using AmmoboxPlus.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles.Abstract.Effects
{
    public class EffectSlime : IEffect
    {
        public static EffectSlime Instance = new EffectSlime();

        public void Proc(ModProjectile cause, NPC targetNpc)
        {
            if (AmmoboxPlus.isEnemyBlacklisted(targetNpc.type))
            {
                return;
            }
            targetNpc.GetGlobalNPC<AmmoboxGlobalNPC>().apSlime = true;

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                targetNpc.AddBuff(ModContent.BuffType<Buffs.Slime>(), 200);
                targetNpc.AddBuff(BuffID.Slimed, 200);
            }
            else
            {
                targetNpc.AddBuff(BuffID.Slimed, 200);
                var packet = cause.Mod.GetPacket();
                int buffType = ModContent.BuffType<Buffs.Slime>();
                packet.Write((byte)AmmoboxMsgType.AmmoboxSlime);
                packet.Write(targetNpc.whoAmI);
                packet.Write(buffType);
                packet.Write(200);
                packet.Send();
            }
        }
    }
}
