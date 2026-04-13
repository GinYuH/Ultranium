using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent.Projectiles.Flayer;

public class FlayerAuraBase : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Flayer Aura");
	}

	public override void SetDefaults()
	{
		Projectile.width = 4;
		Projectile.height = 4;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.ignoreWater = true;
		Projectile.alpha = 255;
		Projectile.penetrate = -1;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 480;
	}

	public override void AI()
	{
		Projectile.velocity *= 0f;
		if (Projectile.ai[0] != 0f)
		{
			return;
		}
		int num = 35;
		_ = Projectile.whoAmI;
		Projectile.ai[1] = Projectile.whoAmI;
		for (int i = 0; i < num; i++)
		{
			if (Main.player[Projectile.owner].ownedProjectileCounts[Mod.Find<ModProjectile>("FlayerAura").Type] < num)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("FlayerAura").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, (float)i, (float)Projectile.whoAmI);
			}
		}
	}
}
