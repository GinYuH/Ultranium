using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Guardians.Hell;

public class HellJavelin : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Igneous Impaler");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 46;
		((ModProjectile)this).Projectile.height = 50;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Ranged;
		((ModProjectile)this).Projectile.penetrate = 1;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		((ModProjectile)this).Projectile.tileCollide = true;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.rotation = (float)Math.Atan2(((ModProjectile)this).Projectile.velocity.Y, ((ModProjectile)this).Projectile.velocity.X) + 0.8f;
		((ModProjectile)this).Projectile.ai[0] += 1f;
		if (((ModProjectile)this).Projectile.ai[0] < 20f)
		{
			((ModProjectile)this).Projectile.tileCollide = false;
		}
		if (((ModProjectile)this).Projectile.ai[0] >= 20f)
		{
			((ModProjectile)this).Projectile.tileCollide = true;
		}
		if (((ModProjectile)this).Projectile.ai[0] >= 150f)
		{
			((ModProjectile)this).Projectile.velocity.Y = ((ModProjectile)this).Projectile.velocity.Y + 0.15f;
			((ModProjectile)this).Projectile.velocity.X = ((ModProjectile)this).Projectile.velocity.X * 0.99f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		((ModProjectile)this).Projectile.Kill();
		SoundEngine.PlaySound(SoundID.Item10, new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y));
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		((ModProjectile)this).Projectile.Kill();
		int num = 3;
		int num2 = Main.rand.Next(0, 180);
		for (int i = 0; i < num; i++)
		{
			float num3 = MathHelper.ToRadians(270 / num * i + num2);
			Vector2 vector = new Vector2(((ModProjectile)this).Projectile.velocity.X, ((ModProjectile)this).Projectile.velocity.Y).RotatedBy(num3);
			vector.Normalize();
			vector.X *= 3f;
			vector.Y *= 3f;
			Projectile.NewProjectile(((ModProjectile)this).Projectile.Center.X, ((ModProjectile)this).Projectile.Center.Y, vector.X, vector.Y, 400, ((ModProjectile)this).Projectile.damage, 2f, ((ModProjectile)this).Projectile.owner, 0f, 0f);
		}
	}
}
