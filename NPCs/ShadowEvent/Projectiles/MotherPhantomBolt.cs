using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent.Projectiles;

public class MotherPhantomBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 35;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		((ModProjectile)this).DisplayName.SetDefault("Darkness Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.aiStyle = 132;
		((ModProjectile)this).projectile.width = 72;
		((ModProjectile)this).projectile.height = 72;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.aiStyle = 0;
		((ModProjectile)this).projectile.penetrate = 3;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.timeLeft = 420;
		((ModProjectile)this).projectile.alpha = 125;
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

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.rotation = ((ModProjectile)this).projectile.velocity.ToRotation() + 1.57f;
		if (((ModProjectile)this).projectile.localAI[0] == 0f)
		{
			((ModProjectile)this).projectile.localAI[0] = 1f;
			Main.PlaySound(SoundID.Item20, ((ModProjectile)this).projectile.position);
		}
		if ((((ModProjectile)this).projectile.ai[0] -= 1f) > 0f)
		{
			float num = ((ModProjectile)this).projectile.velocity.Length();
			num += ((ModProjectile)this).projectile.ai[1];
			((ModProjectile)this).projectile.velocity = Vector2.Normalize(((ModProjectile)this).projectile.velocity) * num;
		}
		else if (((ModProjectile)this).projectile.ai[0] == 0f)
		{
			((ModProjectile)this).projectile.ai[1] = (int)Player.FindClosest(((ModProjectile)this).projectile.Center, 0, 0);
			if (((ModProjectile)this).projectile.ai[1] != -1f && ((Entity)Main.player[(int)((ModProjectile)this).projectile.ai[1]]).active && !Main.player[(int)((ModProjectile)this).projectile.ai[1]].dead)
			{
				((ModProjectile)this).projectile.velocity = ((ModProjectile)this).projectile.DirectionTo(Main.player[(int)((ModProjectile)this).projectile.ai[1]].Center);
				((ModProjectile)this).projectile.netUpdate = true;
			}
		}
		else if (((ModProjectile)this).projectile.localAI[1] < 300f)
		{
			float curAngle = ((ModProjectile)this).projectile.velocity.ToRotation();
			float targetAngle = (Main.player[(int)((ModProjectile)this).projectile.ai[1]].Center - ((ModProjectile)this).projectile.Center).ToRotation();
			((ModProjectile)this).projectile.velocity = new Vector2(((ModProjectile)this).projectile.velocity.Length(), 0f).RotatedBy(curAngle.AngleLerp(targetAngle, 0.01f));
		}
	}

	public override void Kill(int timeLeft)
	{
		Main.PlaySound(2, (int)((ModProjectile)this).projectile.position.X, (int)((ModProjectile)this).projectile.position.Y, 14, 1f, 0f);
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("ShadowDustPurple"), 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).projectile.DirectionTo(Main.dust[num].position) * 10f;
			}
		}
		for (int j = 0; j < 40; j++)
		{
			int num2 = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("ShadowDustPurple"), 0f, -2f, 0, default(Color), 2f);
			Main.dust[num2].noGravity = true;
			Main.dust[num2].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num2].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num2].position != ((ModProjectile)this).projectile.Center)
			{
				Main.dust[num2].velocity = ((ModProjectile)this).projectile.DirectionTo(Main.dust[num2].position) * 3f;
			}
		}
	}
}
