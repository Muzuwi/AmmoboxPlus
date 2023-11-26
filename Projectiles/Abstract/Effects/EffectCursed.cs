using AmmoboxPlus.Globals;
using AmmoboxPlus.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles.Abstract.Effects
{
    public class EffectCursed : IEffect
    {
        public static EffectCursed Instance = new EffectCursed();

        public void Proc(ModProjectile cause, NPC targetNpc)
        {
            if (!Main.rand.NextBool(2))
            {
                return;
            }
            targetNpc.AddBuff(BuffID.CursedInferno, 240);
        }
    }
}
