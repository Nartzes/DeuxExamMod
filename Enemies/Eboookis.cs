//Eboookis v4 no drop just movement ai and image load in atm 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework; // Required for Vector2

namespace DeuxExamMod.Enemies
{
    public class Eboookis : ModNPC
    {
        private float gravity = 0.35f; // Gravity strength for movement
        private float maxFallSpeed = 10f; // Maximum falling speed
        private float jumpSpeed = 7f; // Speed at which the NPC jumps
        private int frameCounter = 0; // Counter to handle frame changes
        private int frame = 0; // Current frame

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eboookis");
            Main.npcFrameCount[NPC.type] = 4; // Number of frames in your animation sprite
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
            NPC.knockBackResist = 0.8f;
            NPC.aiStyle = -1; // Use custom AI
            NPC.noGravity = true; // NPC controls its own gravity
            NPC.noTileCollide = false; // NPC collides with tiles
        }

        public override void AI()
        {
            // Find the closest player
            Player targetPlayer = null;
            float closestDistance = float.MaxValue;
            foreach (Player player in Main.player)
            {
                float distance = Vector2.Distance(player.Center, NPC.Center);
                if (distance < closestDistance && player.active && !player.dead)
                {
                    closestDistance = distance;
                    targetPlayer = player;
                }
            }

            // Follow the player continuously
            if (targetPlayer != null)
            {
                Vector2 direction = targetPlayer.Center - NPC.Center;
                direction.Normalize();
                float speed = 2f; // Adjust movement speed
                NPC.velocity.X = direction.X * speed;

                // Apply gravity manually
                NPC.velocity.Y += gravity;

                // Jump if close to the player
                if (closestDistance < 200) // NPC will jump when the player is within 200 pixels
                {
                    NPC.velocity.Y = -jumpSpeed;
                }

                // Limit falling speed
                if (NPC.velocity.Y > maxFallSpeed)
                {
                    NPC.velocity.Y = maxFallSpeed;
                }

                // Ensure the state is updated across clients
                NPC.netUpdate = true;
            }

            // Handle animation
            HandleAnimation();
        }

        private void HandleAnimation()
        {
            frameCounter++;
            if (frameCounter > 5) // Change '5' to adjust the speed of the animation
            {
                frameCounter = 0;
                frame++;
                if (frame >= Main.npcFrameCount[NPC.type])
                    frame = 0;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = frame * frameHeight; // Ensure this matches the height of each frame in your sprite sheet
        }
    }
}





//v2 ebbokis co


//    using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using Microsoft.Xna.Framework; // Required for Vector2

//namespace DeuxExamMod.Enemies
//{
//    public class Eboookis : ModNPC
//    {
//        private float followDistance = 50f; // Desired distance above the ground

//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("Eboookis");
//            Main.npcFrameCount[NPC.type] = 4; // Number of frames in your animation sprite
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

//            if (targetPlayer != null)
//            {
//                // Compute direction to the player, ignoring vertical component for now
//                Vector2 direction = targetPlayer.Center - NPC.Center;
//                direction.Y = 0; // Ignore vertical movement component
//                direction.Normalize();
//                float speed = 2f; // Adjust movement speed
//                NPC.velocity.X = direction.X * speed;

//                // Adjust vertical position to fly close to the ground
//                AdjustVerticalPosition();
//            }

//            // Ensure the state is updated across clients
//            NPC.netUpdate = true;

//            // Handle animation
//            HandleAnimation();
//        }

//        private void AdjustVerticalPosition()
//        {
//            // Cast a ray downwards to check the distance to the ground
//            Vector2 groundCheck = NPC.position;
//            groundCheck.Y += NPC.height + followDistance; // Offset by height and follow distance
//            while (!WorldGen.SolidTile(Framing.GetTileSafely(groundCheck.ToTileCoordinates()).X, Framing.GetTileSafely(groundCheck.ToTileCoordinates()).Y))
//            {
//                groundCheck.Y += 16; // Increment to move down by tile height
//                if (groundCheck.Y > Main.maxTilesY * 16) // Check to not go below the map
//                    break;
//            }
//            NPC.position.Y = groundCheck.Y - NPC.height - followDistance;
//        }

//        private void HandleAnimation()
//        {
//            int frameSpeed = 5; // Frame speed
//            NPC.frameCounter++;
//            if (NPC.frameCounter >= frameSpeed)
//            {
//                NPC.frameCounter = 0;
//                NPC.frame.Y += NPC.frame.Height;
//                if (NPC.frame.Y >= NPC.frame.Height * Main.npcFrameCount[NPC.type])
//                    NPC.frame.Y = 0;
//            }
//        }
//    }
//}
