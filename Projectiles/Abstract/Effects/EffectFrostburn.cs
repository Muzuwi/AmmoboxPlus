using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles.Abstract.Effects
{
    public class EffectFrostburn : IEffect
    {
        public static EffectFrostburn Instance = new EffectFrostburn();

        public void Proc(ModProjectile cause, NPC targetNpc)
        {
            if (!Main.rand.NextBool(2))
            {
                return;
            }

            targetNpc.AddBuff(BuffID.Frostburn, 240);
        }
    }
}
