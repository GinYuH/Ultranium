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
		//DisplayName.SetDefault("Igneous Impaler");
	}

	public override void SetDefaults()
	{
		Projectile.width = 46;
		Projectile.height = 50;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.penetrate = 1;
		Projectile.extraUpdates = 1;
		Projectile.tileCollide = true;
	}

	public override void AI()
	{
		Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 0.8f;
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] < 20f)
		{
			Projectile.tileCollide = false;
		}
		if (Projectile.ai[0] >= 20f)
		{
			Projectile.tileCollide = true;
		}
		if (Projectile.ai[0] >= 150f)
		{
			Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;
			Projectile.velocity.X = Projectile.velocity.X * 0.99f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Projectile.Kill();
		SoundEngine.PlaySound(SoundID.Item10, new Vector2(Projectile.position.X, Projectile.position.Y));
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Projectile.Kill();
		int num = 3;
		int num2 = Main.rand.Next(0, 180);
		for (int i = 0; i < num; i++)
		{
			float num3 = MathHelper.ToRadians(270 / num * i + num2);
			Vector2 vector = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(num3);
			vector.Normalize();
			vector.X *= 3f;
			vector.Y *= 3f;
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, vector.X, vector.Y, 400, Projectile.damage, 2f, Projectile.owner, 0f, 0f);
		}
	}
}
