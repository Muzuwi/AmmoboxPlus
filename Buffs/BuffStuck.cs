using Terraria;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Buffs
{
    public class Stuck : ABBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Stuck");
            // Description.SetDefault("You are stuck in place!");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<AmmoboxGlobalNPC>().apStuck = true;
        }
    }
}