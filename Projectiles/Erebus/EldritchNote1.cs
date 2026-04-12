using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus;

public class EldritchNote1 : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Eldritch Note");
	}

	public override void SetDefaults()
	{
		Projectile.width = 18;
		Projectile.height = 24;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 300;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 8;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] >= 65f)
		{
			Projectile.velocity *= 0.98f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("ShadowDustPurple").Type, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != Projectile.Center)
			{
				Main.dust[num].velocity = Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}
}
