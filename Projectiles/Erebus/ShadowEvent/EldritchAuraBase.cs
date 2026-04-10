using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.ShadowEvent;

public class EldritchAuraBase : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Eldritch Aura");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 4;
		((ModProjectile)this).Projectile.height = 4;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.alpha = 60;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.timeLeft = 120;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.position.X = Main.player[((ModProjectile)this).Projectile.owner].Center.X - (float)(((ModProjectile)this).Projectile.width / 2);
		((ModProjectile)this).Projectile.position.Y = Main.player[((ModProjectile)this).Projectile.owner].Center.Y - (float)(((ModProjectile)this).Projectile.height / 2);
		if (((ModProjectile)this).Projectile.ai[0] != 0f)
		{
			return;
		}
		int num = 15;
		_ = ((ModProjectile)this).Projectile.whoAmI;
		((ModProjectile)this).Projectile.ai[1] = ((ModProjectile)this).Projectile.whoAmI;
		for (int i = 0; i < num; i++)
		{
			if (Main.player[((ModProjectile)this).Projectile.owner].ownedProjectileCounts[((ModProjectile)this).Mod.Find<ModProjectile>("EldritchAura1").Type] < num)
			{
				Projectile.NewProjectile(null, ((ModProjectile)this).Projectile.Center.X, ((ModProjectile)this).Projectile.Center.Y, 0f, 0f, ((ModProjectile)this).Mod.Find<ModProjectile>("EldritchAura1").Type, ((ModProjectile)this).Projectile.damage, ((ModProjectile)this).Projectile.knockBack, ((ModProjectile)this).Projectile.owner, (float)i, (float)((ModProjectile)this).Projectile.whoAmI);
			}
		}
	}
}
