using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Aldin.Projectiles;

public class CosmicWrathBolt2 : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Cosmic Bolt");
	}

	public override void SetDefaults()
	{
		Projectile.width = 18;
		Projectile.height = 18;
		Projectile.hostile = true;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		Projectile.aiStyle = -1;
		Projectile.alpha = 255;
	}

	public override void AI()
	{
		Projectile.velocity *= 0f;
		if (!((Projectile.ai[0] += 1f) > 5f))
		{
			return;
		}
		Projectile.ai[0] = 0f;
		if (Main.netMode != 1)
		{
			for (int i = -1; i <= 1; i++)
			{
				if (i != 0)
				{
					Vector2 center = Projectile.Center;
					center.X += 160f * Projectile.ai[1] * (float)i;
					Projectile.NewProjectile(null, center, Vector2.UnitY * -18f, Mod.Find<ModProjectile>("CosmosBolt").Type, Projectile.damage, 0f, Main.myPlayer, 210f, 0f);
				}
			}
		}
		if ((Projectile.ai[1] += 1f) > 10f)
		{
			Projectile.Kill();
		}
	}
}
