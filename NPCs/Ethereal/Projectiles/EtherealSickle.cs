using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ethereal.Projectiles;

public class EtherealSickle : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 11;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		((ModProjectile)this).DisplayName.SetDefault("Ethereal Scythe");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.width = 42;
		((ModProjectile)this).projectile.height = 38;
		((ModProjectile)this).projectile.aiStyle = 0;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.penetrate = 2;
		((ModProjectile)this).projectile.timeLeft = 180;
		((ModProjectile)this).projectile.light = 0f;
		((ModProjectile)this).projectile.extraUpdates = 1;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(SpriteBatch sb, Color lightColor)
	{
		Texture2D texture2D = Main.projectileTexture[((ModProjectile)this).projectile.type];
		Vector2 vector = new Vector2((float)texture2D.Width * 0.5f, (float)((ModProjectile)this).projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY);
			Color color = ((ModProjectile)this).projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).projectile.oldPos.Length - i) / (float)((ModProjectile)this).projectile.oldPos.Length);
			Rectangle value = new Rectangle(0, texture2D.Height / Main.projFrames[((ModProjectile)this).projectile.type] * ((ModProjectile)this).projectile.frame, texture2D.Width, texture2D.Height / Main.projFrames[((ModProjectile)this).projectile.type]);
			sb.Draw(texture2D, position, value, color, ((ModProjectile)this).projectile.rotation, vector, ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.rotation += 0.2f;
		((ModProjectile)this).projectile.velocity *= 1.02f;
	}

	public override void Kill(int timeLeft)
	{
		((Entity)((ModProjectile)this).projectile).active = false;
		((ModProjectile)this).projectile.position.X = ((ModProjectile)this).projectile.position.X + (float)(((ModProjectile)this).projectile.width / 2);
		((ModProjectile)this).projectile.position.Y = ((ModProjectile)this).projectile.position.Y + (float)(((ModProjectile)this).projectile.height / 2);
		((ModProjectile)this).projectile.width = 30;
		((ModProjectile)this).projectile.height = 30;
		((ModProjectile)this).projectile.position.X = ((ModProjectile)this).projectile.position.X - (float)(((ModProjectile)this).projectile.width / 2);
		((ModProjectile)this).projectile.position.Y = ((ModProjectile)this).projectile.position.Y - (float)(((ModProjectile)this).projectile.height / 2);
		for (int i = 0; i < 20; i++)
		{
			int num = Dust.NewDust(new Vector2(((ModProjectile)this).projectile.position.X, ((ModProjectile)this).projectile.position.Y), ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 62, 0f, 0f, 100, default(Color), 2f);
			Main.dust[num].velocity *= 2f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num].scale = 0.5f;
				Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}
}
