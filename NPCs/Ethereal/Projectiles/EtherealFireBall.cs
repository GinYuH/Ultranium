using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ethereal.Projectiles;

public class EtherealFireBall : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Ethereal Fireball");
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1.2f;
		Projectile.width = 18;
		Projectile.height = 18;
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.alpha = 100;
		Projectile.hostile = true;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 180;
		base.CooldownSlot = 1;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.Request<Texture2D>("Ultranium/NPCs/Ethereal/Projectiles/EtherealFireBallTrail").Width() * 0.5f, (float)Projectile.height * 0.5f);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
            Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/NPCs/Ethereal/Projectiles/EtherealFireBallTrail").Value, position, null, color, Projectile.rotation, vector, Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
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
			else
			{
				Projectile.Kill();
			}
		}
		else
		{
			Projectile.tileCollide = true;
			float curAngle = Projectile.velocity.ToRotation();
			float targetAngle = (Main.player[(int)Projectile.ai[1]].Center - Projectile.Center).ToRotation();
			Projectile.velocity = new Vector2(Projectile.velocity.Length(), 0f).RotatedBy(curAngle.AngleLerp(targetAngle, 0.025f));
		}
		Projectile.rotation += 0.2f;
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 62, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != Projectile.Center)
			{
				Main.dust[num].velocity = Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(Mod.Find<ModBuff>("ShadowflameDebuff").Type, 120, quiet: false);
	}
}
