using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Blood;

public class BloodBall : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		((ModProjectile)this).DisplayName.SetDefault("Blood Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 14;
		((ModProjectile)this).projectile.height = 14;
		((ModProjectile)this).projectile.aiStyle = 18;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.magic = true;
		((ModProjectile)this).projectile.penetrate = 3;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.timeLeft = 200;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.aiStyle = 0;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Vector2 vector = new Vector2((float)Main.projectileTexture[((ModProjectile)this).projectile.type].Width * 0.5f, (float)((ModProjectile)this).projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY);
			Color color = ((ModProjectile)this).projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).projectile.oldPos.Length - i) / (float)((ModProjectile)this).projectile.oldPos.Length);
			spriteBatch.Draw(Main.projectileTexture[((ModProjectile)this).projectile.type], position, null, color, ((ModProjectile)this).projectile.rotation, vector, ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.ai[0] += 1f;
		if (((ModProjectile)this).projectile.ai[0] >= 30f)
		{
			((ModProjectile)this).projectile.velocity.Y = ((ModProjectile)this).projectile.velocity.Y + 0.15f;
			((ModProjectile)this).projectile.velocity.X = ((ModProjectile)this).projectile.velocity.X * 0.99f;
		}
		((ModProjectile)this).projectile.rotation += 0.02f;
	}

	public override void Kill(int timeLeft)
	{
		Main.PlaySound(2, (int)((ModProjectile)this).projectile.position.X, (int)((ModProjectile)this).projectile.position.Y, 86, 1f, 0f);
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 5, 0f, -2f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 0.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 0.5f;
			if (Main.dust[num].position != ((ModProjectile)this).projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}
}
