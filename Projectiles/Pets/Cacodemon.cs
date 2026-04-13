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
		//DisplayName.SetDefault("Cacodemon");
		Main.projFrames[Projectile.type] = 4;
		Main.projPet[Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		Projectile.width = 34;
		Projectile.height = 34;
		Projectile.netImportant = true;
		Projectile.friendly = true;
		Main.projPet[Projectile.type] = true;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 999999999;
		Projectile.timeLeft *= 999999999;
		Projectile.penetrate = -1;
	}

	public override void AI()
	{
		Player player = Main.player[Projectile.owner];
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.dead)
		{
			modPlayer.Cacodemon = false;
		}
		if (modPlayer.Cacodemon)
		{
			Projectile.timeLeft = 2;
		}
		if (++Projectile.frameCounter >= 60)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= 4)
			{
				Projectile.frame = 0;
			}
		}
		if (!Collision.CanHitLine(Projectile.Center, 1, 1, player.Center, 1, 1))
		{
			Projectile.ai[0] = 1f;
		}
		float num = 8f;
		if (Projectile.ai[0] == 1f)
		{
			num = 17f;
		}
		Vector2 center = Projectile.Center;
		Vector2 vector = player.Center - center;
		Projectile.ai[1] = 3600f;
		Projectile.netUpdate = true;
		int num2 = 1;
		for (int i = 0; i < Projectile.whoAmI; i++)
		{
			if (((Entity)Main.projectile[i]).active && Main.projectile[i].owner == Projectile.owner && Main.projectile[i].type == Projectile.type)
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
		if (num3 < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
		{
			Projectile.ai[0] = 0f;
			Projectile.netUpdate = true;
		}
		if (num3 > 2000f)
		{
			Projectile.Center = player.Center;
		}
		if (num3 > 48f)
		{
			vector.Normalize();
			vector *= num;
			float num4 = 20f;
			Projectile.velocity = (Projectile.velocity * num4 + vector) / (num4 + 1f);
		}
		else
		{
			Projectile.direction = Main.player[Projectile.owner].direction;
			Projectile.velocity *= (float)Math.Pow(0.9, 1.0);
		}
		Projectile.rotation = Projectile.velocity.X * 0.05f;
		if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
		{
			Projectile.spriteDirection = -Projectile.direction;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (Projectile.penetrate == 0)
		{
			Projectile.Kill();
		}
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Projectile.frameCounter++;
		if (Projectile.frameCounter >= 15)
		{
			Projectile.frame++;
			Projectile.frameCounter = 0;
			if (Projectile.frame > 3)
			{
				Projectile.frame = 0;
			}
		}
		return true;
	}
}
