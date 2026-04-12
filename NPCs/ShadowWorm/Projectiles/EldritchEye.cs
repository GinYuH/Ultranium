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
		DisplayName.SetDefault("Eldritch Eye");
		Main.projFrames[Projectile.type] = 8;
	}

	public override void SetDefaults()
	{
		Projectile.width = 136;
		Projectile.height = 136;
		Projectile.timeLeft = 240;
		Projectile.aiStyle = -1;
		Projectile.alpha = 0;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.tileCollide = false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		Projectile.velocity *= 0f;
		if (++Projectile.frameCounter >= 5)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= 8)
			{
				Projectile.frame = 0;
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item14, new Vector2(Projectile.position.X, Projectile.position.Y));
		for (int i = 0; i < 4; i++)
		{
			Vector2 vector = ((float)Math.PI / 2f * (float)i).ToRotationVector2();
			vector.Normalize();
			vector *= 7f;
			Projectile.NewProjectile(null, Projectile.Center.X, Projectile.Center.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("EldritchBlast").Type, Projectile.damage, 1f, Main.myPlayer, 0f, 0f);
		}
	}
}
