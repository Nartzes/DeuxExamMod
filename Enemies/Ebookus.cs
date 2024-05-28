using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DeuxExamMod.Items.Misc;  // Ensure the namespace matches where CollegeNote is defined
using DeuxExamMod.Items.Weapon.Melee;  // Ensure the namespace matches where BookMark is defined
using DeuxExamMod.Items.Consumeable;  // Ensure the namespace matches where FeelGoodJuice is defined
using DeuxExamMod.Projectiles;  // Ensure this matches the namespace for EbookusProj

namespace DeuxExamMod.Enemies
{
    public class Ebookus : ModNPC
    {
        private int shootingCooldown = 60; // Cooldown for shooting
        private int changeDirectionCooldown = 20; // Cooldown for changing direction
        private int jumpCooldown = 0; // Cooldown timer for jumping
        private const float FollowDistance = 200f; // Desired distance to maintain from the player

        public int item1Type = ModContent.ItemType<FeelGoodJuice>();
        public int item2Type = ModContent.ItemType<CollegeNote>();
        public int item1Amount = 1;
        public int item2Amount = 1;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ebookus");
            Main.npcFrameCount[NPC.type] = 8; // Set to 8 if the image has 8 frames
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
            NPC.noGravity = false;
            NPC.noTileCollide = false;
        }

        public override void AI()
        {
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            if (!player.dead && player.active)
            {
                if (NPC.localAI[0] == 0f)
                {
                    // Spawn 5 spaces above the player
                    NPC.position.Y = player.position.Y - 5 * 16;
                    NPC.localAI[0] = 1f; // Ensure this is only done once
                }
                FollowPlayerWithCollision(player);
                ShootAtPlayer(player);
                CustomJumpLogic(player);
            }
            HandleAnimation();
        }

        private void FollowPlayerWithCollision(Player player)
        {
            Vector2 moveDirection = player.Center - NPC.Center;

            // Change direction randomly to mimic erratic behavior
            changeDirectionCooldown--;
            if (changeDirectionCooldown <= 0)
            {
                moveDirection = moveDirection.RotatedByRandom(MathHelper.ToRadians(45)); // Randomize direction within 45 degrees
                changeDirectionCooldown = Main.rand.Next(15, 30); // Random cooldown between changes
            }

            // Normalize direction vector and apply velocity
            moveDirection.Normalize();
            NPC.velocity = (NPC.velocity * 0.9f) + moveDirection * 0.5f;

            // Handle collision with tiles
            TileCollision();
        }

        private void TileCollision()
        {
            Vector2 newPosition = NPC.position;
            Vector2 velocity = NPC.velocity;

            int width = NPC.width;
            int height = NPC.height;

            // Check collision with tiles and adjust position accordingly
            bool collisionX = WorldGen.SolidTile((int)(newPosition.X / 16), (int)(newPosition.Y / 16));
            bool collisionY = WorldGen.SolidTile((int)((newPosition.X + width) / 16), (int)(newPosition.Y / 16));

            if (collisionX)
            {
                newPosition.X -= velocity.X;
                velocity.X = 0;
            }

            if (collisionY)
            {
                newPosition.Y -= velocity.Y;
                velocity.Y = 0;
            }

            NPC.position = newPosition;
            NPC.velocity = velocity;
        }

        private void ShootAtPlayer(Player player)
        {
            shootingCooldown--;
            if (shootingCooldown <= 0)
            {
                Vector2 shootDirection = player.Center - NPC.Center;
                shootDirection.Normalize();
                shootDirection *= 10f; // Speed of the projectile

                int projectileType = ModContent.ProjectileType<EbookusProj>(); // Correctly reference the projectile type
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, shootDirection, projectileType, NPC.damage / 2, 0, Main.myPlayer);

                shootingCooldown = 60; // Reset cooldown
            }
        }

        private void CustomJumpLogic(Player player)
        {
            // Custom AI code for Ebookus with goblin behaviors
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

        private void HandleAnimation()
        {
            NPC.frameCounter++;
            if (NPC.frameCounter > 8f) // Adjust the speed of the animation by changing this value
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = (NPC.frame.Y + NPC.frame.Height) % (Main.npcFrameCount[NPC.type] * NPC.frame.Height);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool isNightTime = !Main.dayTime;
            bool hasDefeatedSkeletron = NPC.downedBoss3;
            bool playerNearSurface = spawnInfo.Player.ZoneOverworldHeight;
            bool notUnderground = spawnInfo.SpawnTileY < Main.worldSurface;

            if (isNightTime && hasDefeatedSkeletron && playerNearSurface && notUnderground)
            {
                return 1f; // 100% chance to spawn at night after Skeletron is defeated and above ground
            }

            if (Main.dayTime && spawnInfo.Player.ZoneOverworldHeight) // Only spawn during the day
            {
                return 0.2f; // Adjusted spawn chance for goblin behavior
            }

            return 0f;
        }

        public override void OnKill()
        {
            // Drop CollegeNote (common)
            if (Main.rand.NextFloat() < 0.75f) // 75% chance to drop
            {
                Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), item1Type, item1Amount);
            }

            // Drop Bookmark (uncommon)
            if (Main.rand.NextFloat() < 0.25f) // 25% chance to drop
            {
                Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), ModContent.ItemType<BookMark>());
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("DeuxExamMod/Content/Images/Enemies/Ebookus").Value;
            spriteBatch.Draw(texture, NPC.Center - Main.screenPosition, null, drawColor, NPC.rotation, texture.Size() / 2, NPC.scale, SpriteEffects.None, 0f);
        }
    }
}
