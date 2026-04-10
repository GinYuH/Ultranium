using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.Biome;

public class ShadowBladeImage : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Tenebris Sickle");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 32;
		((ModProjectile)this).projectile.height = 36;
		((ModProjectile)this).projectile.aiStyle = -1;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.timeLeft = 120;
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.immune[((ModProjectile)this).projectile.owner] = 10;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.rotation += 0.4f;
		((ModProjectile)this).projectile.velocity *= 0.99f;
		if (((ModProjectile)this).projectile.timeLeft < 60)
		{
			((ModProjectile)this).projectile.alpha += 3;
		}
	}
}
