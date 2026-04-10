using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Guardians.Hell;

public class HellJavelin : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Igneous Impaler");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 46;
		((ModProjectile)this).projectile.height = 50;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ranged = true;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.tileCollide = true;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.rotation = (float)Math.Atan2(((ModProjectile)this).projectile.velocity.Y, ((ModProjectile)this).projectile.velocity.X) + 0.8f;
		((ModProjectile)this).projectile.ai[0] += 1f;
		if (((ModProjectile)this).projectile.ai[0] < 20f)
		{
			((ModProjectile)this).projectile.tileCollide = false;
		}
		if (((ModProjectile)this).projectile.ai[0] >= 20f)
		{
			((ModProjectile)this).projectile.tileCollide = true;
		}
		if (((ModProjectile)this).projectile.ai[0] >= 150f)
		{
			((ModProjectile)this).projectile.velocity.Y = ((ModProjectile)this).projectile.velocity.Y + 0.15f;
			((ModProjectile)this).projectile.velocity.X = ((ModProjectile)this).projectile.velocity.X * 0.99f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		((ModProjectile)this).projectile.Kill();
		Main.PlaySound(2, (int)((ModProjectile)this).projectile.position.X, (int)((ModProjectile)this).projectile.position.Y, 10, 1f, 0f);
		return false;
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		((ModProjectile)this).projectile.Kill();
		int num = 3;
		int num2 = Main.rand.Next(0, 180);
		for (int i = 0; i < num; i++)
		{
			float num3 = MathHelper.ToRadians(270 / num * i + num2);
			Vector2 vector = new Vector2(((ModProjectile)this).projectile.velocity.X, ((ModProjectile)this).projectile.velocity.Y).RotatedBy(num3);
			vector.Normalize();
			vector.X *= 3f;
			vector.Y *= 3f;
			Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, vector.X, vector.Y, 400, ((ModProjectile)this).projectile.damage, 2f, ((ModProjectile)this).projectile.owner, 0f, 0f);
		}
	}
}
