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
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 35;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		// DisplayName.SetDefault("Cosmos Bolt");
	}

	public override void SetDefaults()
	{
		Projectile.aiStyle = 132;
		Projectile.width = 72;
		Projectile.height = 72;
		Projectile.hostile = true;
		Projectile.friendly = false;
		Projectile.aiStyle = 0;
		Projectile.penetrate = 3;
		Projectile.extraUpdates = 1;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 500;
		Projectile.alpha = 125;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		float amount = (float)(Main.GameUpdateCount % 60) / 60f;
		int num = (int)(Main.GameUpdateCount / 60 % 2);
		return Color.Lerp(ColorCycle[num], ColorCycle[(num + 1) % 2], amount);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, (float)Projectile.height * 0.5f);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
			Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, position, null, color, Projectile.rotation, vector, Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation() + 1.57f;
		accel++;
		if (accel <= 85)
		{
			return;
		}
		if (Projectile.localAI[0] == 0f)
		{
			Projectile.localAI[0] = 1f;
		}
		Projectile.ai[0] -= 1f;
		if (Projectile.ai[0] > 0f)
		{
			float num = Projectile.velocity.Length();
			num += Projectile.ai[1];
			Projectile.velocity = Vector2.Normalize(Projectile.velocity) * num;
			return;
		}
		if (Projectile.ai[0] == 0f)
		{
			Projectile.ai[1] = (int)Player.FindClosest(Projectile.Center, 0, 0);
			if (Projectile.ai[1] != -1f && ((Entity)Main.player[(int)Projectile.ai[1]]).active && !Main.player[(int)Projectile.ai[1]].dead)
			{
				Projectile.velocity = Projectile.DirectionTo(Main.player[(int)Projectile.ai[1]].Center);
				Projectile.netUpdate = true;
			}
			else
			{
				Projectile.Kill();
			}
			return;
		}
		Projectile.localAI[1] += 1f;
		if (Projectile.localAI[1] < 90f)
		{
			Projectile.velocity *= 1.023f;
		}
		if (Projectile.localAI[1] < 120f)
		{
			float curAngle = Projectile.velocity.ToRotation();
			float targetAngle = (Main.player[(int)Projectile.ai[1]].Center - Projectile.Center).ToRotation();
			Projectile.velocity = new Vector2(Projectile.velocity.Length(), 0f).RotatedBy(curAngle.AngleLerp(targetAngle, 0.025f));
		}
	}
}
