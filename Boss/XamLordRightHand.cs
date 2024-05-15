using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework; // Add this line

namespace DeuxExamMod.Boss
{
    public class XamLordRightHand : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("XamLord Right Hand");
            Main.npcFrameCount[NPC.type] = 1; // Set to 1 if you have only one frame
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.SkeletronHand);
            NPC.width = 56; // Set to the width of your image
            NPC.height = 84; // Set to the height of your image
            NPC.damage = 10;
            NPC.defense = 5;
            NPC.lifeMax = 50;
            NPC.value = 60f;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = -1; // Disable default AI style
            NPC.noTileCollide = true; // Skeletron Hands usually don't collide with tiles
            NPC.noGravity = true; // Skeletron Hands are not affected by gravity
        }

        public override void AI()
        {
            // Implement Skeletron-like AI behavior here
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];

            if (!player.active || player.dead)
            {
                NPC.TargetClosest(false);
                NPC.velocity = new Vector2(0f, -10f);
                return;
            }

            Vector2 targetPosition = player.Center + new Vector2(-200f, -200f); // Adjust for positioning
            float speed = 10f;
            Vector2 moveTo = targetPosition - NPC.Center;
            float magnitude = (float)System.Math.Sqrt(moveTo.X * moveTo.X + moveTo.Y * moveTo.Y);
            if (magnitude > speed)
            {
                moveTo *= speed / magnitude;
            }
            NPC.velocity = moveTo;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = 0; // Set to fixed frame, or change according to your custom animation needs
        }
    }
}