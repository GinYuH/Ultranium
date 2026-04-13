using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent.Projectiles;

public class MotherPhantomBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 35;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		//DisplayName.SetDefault("Darkness Bolt");
	}

	public override void SetDefaults()
	{
		Projectile.aiStyle = 132;
		Projectile.width = 72;
		Projectile.height = 72;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.aiStyle = 0;
		Projectile.penetrate = 3;
		Projectile.extraUpdates = 1;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 420;
		Projectile.alpha = 125;
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

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation() + 1.57f;
		if (Projectile.localAI[0] == 0f)
		{
			Projectile.localAI[0] = 1f;
			SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
		}
		if ((Projectile.ai[0] -= 1f) > 0f)
		{
			float num = Projectile.velocity.Length();
			num += Projectile.ai[1];
			Projectile.velocity = Vector2.Normalize(Projectile.velocity) * num;
		}
		else if (Projectile.ai[0] == 0f)
		{
			Projectile.ai[1] = (int)Player.FindClosest(Projectile.Center, 0, 0);
			if (Projectile.ai[1] != -1f && ((Entity)Main.player[(int)Projectile.ai[1]]).active && !Main.player[(int)Projectile.ai[1]].dead)
			{
				Projectile.velocity = Projectile.DirectionTo(Main.player[(int)Projectile.ai[1]].Center);
				Projectile.netUpdate = true;
			}
		}
		else if (Projectile.localAI[1] < 300f)
		{
			float curAngle = Projectile.velocity.ToRotation();
			float targetAngle = (Main.player[(int)Projectile.ai[1]].Center - Projectile.Center).ToRotation();
			Projectile.velocity = new Vector2(Projectile.velocity.Length(), 0f).RotatedBy(curAngle.AngleLerp(targetAngle, 0.01f));
		}
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item14, new Vector2(Projectile.position.X, Projectile.position.Y));
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("ShadowDustPurple").Type, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != Projectile.Center)
			{
				Main.dust[num].velocity = Projectile.DirectionTo(Main.dust[num].position) * 10f;
			}
		}
		for (int j = 0; j < 40; j++)
		{
			int num2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("ShadowDustPurple").Type, 0f, -2f, 0, default(Color), 2f);
			Main.dust[num2].noGravity = true;
			Main.dust[num2].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num2].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num2].position != Projectile.Center)
			{
				Main.dust[num2].velocity = Projectile.DirectionTo(Main.dust[num2].position) * 3f;
			}
		}
	}
}
