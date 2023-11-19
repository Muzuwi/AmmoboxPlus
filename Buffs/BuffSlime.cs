using Terraria;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Buffs
{
    public class Slime : ABBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Slime");
            // Description.SetDefault("Your movements are limited.");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<AmmoboxGlobalNPC>().apSlime = true;
            npc.netUpdate = true;
        }
    }
}