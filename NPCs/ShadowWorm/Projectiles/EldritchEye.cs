using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm.Projectiles;

public class EldritchEye : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Eldritch Eye");
		Main.projFrames[((ModProjectile)this).Projectile.type] = 8;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 136;
		((ModProjectile)this).Projectile.height = 136;
		((ModProjectile)this).Projectile.timeLeft = 240;
		((ModProjectile)this).Projectile.aiStyle = -1;
		((ModProjectile)this).Projectile.alpha = 0;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.tileCollide = false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.velocity *= 0f;
		if (++((ModProjectile)this).Projectile.frameCounter >= 5)
		{
			((ModProjectile)this).Projectile.frameCounter = 0;
			if (++((ModProjectile)this).Projectile.frame >= 8)
			{
				((ModProjectile)this).Projectile.frame = 0;
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item14, new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y));
		for (int i = 0; i < 4; i++)
		{
			Vector2 vector = ((float)Math.PI / 2f * (float)i).ToRotationVector2();
			vector.Normalize();
			vector *= 7f;
			Projectile.NewProjectile(((ModProjectile)this).Projectile.Center.X, ((ModProjectile)this).Projectile.Center.Y, vector.X, vector.Y, ((ModProjectile)this).Mod.Find<ModProjectile>("EldritchBlast").Type, ((ModProjectile)this).Projectile.damage, 1f, Main.myPlayer, 0f, 0f);
		}
	}
}
