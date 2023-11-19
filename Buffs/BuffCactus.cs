using Terraria;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Buffs
{
    public class Cactus : ABBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Spiky Shield");
            // Description.SetDefault("Enemies nearby are hurt");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<AmmoboxGlobalNPC>().apCactus = true;
            //npc.GetGlobalNPC<AmmoboxGlobalNPC>().apCactusDamage = npc.damage;
        }
    }
}