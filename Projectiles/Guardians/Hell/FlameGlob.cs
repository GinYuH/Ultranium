using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Guardians.Hell;

public class FlameGlob : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Flame Glob");
		Main.projFrames[((ModProjectile)this).Projectile.type] = 4;
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.scale = 1f;
		((ModProjectile)this).Projectile.width = 55;
		((ModProjectile)this).Projectile.height = 55;
		((ModProjectile)this).Projectile.aiStyle = -1;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Magic;
		((ModProjectile)this).Projectile.penetrate = 1;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.timeLeft = 200;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		((ModProjectile)this).Projectile.frameCounter++;
		if (((ModProjectile)this).Projectile.frameCounter >= 11)
		{
			((ModProjectile)this).Projectile.frame++;
			((ModProjectile)this).Projectile.frameCounter = 0;
			if (((ModProjectile)this).Projectile.frame >= 4)
			{
				((ModProjectile)this).Projectile.frame = 0;
			}
		}
		Texture2D texture2D = TextureAssets.Projectile[((ModProjectile)this).Projectile.type].Value;
		Vector2 vector = new Vector2((float)texture2D.Width * 0.5f, (float)((ModProjectile)this).Projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).Projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).Projectile.gfxOffY);
			Color color = ((ModProjectile)this).Projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).Projectile.oldPos.Length - i) / (float)((ModProjectile)this).Projectile.oldPos.Length);
			Rectangle value = new Rectangle(0, texture2D.Height / Main.projFrames[((ModProjectile)this).Projectile.type] * ((ModProjectile)this).Projectile.frame, texture2D.Width, texture2D.Height / Main.projFrames[((ModProjectile)this).Projectile.type]);
			sb.Draw(texture2D, position, value, color, ((ModProjectile)this).Projectile.rotation, vector, ((ModProjectile)this).Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.localAI[0] += 1f;
		if (((ModProjectile)this).Projectile.localAI[0] == 12f)
		{
			((ModProjectile)this).Projectile.localAI[0] = 0f;
			for (int i = 0; i < 12; i++)
			{
				Vector2 spinningpoint = Vector2.UnitX * (0f - (float)((ModProjectile)this).Projectile.width) / 2f;
				spinningpoint += -Vector2.UnitY.RotatedBy((float)i * (float)Math.PI / 6f) * new Vector2(8f, 16f);
				spinningpoint = spinningpoint.RotatedBy(((ModProjectile)this).Projectile.rotation - (float)Math.PI / 2f);
				int num = Dust.NewDust(((ModProjectile)this).Projectile.Center, 0, 0, 6, 0f, 0f, 160);
				Main.dust[num].scale = 1.1f;
				Main.dust[num].noGravity = true;
				Main.dust[num].position = ((ModProjectile)this).Projectile.Center + spinningpoint;
				Main.dust[num].velocity = ((ModProjectile)this).Projectile.velocity * 0.1f;
				Main.dust[num].velocity = Vector2.Normalize(((ModProjectile)this).Projectile.Center - ((ModProjectile)this).Projectile.velocity * 3f - Main.dust[num].position) * 1.25f;
			}
		}
		((ModProjectile)this).Projectile.rotation = (float)Math.Atan2(((ModProjectile)this).Projectile.velocity.Y, ((ModProjectile)this).Projectile.velocity.X) + 1.57f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		for (int i = 0; i < 6; i++)
		{
			Vector2 vector = ((float)Math.PI / 3f * (float)i).ToRotationVector2();
			vector.Normalize();
			vector *= 5f;
			Projectile.NewProjectile(((ModProjectile)this).Projectile.Center.X, ((ModProjectile)this).Projectile.Center.Y, vector.X, vector.Y, ((ModProjectile)this).Mod.Find<ModProjectile>("FlareBlast").Type, ((ModProjectile)this).Projectile.damage, 1f, Main.myPlayer, 0f, 0f);
		}
	}
}
