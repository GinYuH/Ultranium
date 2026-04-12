using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Stellar;

public class StellarBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Stellar Bolt");
	}

	public override void SetDefaults()
	{
		Projectile.width = 4;
		Projectile.height = 4;
		Projectile.friendly = true;
		Projectile.ignoreWater = true;
		Projectile.alpha = 60;
		Projectile.penetrate = 3;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 200;
		Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		if (Projectile.ai[0] == 0f)
		{
			int num = 1;
			_ = Projectile.whoAmI;
			for (int i = 0; i < num; i++)
			{
				int num2 = 12;
				Projectile.NewProjectile(null, Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("StellarBoltSwirl").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, (float)(i * num2), (float)Projectile.whoAmI);
			}
			Projectile.ai[0] = 1f;
		}
	}
}
