using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pumpkin;

public class PumpkinSeed : ModProjectile
{
	public float Timer
	{
		get
		{
			return Projectile.ai[0];
		}
		set
		{
			Projectile.ai[0] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Pumpkin Seed");
	}

	public override void SetDefaults()
	{
		Projectile.width = 20;
		Projectile.aiStyle = 1;
		Projectile.height = 20;
		Projectile.timeLeft = 60;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Magic;
	}
}
