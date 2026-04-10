using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class TacoDemon : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Taco Demon");
		Main.projFrames[((ModProjectile)this).Projectile.type] = 1;
		Main.projPet[((ModProjectile)this).Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 34;
		((ModProjectile)this).Projectile.height = 34;
		((ModProjectile)this).Projectile.netImportant = true;
		((ModProjectile)this).Projectile.friendly = true;
		Main.projPet[((ModProjectile)this).Projectile.type] = true;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.timeLeft = 999999999;
		((ModProjectile)this).Projectile.timeLeft *= 999999999;
		((ModProjectile)this).Projectile.penetrate = -1;
	}

	public override void AI()
	{
		Player player = Main.player[((ModProjectile)this).Projectile.owner];
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.dead)
		{
			modPlayer.TacoDemon = false;
		}
		if (modPlayer.TacoDemon)
		{
			((ModProjectile)this).Projectile.timeLeft = 2;
		}
		if (!Collision.CanHitLine(((ModProjectile)this).Projectile.Center, 1, 1, player.Center, 1, 1))
		{
			((ModProjectile)this).Projectile.ai[0] = 1f;
		}
		float num = 8f;
		if (((ModProjectile)this).Projectile.ai[0] == 1f)
		{
			num = 17f;
		}
		Vector2 center = ((ModProjectile)this).Projectile.Center;
		Vector2 vector = player.Center - center;
		((ModProjectile)this).Projectile.ai[1] = 3600f;
		((ModProjectile)this).Projectile.netUpdate = true;
		int num2 = 1;
		for (int i = 0; i < ((ModProjectile)this).Projectile.whoAmI; i++)
		{
			if (((Entity)Main.projectile[i]).active && Main.projectile[i].owner == ((ModProjectile)this).Projectile.owner && Main.projectile[i].type == ((ModProjectile)this).Projectile.type)
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
		if (num3 < 100f && ((ModProjectile)this).Projectile.ai[0] == 1f && !Collision.SolidCollision(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height))
		{
			((ModProjectile)this).Projectile.ai[0] = 0f;
			((ModProjectile)this).Projectile.netUpdate = true;
		}
		if (num3 > 2000f)
		{
			((ModProjectile)this).Projectile.Center = player.Center;
		}
		if (num3 > 48f)
		{
			vector.Normalize();
			vector *= num;
			float num4 = 20f;
			((ModProjectile)this).Projectile.velocity = (((ModProjectile)this).Projectile.velocity * num4 + vector) / (num4 + 1f);
		}
		else
		{
			((ModProjectile)this).Projectile.direction = Main.player[((ModProjectile)this).Projectile.owner].direction;
			((ModProjectile)this).Projectile.velocity *= (float)Math.Pow(0.9, 1.0);
		}
		((ModProjectile)this).Projectile.rotation = ((ModProjectile)this).Projectile.velocity.X * 0.05f;
		if ((double)Math.Abs(((ModProjectile)this).Projectile.velocity.X) > 0.2)
		{
			((ModProjectile)this).Projectile.spriteDirection = -((ModProjectile)this).Projectile.direction;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (((ModProjectile)this).Projectile.penetrate == 0)
		{
			((ModProjectile)this).Projectile.Kill();
		}
		return false;
	}
}
