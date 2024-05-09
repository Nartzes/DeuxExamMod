using DeuxExamMod.Buff;
using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace DeuxExamMod.Projectiles
{
    public class GhostDogProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 8; // This adjust how much of the verticalness the frames are and it is the main reason why you see more than one fram at once.
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Puppy); // Copy the stats of the Zephyr Fish
            AIType = ProjectileID.Puppy; // Mimic as the Zephyr Fish during AI.

        }


        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];

            player.puppy = false; // Relic from AIType

            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // Keep the projectile from disappearing as long as the player isn't dead and has the pet buff.
            if (!player.dead && player.HasBuff(ModContent.BuffType<GhostDogBuff>()))
            {
                Projectile.timeLeft = 2;
            }
        }

        public void AIGeneral(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
        {
            Vector2 idlePosition = owner.Center;
            idlePosition.Y -= 48f;

            float minionPositionOffset = (10 + Projectile.minionPos * 40) * -owner.direction;
            idlePosition.X += minionPositionOffset;

            vectorToIdlePosition = idlePosition - Projectile.Center;
            distanceToIdlePosition = vectorToIdlePosition.Length();

            if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f)
            {
                Projectile.position = idlePosition;
                Projectile.velocity *= 0.1f;
                Projectile.netUpdate = true;
            }

            float overlapVelocity = 0.04f;

            //for (int i = 0; i < Main.maxProjectiles; i++)
            //{
            //    //Projectile other = Main.Projectile[i];
            //    //if (
            //    //    i != Projectile.whoAmI &&
            //    //    other.owner == Projectile.owner &&
            //    //    Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width
            //    //)
            //    //{
            //        if (Projectile.position.X < other.position.X)
            //        {
            //            Projectile.velocity.X -= overlapVelocity;
            //        }
            //        else
            //        {
            //            Projectile.velocity.X += overlapVelocity;
            //        }

            //        if (Projectile.position.Y < other.position.Y)
            //        {
            //            Projectile.velocity.Y -= overlapVelocity;
            //        }
            //        else
            //        {
            //            Projectile.velocity.Y += overlapVelocity;
            //        }
            //}
        }

        private void AIMovement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
        {
            float speed = 8f;
            float inertia = 20f;

            if (foundTarget)
            {
                if (distanceFromTarget > 40f)
                {
                    Vector2 direction = targetCenter - Projectile.Center;
                    direction.Normalize();
                    direction *= speed;

                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
                }
                return;
            }

            if (distanceToIdlePosition > 600f)
            {
                speed = 12f;
                inertia = 60f;
            }
            else
            {
                speed = 4f;
                inertia = 80f;
            }

            if (distanceToIdlePosition > 20f)
            {
                vectorToIdlePosition.Normalize();
                vectorToIdlePosition *= speed;

                Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
            }
            else if (Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity.X = -0.15f;
                Projectile.velocity.Y = -0.05f;
            }
        }

        private void AIUpdateAnimation()
        {
            Projectile.rotation = Projectile.velocity.X * 0.05f;

            int frameSpeed = 5;
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }

        }
    }
}

