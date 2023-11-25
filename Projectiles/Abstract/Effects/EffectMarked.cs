using AmmoboxPlus.Globals;
using AmmoboxPlus.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles.Abstract.Effects
{
    public class EffectMarked : IEffect
    {
        public static EffectMarked Instance = new EffectMarked();

        public void Proc(ModProjectile cause, NPC targetNpc)
        {
            targetNpc.GetGlobalNPC<AmmoboxGlobalNPC>().apMarked = true;

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                targetNpc.AddBuff(ModContent.BuffType<Buffs.Marked>(), 100);
            }
            else
            {
                var packet = cause.Mod.GetPacket();
                int buffType = ModContent.BuffType<Buffs.Marked>();
                packet.Write((byte)AmmoboxMsgType.AmmoboxClouded);
                packet.Write(targetNpc.whoAmI);
                packet.Write(buffType);
                packet.Write(100);
                packet.Send();
            }
        }
    }
}
