//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using Microsoft.Xna.Framework;
//using DeuxExamMod.Items.Consumeable;  // Ensure the namespace matches where FeelGoodJuice is defined
//using DeuxExamMod.Items.Misc;  // Ensure the namespace matches where CollegeNote is defined

//namespace DeuxExamMod.Enemies
//{
//    public class Chad : ModNPC
//    {
//        private Player targetPlayer;
//        private float gravity = 0.35f; // Adjust gravity strength
//        private float maxFallSpeed = 10f;
//        private float jumpSpeed = 7f; // Jumping speed

//        // Configurable item drops
//        public int item1Type = ModContent.ItemType<FeelGoodJuice>();  // Changed to ModContent.ItemType<T>()
//        public int item2Type = ModContent.ItemType<CollegeNote>();    // Changed to ModContent.ItemType<T>()
//        public int item1Amount = 1;
//        public int item2Amount = 1;

//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("Chad");
//            Main.npcFrameCount[NPC.type] = 1; // Adjust frame count if needed
//        }

//        public override void SetDefaults()
//        {
//            NPC.width = 100;  // Set appropriate width for the NPC size
//            NPC.height = 100; // Set appropriate height for the NPC size
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
//            // Find the closest player
//            float closestDistance = float.MaxValue;
//            foreach (Player player in Main.player)
//            {
//                float distance = Vector2.Distance(player.Center, NPC.Center);
//                if (distance < closestDistance && player.active && !player.dead) // Corrected logical negation
//                {
//                    closestDistance = distance;
//                    targetPlayer = player;
//                }
//            }

//            // Follow the player and maintain ground contact
//            if (targetPlayer != null)
//            {
//                Vector2 direction = targetPlayer.Center - NPC.Center;
//                direction.Normalize();

//                float speed = 2f; // Adjust movement speed
//                NPC.velocity.X = direction.X * speed;

//                // Apply gravity
//                NPC.velocity.Y += gravity;

//                // Implement jumping mechanism
//                if (closestDistance < 200) // NPC will jump when the player is within 200 pixels
//                {
//                    NPC.velocity.Y = -jumpSpeed; // Negative Y velocity for jumping
//                }

//                // Limit falling speed
//                if (NPC.velocity.Y > maxFallSpeed)
//                {
//                    NPC.velocity.Y = maxFallSpeed;
//                }

//                NPC.netUpdate = true; // Ensure the state is updated across clients
//            }
//        }

//        public override void OnKill()
//        {
//            // Manually spawn FeelGoodJuice
//            if (item1Type > 0 && item1Amount > 0)
//            {
//                for (int i = 0; i < item1Amount; i++)
//                {
//                    var item = new Item();
//                    item.SetDefaults(ModContent.ItemType<FeelGoodJuice>());
//                    item.position = NPC.Center; // Set the item's position to the NPC's center
//                    item.velocity = new Vector2(Main.rand.Next(-20, 21), Main.rand.Next(-20, 1)); // Randomize the drop direction
//                    Item.NewItem(NPC.GetSource_Loot(), item.position, item.Size, item.type, 1); // Drop the item into the world
//                }
//            }

//            // Manually spawn CollegeNote
//            if (item2Type > 0 && item2Amount > 0)
//            {
//                for (int i = 0; i < item2Amount; i++)
//                {
//                    var item = new Item();
//                    item.SetDefaults(ModContent.ItemType<CollegeNote>());
//                    item.position = NPC.Center; // Set the item's position to the NPC's center
//                    item.velocity = new Vector2(Main.rand.Next(-20, 21), Main.rand.Next(-20, 1)); // Randomize the drop direction
//                    Item.NewItem(NPC.GetSource_Loot(), item.position, item.Size, item.type, 1); // Drop the item into the world
//                }
//            }
////        }
//    }
//}







using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DeuxExamMod.Items.Consumeable;  // Ensure the namespace matches where FeelGoodJuice is defined
using DeuxExamMod.Items.Misc;  // Ensure the namespace matches where CollegeNote is defined

namespace DeuxExamMod.Enemies
{
    public class Chad : ModNPC
    {
        private Player targetPlayer;
        private int jumpCooldown = 0; // Cooldown timer for jumping

        public int item1Type = ModContent.ItemType<FeelGoodJuice>();  // Changed to ModContent.ItemType<T>()
        public int item2Type = ModContent.ItemType<CollegeNote>();    // Changed to ModContent.ItemType<T>()
        public int item1Amount = 1;
        public int item2Amount = 1;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chad");
            Main.npcFrameCount[NPC.type] = 1; // Adjust frame count if needed
        }

        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.damage = 30;
            NPC.defense = 10;
            NPC.lifeMax = 200;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 60f;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 3; // Zombie-like behavior
            NPC.noGravity = false; // Zombies use gravity
            NPC.noTileCollide = false; // Zombies collide with tiles
        }

        public override void AI()
        {
            base.AI(); // Use the base zombie AI

            // Add custom jumping logic with cooldown
            if (jumpCooldown <= 0 && Main.rand.NextBool(600)) // 1/600 chance per tick to jump
            {
                NPC.velocity.Y = -7f; // Simulate jump
                jumpCooldown = 100; // Reset cooldown (approximately 1.6 seconds)
            }
            jumpCooldown--; // Decrease cooldown
        }

        public override void OnKill()
        {
            // Manually spawn FeelGoodJuice
            if (item1Type > 0 && item1Amount > 0)
            {
                Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), item1Type, item1Amount);
            }

            // Manually spawn CollegeNote
            if (item2Type > 0 && item2Amount > 0)
            {
                Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), item2Type, item2Amount);
            }
        }
    }
}


