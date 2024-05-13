using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;

namespace DeuxExamMod.Enemies
{
    public class Eboookis : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eboookis");
            Main.npcFrameCount[NPC.type] = 5; // Assuming 8 frames for the NPC's animation
        }

        public override void SetDefaults()
        {
            NPC.width = 36;
            NPC.height = 36;
            NPC.damage = 45;
            NPC.defense = 20;
            NPC.lifeMax = 400;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = 100f;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
        }

        public override void AI()
        {
            NPC.TargetClosest(true);
            Player target = Main.player[NPC.target];
            if (!target.dead && target.active)
            {
                HoverAroundPlayer(target);
            }
            HandleAnimation();
        }

        private void HoverAroundPlayer(Player player)
        {
            float desiredDistance = 200f; // Desired hover distance from the player
            Vector2 moveDirection = player.Center - NPC.Center;
            float currentDistance = moveDirection.Length();

            // Normalize direction vector only if we're not too close
            if (currentDistance > 50f) // Avoid getting too close to the player
            {
                moveDirection.Normalize();
                float distanceDifference = currentDistance - desiredDistance;

                // Move towards or away from the player based on the distance difference
                NPC.velocity = (NPC.velocity * 0.9f) + moveDirection * distanceDifference * 0.1f;
            }
            else
            {
                // If too close, move directly away quickly
                moveDirection.Normalize();
                NPC.velocity = -moveDirection * 4f;
            }
        }

        private void HandleAnimation()
        {
            NPC.frameCounter++;
            if (NPC.frameCounter > 5)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = (NPC.frame.Y + 1) % (Main.npcFrameCount[NPC.type] * NPC.frame.Height);
            }
        }

        public override void OnKill()
        {
            // Always drop Bookmark
            int itemType = ModContent.ItemType<Items.Weapon.Melee.BookMark>(); // Ensure you have the right namespace and class name here
            Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), itemType);

            // Always drop CollegeNote
            itemType = ModContent.ItemType<Items.Misc.CollegeNote>(); // Ensure you have the right namespace and class name here
            Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), itemType);
        }
    }
}