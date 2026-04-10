using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pumpkin;

public class PumpkinSeed : ModProjectile
{
	public float Timer
	{
		get
		{
			return ((ModProjectile)this).Projectile.ai[0];
		}
		set
		{
			((ModProjectile)this).Projectile.ai[0] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Pumpkin Seed");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 20;
		((ModProjectile)this).Projectile.aiStyle = 1;
		((ModProjectile)this).Projectile.height = 20;
		((ModProjectile)this).Projectile.timeLeft = 60;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Magic;
	}
}
