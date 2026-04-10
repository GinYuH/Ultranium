using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent.Projectiles.Flayer;

public class FlayerAuraBase : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Flayer Aura");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 4;
		((ModProjectile)this).projectile.height = 4;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.timeLeft = 480;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.velocity *= 0f;
		if (((ModProjectile)this).projectile.ai[0] != 0f)
		{
			return;
		}
		int num = 35;
		_ = ((ModProjectile)this).projectile.whoAmI;
		((ModProjectile)this).projectile.ai[1] = ((ModProjectile)this).projectile.whoAmI;
		for (int i = 0; i < num; i++)
		{
			if (Main.player[((ModProjectile)this).projectile.owner].ownedProjectileCounts[((ModProjectile)this).mod.ProjectileType("FlayerAura")] < num)
			{
				Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, 0f, 0f, ((ModProjectile)this).mod.ProjectileType("FlayerAura"), ((ModProjectile)this).projectile.damage, ((ModProjectile)this).projectile.knockBack, ((ModProjectile)this).projectile.owner, (float)i, (float)((ModProjectile)this).projectile.whoAmI);
			}
		}
	}
}
