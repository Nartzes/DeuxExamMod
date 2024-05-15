using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DeuxExamMod.Boss
{
    public class XamLordLeftHand : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("XamLord Left Hand");
        }

        public override void SetDefaults()
        {
            // Clone the defaults from the Skeleton NPC
            NPC.CloneDefaults(NPCID.Skeleton);

            // Modify the cloned defaults as needed
            NPC.width = 18;
            NPC.height = 18;
            NPC.damage = 10;
            NPC.defense = 5;
            NPC.lifeMax = 50;
            NPC.value = 60f;
            NPC.knockBackResist = 0.5f;
        }

        public override void AI()
        {
            // Additional AI code if needed
        }
    }
}