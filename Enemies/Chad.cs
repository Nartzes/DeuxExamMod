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


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chad");
            Main.npcFrameCount[NPC.type] = 1; // Adjust frame count if needed
        }

        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.damage = 25;
            NPC.defense = 20;
            NPC.lifeMax = 600;
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
            int itemType = ModContent.ItemType<CollegeNote>(); // Ensure you have the right namespace and class name here
            if (Main.rand.NextFloat() < 0.30f) // 30% chance to drop
            {
                Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), itemType);
            }
            // Drop Bookmark (uncommon)
            itemType = ModContent.ItemType<FeelGoodJuice>(); // Ensure you have the right namespace and class name here
            if (Main.rand.NextFloat() < 0.1f) // 10% chance to drop
            {
                Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), itemType);
            }

        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.dayTime && NPC.downedBoss3 && spawnInfo.Player.ZoneOverworldHeight)
            {
                return 1f; // 10% chance to spawn
            }

            return 0f;
        }
    }
}
