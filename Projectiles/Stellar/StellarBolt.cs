using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Stellar;

public class StellarBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Stellar Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 4;
		((ModProjectile)this).Projectile.height = 4;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.alpha = 60;
		((ModProjectile)this).Projectile.penetrate = 3;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.timeLeft = 200;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		if (((ModProjectile)this).Projectile.ai[0] == 0f)
		{
			int num = 1;
			_ = ((ModProjectile)this).Projectile.whoAmI;
			for (int i = 0; i < num; i++)
			{
				int num2 = 12;
				Projectile.NewProjectile(null, ((ModProjectile)this).Projectile.Center.X, ((ModProjectile)this).Projectile.Center.Y, 0f, 0f, ((ModProjectile)this).Mod.Find<ModProjectile>("StellarBoltSwirl").Type, ((ModProjectile)this).Projectile.damage, ((ModProjectile)this).Projectile.knockBack, ((ModProjectile)this).Projectile.owner, (float)(i * num2), (float)((ModProjectile)this).Projectile.whoAmI);
			}
			((ModProjectile)this).Projectile.ai[0] = 1f;
		}
	}
}
