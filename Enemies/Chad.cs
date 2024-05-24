using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DeuxExamMod.Items.Consumeable;  // Ensure the namespace matches where FeelGoodJuice is defined
using DeuxExamMod.Items.Misc;  // Ensure the namespace matches where CollegeNote is defined

namespace DeuxExamMod.Enemies
{
    public class Chad : ModNPC
    {
        private int jumpCooldown = 0; // Cooldown timer for jumping
        private const float FollowDistance = 200f; // Desired distance to maintain from the player

        public int item1Type = ModContent.ItemType<FeelGoodJuice>();  // Changed to ModContent.ItemType<T>()
        public int item2Type = ModContent.ItemType<CollegeNote>();    // Changed to ModContent.ItemType<T>()
        public int item1Amount = 1;
        public int item2Amount = 1;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chad");
            Main.npcFrameCount[NPC.type] = 1; // Set to 1 if the image is a single frame
        }

        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 40;
            NPC.damage = 30;
            NPC.defense = 15;
            NPC.lifeMax = 300;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = 100f;
            NPC.knockBackResist = 0.4f;
            NPC.aiStyle = 3; // Fighter AI style
            AIType = NPCID.GoblinPeon;
            NPC.noGravity = false; // Ensure gravity affects the NPC
            NPC.noTileCollide = false; // Ensure the NPC collides with tiles
        }

        public override void AI()
        {
            // Custom AI code for Chad with goblin behaviors
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];

            if (player == null || !player.active || player.dead)
            {
                NPC.velocity = Vector2.Zero;
                return;
            }

            Vector2 direction = player.Center - NPC.Center;
            float distance = direction.Length();
            direction.Normalize();

            if (distance > FollowDistance) // Follow player if beyond follow distance
            {
                NPC.velocity = new Vector2(direction.X * 5f, NPC.velocity.Y); // Maintain horizontal movement only
            }
            else // Slow down if within follow distance
            {
                NPC.velocity.X *= 0.9f; // Slow down horizontal movement to maintain distance
            }

            // Add custom jumping logic with cooldown
            if (jumpCooldown <= 0 && Main.rand.NextBool(200)) // Randomly perform a jump attack
            {
                if (NPC.velocity.Y == 0) // Only jump if on the ground
                {
                    NPC.velocity.Y = -10f; // Simulate jump
                    jumpCooldown = 100; // Reset cooldown (approximately 1.6 seconds)
                }
            }
            jumpCooldown--; // Decrease cooldown
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.dayTime && spawnInfo.Player.ZoneOverworldHeight && NPC.downedBoss3) // Only spawn during the day after Skeletron is defeated
            {
                return 0.2f; // Adjusted spawn chance for goblin behavior
            }
            return 0f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood);
                }
            }
        }

        public override void OnKill()
        {
            // Drop FeelGoodJuice (common)
            if (item1Type > 0 && item1Amount > 0 && Main.rand.NextFloat() < 0.75f) // 75% chance to drop
            {
                Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), item1Type, item1Amount);
            }

            // Drop CollegeNote (uncommon)
            if (item2Type > 0 && item2Amount > 0 && Main.rand.NextFloat() < 0.25f) // 25% chance to drop
            {
                Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), item2Type, item2Amount);
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("DeuxExamMod/Content/Images/Enemies/Chad").Value;
            spriteBatch.Draw(texture, NPC.Center - Main.screenPosition, null, drawColor, NPC.rotation, texture.Size() / 2, NPC.scale, SpriteEffects.None, 0f);
        }
    }
}
