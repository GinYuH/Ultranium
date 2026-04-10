using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Stellar;

public class StellarBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Stellar Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 4;
		((ModProjectile)this).projectile.height = 4;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.alpha = 60;
		((ModProjectile)this).projectile.penetrate = 3;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.timeLeft = 200;
		((ModProjectile)this).projectile.magic = true;
	}

	public override void AI()
	{
		if (((ModProjectile)this).projectile.ai[0] == 0f)
		{
			int num = 1;
			_ = ((ModProjectile)this).projectile.whoAmI;
			for (int i = 0; i < num; i++)
			{
				int num2 = 12;
				Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, 0f, 0f, ((ModProjectile)this).mod.ProjectileType("StellarBoltSwirl"), ((ModProjectile)this).projectile.damage, ((ModProjectile)this).projectile.knockBack, ((ModProjectile)this).projectile.owner, (float)(i * num2), (float)((ModProjectile)this).projectile.whoAmI);
			}
			((ModProjectile)this).projectile.ai[0] = 1f;
		}
	}
}
