using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm.Projectiles;

public class EldritchEye : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Eldritch Eye");
		Main.projFrames[((ModProjectile)this).projectile.type] = 8;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 136;
		((ModProjectile)this).projectile.height = 136;
		((ModProjectile)this).projectile.timeLeft = 240;
		((ModProjectile)this).projectile.aiStyle = -1;
		((ModProjectile)this).projectile.alpha = 0;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.tileCollide = false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.velocity *= 0f;
		if (++((ModProjectile)this).projectile.frameCounter >= 5)
		{
			((ModProjectile)this).projectile.frameCounter = 0;
			if (++((ModProjectile)this).projectile.frame >= 8)
			{
				((ModProjectile)this).projectile.frame = 0;
			}
		}
	}

	public override void Kill(int timeLeft)
	{
		Main.PlaySound(2, (int)((ModProjectile)this).projectile.position.X, (int)((ModProjectile)this).projectile.position.Y, 14, 1f, 0f);
		for (int i = 0; i < 4; i++)
		{
			Vector2 vector = ((float)Math.PI / 2f * (float)i).ToRotationVector2();
			vector.Normalize();
			vector *= 7f;
			Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, vector.X, vector.Y, ((ModProjectile)this).mod.ProjectileType("EldritchBlast"), ((ModProjectile)this).projectile.damage, 1f, Main.myPlayer, 0f, 0f);
		}
	}
}
