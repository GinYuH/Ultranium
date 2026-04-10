using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ultrum.Projectiles;

public class UltraBeam : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 4;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
		// ((ModProjectile)this).DisplayName.SetDefault("Ultranium Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 26;
		((ModProjectile)this).Projectile.height = 38;
		((ModProjectile)this).Projectile.penetrate = 1;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.alpha = 0;
		((ModProjectile)this).Projectile.timeLeft = 600;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/Ultrum/Projectiles/UltraBeamTrail").Width * 0.5f, (float)((ModProjectile)this).Projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).Projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).Projectile.gfxOffY);
			Color color = ((ModProjectile)this).Projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).Projectile.oldPos.Length - i) / (float)((ModProjectile)this).Projectile.oldPos.Length);
			spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/Ultrum/Projectiles/UltraBeamTrail"), position, null, color, ((ModProjectile)this).Projectile.rotation, vector, ((ModProjectile)this).Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.rotation = (float)Math.Atan2(((ModProjectile)this).Projectile.velocity.Y, ((ModProjectile)this).Projectile.velocity.X) + 1.57f;
		if (((ModProjectile)this).Projectile.localAI[0] == 0f)
		{
			SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y));
			((ModProjectile)this).Projectile.localAI[0] = 1f;
		}
		((ModProjectile)this).Projectile.localAI[1] += 1f;
		if (((ModProjectile)this).Projectile.localAI[1] == 60f)
		{
			double num = Math.Atan2(Main.player[Main.myPlayer].position.Y - ((ModProjectile)this).Projectile.position.Y, Main.player[Main.myPlayer].position.X - ((ModProjectile)this).Projectile.position.X);
			((ModProjectile)this).Projectile.velocity = new Vector2((float)Math.Cos(num), (float)Math.Sin(num)) * 20f;
		}
	}
}
