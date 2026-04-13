using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.Biome;

public class ShadowBladeImage : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Tenebris Sickle");
	}

	public override void SetDefaults()
	{
		Projectile.width = 32;
		Projectile.height = 36;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.penetrate = 1;
		Projectile.tileCollide = true;
		Projectile.timeLeft = 120;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 10;
	}

	public override void AI()
	{
		Projectile.rotation += 0.4f;
		Projectile.velocity *= 0.99f;
		if (Projectile.timeLeft < 60)
		{
			Projectile.alpha += 3;
		}
	}
}
