using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.ShadowEvent;

public class EldritchAuraBase : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Eldritch Aura");
	}

	public override void SetDefaults()
	{
		Projectile.width = 4;
		Projectile.height = 4;
		Projectile.friendly = true;
		Projectile.ignoreWater = true;
		Projectile.alpha = 60;
		Projectile.penetrate = -1;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 120;
	}

	public override void AI()
	{
		Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
		Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.height / 2);
		if (Projectile.ai[0] != 0f)
		{
			return;
		}
		int num = 15;
		_ = Projectile.whoAmI;
		Projectile.ai[1] = Projectile.whoAmI;
		for (int i = 0; i < num; i++)
		{
			if (Main.player[Projectile.owner].ownedProjectileCounts[Mod.Find<ModProjectile>("EldritchAura1").Type] < num)
			{
				Projectile.NewProjectile(null, Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("EldritchAura1").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, (float)i, (float)Projectile.whoAmI);
			}
		}
	}
}
