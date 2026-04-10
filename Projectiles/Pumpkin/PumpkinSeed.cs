using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pumpkin;

public class PumpkinSeed : ModProjectile
{
	public float Timer
	{
		get
		{
			return ((ModProjectile)this).projectile.ai[0];
		}
		set
		{
			((ModProjectile)this).projectile.ai[0] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Pumpkin Seed");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 20;
		((ModProjectile)this).projectile.aiStyle = 1;
		((ModProjectile)this).projectile.height = 20;
		((ModProjectile)this).projectile.timeLeft = 60;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.magic = true;
	}
}
