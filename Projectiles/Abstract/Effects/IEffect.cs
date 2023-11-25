
using Terraria;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles.Abstract
{
    public interface IEffect
    {
        void Proc(ModProjectile cause, NPC target);
    }
}
