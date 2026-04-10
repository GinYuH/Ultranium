using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ignodium.Projectiles;

public class FlameBlastSpiral : ModProjectile
{
	public override string Texture => "Ultranium/NPCs/Ignodium/Projectiles/FlameBlast";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		((ModProjectile)this).DisplayName.SetDefault("Flame Blast");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 16;
		((ModProjectile)this).projectile.height = 16;
		((ModProjectile)this).projectile.scale = 1.2f;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.alpha = 0;
		((ModProjectile)this).projectile.timeLeft = 360;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/Ignodium/Projectiles/FlameBlastTrail").Width * 0.5f, (float)((ModProjectile)this).projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY);
			Color color = ((ModProjectile)this).projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).projectile.oldPos.Length - i) / (float)((ModProjectile)this).projectile.oldPos.Length);
			spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/Ignodium/Projectiles/FlameBlastTrail"), position, null, color, ((ModProjectile)this).projectile.rotation, vector, ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.localAI[0] += 1f;
		if (((ModProjectile)this).projectile.localAI[0] <= 180f)
		{
			((ModProjectile)this).projectile.velocity = ((ModProjectile)this).projectile.velocity.RotatedBy(MathHelper.ToRadians(1.5f * ((ModProjectile)this).projectile.ai[0]));
			((ModProjectile)this).projectile.velocity *= 1.005f;
		}
		((ModProjectile)this).projectile.rotation = (float)Math.Atan2(((ModProjectile)this).projectile.velocity.Y, ((ModProjectile)this).projectile.velocity.X) + 1.57f;
	}
}
