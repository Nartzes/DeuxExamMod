
//Chad V1 base version no Drops Or Jumps Added 


//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using Microsoft.Xna.Framework;

//namespace DeuxExamMod.Enemies
//{
//    public class Chad : ModNPC
//    {
//        private Player targetPlayer;
//        private float gravity = 0.35f; // Adjust gravity strength
//        private float maxFallSpeed = 10f;

//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("Chad");
//            Main.npcFrameCount[NPC.type] = 1; // Adjust frame count if needed
//        }

//        public override void SetDefaults()
//        {
//            NPC.width = 100;  // Set appropriate width for the paladin size
//            NPC.height = 100; // Set appropriate height for the paladin size
//            NPC.damage = 30; // Adjust as necessary
//            NPC.defense = 10; // Adjust as necessary
//            NPC.lifeMax = 200; // Adjust as necessary
//            NPC.HitSound = SoundID.NPCHit1;
//            NPC.DeathSound = SoundID.NPCDeath1;
//            NPC.value = 60f; // Adjust as necessary
//            NPC.knockBackResist = 0.5f; // Adjust as necessary
//            NPC.aiStyle = -1; // Custom AI
//            NPC.noGravity = true; // We'll control gravity ourselves
//            NPC.noTileCollide = false; // Collide with tiles
//        }

//        public override void AI()
//        {
//            Find the closest player
//            float closestDistance = float.MaxValue;
//            foreach (Player player in Main.player)
//            {
//                float distance = Vector2.Distance(player.Center, NPC.Center);
//                if (distance < closestDistance && !player.dead && player.active)
//                {
//                    closestDistance = distance;
//                    targetPlayer = player;
//                }
//            }

//            Follow the player and stay on the ground
//            if (targetPlayer != null)
//            {
//                Vector2 direction = targetPlayer.Center - NPC.Center;
//                direction.Normalize();

//                float speed = 2f; // Adjust movement speed
//                NPC.velocity.X = direction.X * speed;

//                Apply gravity and maintain ground contact
//                NPC.velocity.Y += gravity;
//                if (NPC.velocity.Y > maxFallSpeed)
//                {
//                    NPC.velocity.Y = maxFallSpeed;
//                }

//                NPC.netUpdate = true;
//            }
//        }
//    }
//}



//Chad V2 jumps added alongside drops 
using DeuxExamMod.Enemies
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DeuxExamMod.Items; // Assuming CollegeNote is correctly defined in this namespace

namespace DeuxExamMod.Enemies.Misc;

public class Chad : ModNPC
{
    private Player targetPlayer;
    private float gravity = 0.35f; // Gravity strength
    private float maxFallSpeed = 10f; // Maximum falling speed
    private float jumpSpeed = 8f; // Jumping speed

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Chad");
        Main.npcFrameCount[NPC.type] = 1; // Adjust frame count if needed
    }

    public override void SetDefaults()
    {
        NPC.width = 100; // Set appropriate width for the NPC size
        NPC.height = 100; // Set appropriate height for the NPC size
        NPC.damage = 30; // Adjust damage as necessary
        NPC.defense = 10; // Adjust defense as necessary
        NPC.lifeMax = 200; // Set maximum life
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.value = 60f; // Set value
        NPC.knockBackResist = 0.5f; // Set knockback resistance
        NPC.aiStyle = -1; // Custom AI style
        NPC.noGravity = true; // NPC will control its own gravity
        NPC.noTileCollide = false; // Should collide with tiles
    }

    public override void AI()
    {
        // Find the closest player
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

        // Follow the player
        if (targetPlayer != null)
        {
            Vector2 direction = targetPlayer.Center - NPC.Center;
            direction.Normalize(); // Normalize the vector to get direction only
            float speed = 2f; // Movement speed
            NPC.velocity.X = direction.X * speed;
            NPC.velocity.Y += gravity; // Apply gravity

            // Check if NPC should jump
            if (closestDistance < 100) // NPC jumps if very close to the player
            {
                NPC.velocity.Y = -jumpSpeed; // Negative velocity for jumping upwards
            }

            if (NPC.velocity.Y > maxFallSpeed)
            {
                NPC.velocity.Y = maxFallSpeed; // Limit falling speed
            }

            NPC.netUpdate = true; // Sync the NPC state across clients in multiplayer
        }
    }

    public override void OnKill()
    {
        // Drop CollegeNote only when NPC is killed
        Item.NewItem(NPC.getRect(), Mod.Find<ModProjectile>("ColledgeNote").Type, 1); // 100% chance to drop CollegeNote
      
    }
}

