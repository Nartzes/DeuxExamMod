////Eboookis v4 no drop just movement ai and image load in atm 
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using Microsoft.Xna.Framework; // Required for Vector2

//namespace DeuxExamMod.Enemies
//{
//    public class Eboookis : ModNPC
//    {
//        private float gravity = 0.35f; // Gravity strength for movement
//        private float maxFallSpeed = 10f; // Maximum falling speed
//        private float jumpSpeed = 7f; // Speed at which the NPC jumps
//        private int frameCounter = 0; // Counter to handle frame changes
//        private int frame = 0; // Current frame

//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("Eboookis");
//            Main.npcFrameCount[NPC.type] = 6; // Number of frames in your animation sprite
//        }

//        public override void SetDefaults()
//        {
//            NPC.width = 36;
//            NPC.height = 36;
//            NPC.damage = 45;
//            NPC.defense = 20;
//            NPC.lifeMax = 400;
//            NPC.HitSound = SoundID.NPCHit4;
//            NPC.DeathSound = SoundID.NPCDeath6;
//            NPC.value = 100f;
//            NPC.knockBackResist = 0.8f;
//            NPC.aiStyle = -1; // Use custom AI
//            NPC.noGravity = true; // NPC controls its own gravity
//            NPC.noTileCollide = false; // NPC collides with tiles
//        }

//        public override void AI()
//        {
//            // Find the closest player
//            Player targetPlayer = null;
//            float closestDistance = float.MaxValue;
//            foreach (Player player in Main.player)
//            {
//                float distance = Vector2.Distance(player.Center, NPC.Center);
//                if (distance < closestDistance && player.active && !player.dead)
//                {
//                    closestDistance = distance;
//                    targetPlayer = player;
//                }
//            }

//            // Follow the player continuously
//            if (targetPlayer != null)
//            {
//                Vector2 direction = targetPlayer.Center - NPC.Center;
//                direction.Normalize();
//                float speed = 2f; // Adjust movement speed
//                NPC.velocity.X = direction.X * speed;

//                // Apply gravity manually
//                NPC.velocity.Y += gravity;

//                // Jump if close to the player
//                if (closestDistance < 200) // NPC will jump when the player is within 200 pixels
//                {
//                    NPC.velocity.Y = -jumpSpeed;
//                }

//                // Limit falling speed
//                if (NPC.velocity.Y > maxFallSpeed)
//                {
//                    NPC.velocity.Y = maxFallSpeed;
//                }

//                // Ensure the state is updated across clients
//                NPC.netUpdate = true;
//            }

//            // Handle animation
//            HandleAnimation();
//        }

//        private void HandleAnimation()
//        {
//            frameCounter++;
//            if (frameCounter > 5) // Change '5' to adjust the speed of the animation
//            {
//                frameCounter = 0;
//                frame++;
//                if (frame >= Main.npcFrameCount[NPC.type])
//                    frame = 0;
//            }
//        }

//        public override void FindFrame(int frameHeight)
//        {
//            NPC.frame.Y = frame * frameHeight; // Ensure this matches the height of each frame in your sprite sheet
//        }
//    }
//}










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
            Main.npcFrameCount[NPC.type] = 24; // Assuming 8 frames for the NPC's animation
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
