using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
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
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 35;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
		// ((ModProjectile)this).DisplayName.SetDefault("Cosmos Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.aiStyle = 132;
		((ModProjectile)this).Projectile.width = 72;
		((ModProjectile)this).Projectile.height = 72;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.aiStyle = 0;
		((ModProjectile)this).Projectile.penetrate = 3;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.timeLeft = 500;
		((ModProjectile)this).Projectile.alpha = 125;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		float amount = (float)(Main.GameUpdateCount % 60) / 60f;
		int num = (int)(Main.GameUpdateCount / 60 % 2);
		return Color.Lerp(ColorCycle[num], ColorCycle[(num + 1) % 2], amount);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)TextureAssets.Projectile[((ModProjectile)this).Projectile.type].Value.Width * 0.5f, (float)((ModProjectile)this).Projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).Projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).Projectile.gfxOffY);
			Color color = ((ModProjectile)this).Projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).Projectile.oldPos.Length - i) / (float)((ModProjectile)this).Projectile.oldPos.Length);
			spriteBatch.Draw(TextureAssets.Projectile[((ModProjectile)this).Projectile.type].Value, position, null, color, ((ModProjectile)this).Projectile.rotation, vector, ((ModProjectile)this).Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.rotation = ((ModProjectile)this).Projectile.velocity.ToRotation() + 1.57f;
		accel++;
		if (accel <= 85)
		{
			return;
		}
		if (((ModProjectile)this).Projectile.localAI[0] == 0f)
		{
			((ModProjectile)this).Projectile.localAI[0] = 1f;
		}
		((ModProjectile)this).Projectile.ai[0] -= 1f;
		if (((ModProjectile)this).Projectile.ai[0] > 0f)
		{
			float num = ((ModProjectile)this).Projectile.velocity.Length();
			num += ((ModProjectile)this).Projectile.ai[1];
			((ModProjectile)this).Projectile.velocity = Vector2.Normalize(((ModProjectile)this).Projectile.velocity) * num;
			return;
		}
		if (((ModProjectile)this).Projectile.ai[0] == 0f)
		{
			((ModProjectile)this).Projectile.ai[1] = (int)Player.FindClosest(((ModProjectile)this).Projectile.Center, 0, 0);
			if (((ModProjectile)this).Projectile.ai[1] != -1f && ((Entity)Main.player[(int)((ModProjectile)this).Projectile.ai[1]]).active && !Main.player[(int)((ModProjectile)this).Projectile.ai[1]].dead)
			{
				((ModProjectile)this).Projectile.velocity = ((ModProjectile)this).Projectile.DirectionTo(Main.player[(int)((ModProjectile)this).Projectile.ai[1]].Center);
				((ModProjectile)this).Projectile.netUpdate = true;
			}
			else
			{
				((ModProjectile)this).Projectile.Kill();
			}
			return;
		}
		((ModProjectile)this).Projectile.localAI[1] += 1f;
		if (((ModProjectile)this).Projectile.localAI[1] < 90f)
		{
			((ModProjectile)this).Projectile.velocity *= 1.023f;
		}
		if (((ModProjectile)this).Projectile.localAI[1] < 120f)
		{
			float curAngle = ((ModProjectile)this).Projectile.velocity.ToRotation();
			float targetAngle = (Main.player[(int)((ModProjectile)this).Projectile.ai[1]].Center - ((ModProjectile)this).Projectile.Center).ToRotation();
			((ModProjectile)this).Projectile.velocity = new Vector2(((ModProjectile)this).Projectile.velocity.Length(), 0f).RotatedBy(curAngle.AngleLerp(targetAngle, 0.025f));
		}
	}
}
