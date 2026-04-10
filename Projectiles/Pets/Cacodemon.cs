using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class Cacodemon : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Cacodemon");
		Main.projFrames[((ModProjectile)this).projectile.type] = 4;
		Main.projPet[((ModProjectile)this).projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 34;
		((ModProjectile)this).projectile.height = 34;
		((ModProjectile)this).projectile.netImportant = true;
		((ModProjectile)this).projectile.friendly = true;
		Main.projPet[((ModProjectile)this).projectile.type] = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.timeLeft = 999999999;
		((ModProjectile)this).projectile.timeLeft *= 999999999;
		((ModProjectile)this).projectile.penetrate = -1;
	}

	public override void AI()
	{
		Player player = Main.player[((ModProjectile)this).projectile.owner];
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.dead)
		{
			modPlayer.Cacodemon = false;
		}
		if (modPlayer.Cacodemon)
		{
			((ModProjectile)this).projectile.timeLeft = 2;
		}
		if (++((ModProjectile)this).projectile.frameCounter >= 60)
		{
			((ModProjectile)this).projectile.frameCounter = 0;
			if (++((ModProjectile)this).projectile.frame >= 4)
			{
				((ModProjectile)this).projectile.frame = 0;
			}
		}
		if (!Collision.CanHitLine(((ModProjectile)this).projectile.Center, 1, 1, player.Center, 1, 1))
		{
			((ModProjectile)this).projectile.ai[0] = 1f;
		}
		float num = 8f;
		if (((ModProjectile)this).projectile.ai[0] == 1f)
		{
			num = 17f;
		}
		Vector2 center = ((ModProjectile)this).projectile.Center;
		Vector2 vector = player.Center - center;
		((ModProjectile)this).projectile.ai[1] = 3600f;
		((ModProjectile)this).projectile.netUpdate = true;
		int num2 = 1;
		for (int i = 0; i < ((ModProjectile)this).projectile.whoAmI; i++)
		{
			if (((Entity)Main.projectile[i]).active && Main.projectile[i].owner == ((ModProjectile)this).projectile.owner && Main.projectile[i].type == ((ModProjectile)this).projectile.type)
			{
				num2++;
			}
		}
		vector.X -= (10 + num2 * 40) * player.direction;
		vector.Y -= 70f;
		float num3 = vector.Length();
		if (num3 > 200f && num < 9f)
		{
			num = 9f;
		}
		if (num3 < 100f && ((ModProjectile)this).projectile.ai[0] == 1f && !Collision.SolidCollision(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height))
		{
			((ModProjectile)this).projectile.ai[0] = 0f;
			((ModProjectile)this).projectile.netUpdate = true;
		}
		if (num3 > 2000f)
		{
			((ModProjectile)this).projectile.Center = player.Center;
		}
		if (num3 > 48f)
		{
			vector.Normalize();
			vector *= num;
			float num4 = 20f;
			((ModProjectile)this).projectile.velocity = (((ModProjectile)this).projectile.velocity * num4 + vector) / (num4 + 1f);
		}
		else
		{
			((ModProjectile)this).projectile.direction = Main.player[((ModProjectile)this).projectile.owner].direction;
			((ModProjectile)this).projectile.velocity *= (float)Math.Pow(0.9, 1.0);
		}
		((ModProjectile)this).projectile.rotation = ((ModProjectile)this).projectile.velocity.X * 0.05f;
		if ((double)Math.Abs(((ModProjectile)this).projectile.velocity.X) > 0.2)
		{
			((ModProjectile)this).projectile.spriteDirection = -((ModProjectile)this).projectile.direction;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (((ModProjectile)this).projectile.penetrate == 0)
		{
			((ModProjectile)this).projectile.Kill();
		}
		return false;
	}

	public override bool PreDraw(SpriteBatch sb, Color lightColor)
	{
		((ModProjectile)this).projectile.frameCounter++;
		if (((ModProjectile)this).projectile.frameCounter >= 15)
		{
			((ModProjectile)this).projectile.frame++;
			((ModProjectile)this).projectile.frameCounter = 0;
			if (((ModProjectile)this).projectile.frame > 3)
			{
				((ModProjectile)this).projectile.frame = 0;
			}
		}
		return true;
	}
}
