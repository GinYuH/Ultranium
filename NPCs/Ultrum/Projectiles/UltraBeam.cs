using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ultrum.Projectiles;

public class UltraBeam : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 4;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		((ModProjectile)this).DisplayName.SetDefault("Ultranium Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 26;
		((ModProjectile)this).projectile.height = 38;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.alpha = 0;
		((ModProjectile)this).projectile.timeLeft = 600;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/Ultrum/Projectiles/UltraBeamTrail").Width * 0.5f, (float)((ModProjectile)this).projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY);
			Color color = ((ModProjectile)this).projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).projectile.oldPos.Length - i) / (float)((ModProjectile)this).projectile.oldPos.Length);
			spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/Ultrum/Projectiles/UltraBeamTrail"), position, null, color, ((ModProjectile)this).projectile.rotation, vector, ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.rotation = (float)Math.Atan2(((ModProjectile)this).projectile.velocity.Y, ((ModProjectile)this).projectile.velocity.X) + 1.57f;
		if (((ModProjectile)this).projectile.localAI[0] == 0f)
		{
			Main.PlaySound(2, (int)((ModProjectile)this).projectile.position.X, (int)((ModProjectile)this).projectile.position.Y, 20, 1f, 0f);
			((ModProjectile)this).projectile.localAI[0] = 1f;
		}
		((ModProjectile)this).projectile.localAI[1] += 1f;
		if (((ModProjectile)this).projectile.localAI[1] == 60f)
		{
			double num = Math.Atan2(Main.player[Main.myPlayer].position.Y - ((ModProjectile)this).projectile.position.Y, Main.player[Main.myPlayer].position.X - ((ModProjectile)this).projectile.position.X);
			((ModProjectile)this).projectile.velocity = new Vector2((float)Math.Cos(num), (float)Math.Sin(num)) * 20f;
		}
	}
}
