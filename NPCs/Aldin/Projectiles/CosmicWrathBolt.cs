using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Aldin.Projectiles;

public class CosmicWrathBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Cosmic Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 18;
		((ModProjectile)this).projectile.height = 18;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.aiStyle = -1;
		((ModProjectile)this).projectile.alpha = 255;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.velocity *= 0f;
		if (!((((ModProjectile)this).projectile.ai[0] += 1f) > 5f))
		{
			return;
		}
		((ModProjectile)this).projectile.ai[0] = 0f;
		if (Main.netMode != 1)
		{
			for (int i = -1; i <= 1; i++)
			{
				if (i != 0)
				{
					Vector2 center = ((ModProjectile)this).projectile.Center;
					center.X += 160f * ((ModProjectile)this).projectile.ai[1] * (float)i;
					Projectile.NewProjectile(center, Vector2.UnitY * 18f, ((ModProjectile)this).mod.ProjectileType("CosmosBolt"), ((ModProjectile)this).projectile.damage, 0f, Main.myPlayer, 210f, 0f);
				}
			}
		}
		if ((((ModProjectile)this).projectile.ai[1] += 1f) > 10f)
		{
			((ModProjectile)this).projectile.Kill();
		}
	}
}
