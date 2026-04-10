using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent.Projectiles.Flayer;

public class FlayerAuraBase : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Flayer Aura");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 4;
		((ModProjectile)this).Projectile.height = 4;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.alpha = 255;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.timeLeft = 480;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.velocity *= 0f;
		if (((ModProjectile)this).Projectile.ai[0] != 0f)
		{
			return;
		}
		int num = 35;
		_ = ((ModProjectile)this).Projectile.whoAmI;
		((ModProjectile)this).Projectile.ai[1] = ((ModProjectile)this).Projectile.whoAmI;
		for (int i = 0; i < num; i++)
		{
			if (Main.player[((ModProjectile)this).Projectile.owner].ownedProjectileCounts[((ModProjectile)this).Mod.Find<ModProjectile>("FlayerAura").Type] < num)
			{
				Projectile.NewProjectile(((ModProjectile)this).Projectile.Center.X, ((ModProjectile)this).Projectile.Center.Y, 0f, 0f, ((ModProjectile)this).Mod.Find<ModProjectile>("FlayerAura").Type, ((ModProjectile)this).Projectile.damage, ((ModProjectile)this).Projectile.knockBack, ((ModProjectile)this).Projectile.owner, (float)i, (float)((ModProjectile)this).Projectile.whoAmI);
			}
		}
	}
}
