using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Aldin.Projectiles;

public class CosmicWrathBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Cosmic Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 18;
		((ModProjectile)this).Projectile.height = 18;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.aiStyle = -1;
		((ModProjectile)this).Projectile.alpha = 255;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.velocity *= 0f;
		if (!((((ModProjectile)this).Projectile.ai[0] += 1f) > 5f))
		{
			return;
		}
		((ModProjectile)this).Projectile.ai[0] = 0f;
		if (Main.netMode != 1)
		{
			for (int i = -1; i <= 1; i++)
			{
				if (i != 0)
				{
					Vector2 center = ((ModProjectile)this).Projectile.Center;
					center.X += 160f * ((ModProjectile)this).Projectile.ai[1] * (float)i;
					Projectile.NewProjectile(center, Vector2.UnitY * 18f, ((ModProjectile)this).Mod.Find<ModProjectile>("CosmosBolt").Type, ((ModProjectile)this).Projectile.damage, 0f, Main.myPlayer, 210f, 0f);
				}
			}
		}
		if ((((ModProjectile)this).Projectile.ai[1] += 1f) > 10f)
		{
			((ModProjectile)this).Projectile.Kill();
		}
	}
}
