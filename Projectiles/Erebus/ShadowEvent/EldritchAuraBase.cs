using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.ShadowEvent;

public class EldritchAuraBase : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Eldritch Aura");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 4;
		((ModProjectile)this).projectile.height = 4;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.alpha = 60;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.timeLeft = 120;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.position.X = Main.player[((ModProjectile)this).projectile.owner].Center.X - (float)(((ModProjectile)this).projectile.width / 2);
		((ModProjectile)this).projectile.position.Y = Main.player[((ModProjectile)this).projectile.owner].Center.Y - (float)(((ModProjectile)this).projectile.height / 2);
		if (((ModProjectile)this).projectile.ai[0] != 0f)
		{
			return;
		}
		int num = 15;
		_ = ((ModProjectile)this).projectile.whoAmI;
		((ModProjectile)this).projectile.ai[1] = ((ModProjectile)this).projectile.whoAmI;
		for (int i = 0; i < num; i++)
		{
			if (Main.player[((ModProjectile)this).projectile.owner].ownedProjectileCounts[((ModProjectile)this).mod.ProjectileType("EldritchAura1")] < num)
			{
				Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, 0f, 0f, ((ModProjectile)this).mod.ProjectileType("EldritchAura1"), ((ModProjectile)this).projectile.damage, ((ModProjectile)this).projectile.knockBack, ((ModProjectile)this).projectile.owner, (float)i, (float)((ModProjectile)this).projectile.whoAmI);
			}
		}
	}
}
