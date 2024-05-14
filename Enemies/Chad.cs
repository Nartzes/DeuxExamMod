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