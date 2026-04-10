using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Aldin.Projectiles;

public class CosmosBoltHome : ModProjectile
{
	private int accel;

	private Color[] ColorCycle = new Color[2]
	{
		new Color(117, 235, 215),
		new Color(62, 30, 152)
	};

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 35;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		((ModProjectile)this).DisplayName.SetDefault("Cosmos Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.aiStyle = 132;
		((ModProjectile)this).projectile.width = 72;
		((ModProjectile)this).projectile.height = 72;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.aiStyle = 0;
		((ModProjectile)this).projectile.penetrate = 3;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.timeLeft = 500;
		((ModProjectile)this).projectile.alpha = 125;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		float amount = (float)(Main.GameUpdateCount % 60) / 60f;
		int num = (int)(Main.GameUpdateCount / 60 % 2);
		return Color.Lerp(ColorCycle[num], ColorCycle[(num + 1) % 2], amount);
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
		((ModProjectile)this).projectile.rotation = ((ModProjectile)this).projectile.velocity.ToRotation() + 1.57f;
		accel++;
		if (accel <= 85)
		{
			return;
		}
		if (((ModProjectile)this).projectile.localAI[0] == 0f)
		{
			((ModProjectile)this).projectile.localAI[0] = 1f;
		}
		((ModProjectile)this).projectile.ai[0] -= 1f;
		if (((ModProjectile)this).projectile.ai[0] > 0f)
		{
			float num = ((ModProjectile)this).projectile.velocity.Length();
			num += ((ModProjectile)this).projectile.ai[1];
			((ModProjectile)this).projectile.velocity = Vector2.Normalize(((ModProjectile)this).projectile.velocity) * num;
			return;
		}
		if (((ModProjectile)this).projectile.ai[0] == 0f)
		{
			((ModProjectile)this).projectile.ai[1] = (int)Player.FindClosest(((ModProjectile)this).projectile.Center, 0, 0);
			if (((ModProjectile)this).projectile.ai[1] != -1f && ((Entity)Main.player[(int)((ModProjectile)this).projectile.ai[1]]).active && !Main.player[(int)((ModProjectile)this).projectile.ai[1]].dead)
			{
				((ModProjectile)this).projectile.velocity = ((ModProjectile)this).projectile.DirectionTo(Main.player[(int)((ModProjectile)this).projectile.ai[1]].Center);
				((ModProjectile)this).projectile.netUpdate = true;
			}
			else
			{
				((ModProjectile)this).projectile.Kill();
			}
			return;
		}
		((ModProjectile)this).projectile.localAI[1] += 1f;
		if (((ModProjectile)this).projectile.localAI[1] < 90f)
		{
			((ModProjectile)this).projectile.velocity *= 1.023f;
		}
		if (((ModProjectile)this).projectile.localAI[1] < 120f)
		{
			float curAngle = ((ModProjectile)this).projectile.velocity.ToRotation();
			float targetAngle = (Main.player[(int)((ModProjectile)this).projectile.ai[1]].Center - ((ModProjectile)this).projectile.Center).ToRotation();
			((ModProjectile)this).projectile.velocity = new Vector2(((ModProjectile)this).projectile.velocity.Length(), 0f).RotatedBy(curAngle.AngleLerp(targetAngle, 0.025f));
		}
	}
}
