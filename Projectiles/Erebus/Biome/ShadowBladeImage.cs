using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.Biome;

public class ShadowBladeImage : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Tenebris Sickle");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 32;
		((ModProjectile)this).Projectile.height = 36;
		((ModProjectile)this).Projectile.aiStyle = -1;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.penetrate = 1;
		((ModProjectile)this).Projectile.tileCollide = true;
		((ModProjectile)this).Projectile.timeLeft = 120;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[((ModProjectile)this).Projectile.owner] = 10;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.rotation += 0.4f;
		((ModProjectile)this).Projectile.velocity *= 0.99f;
		if (((ModProjectile)this).Projectile.timeLeft < 60)
		{
			((ModProjectile)this).Projectile.alpha += 3;
		}
	}
}
