using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Dread.Projectiles;

public class DreadSpitHoming : ModProjectile
{
	private int target;

	private int HomingTimer;

	public override string Texture => "Ultranium/NPCs/Dread/Projectiles/DreadSpit";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		// DisplayName.SetDefault("Dread Spit");
	}

	public override void SetDefaults()
	{
		Projectile.width = (Projectile.height = 16);
		Projectile.hostile = true;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		Projectile.scale = 1.2f;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 180;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(Mod.Find<ModBuff>("DreadDebuff").Type, 180, quiet: false);
		Projectile.Kill();
		SoundEngine.PlaySound(SoundID.Item20, new Vector2(Projectile.position.X, Projectile.position.Y));
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.Request<Texture2D>("Ultranium/NPCs/Dread/Projectiles/DreadSpitTrail").Width() * 0.5f, (float)Projectile.height * 0.5f);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
            Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/NPCs/Dread/Projectiles/DreadSpitTrail").Value, position, null, color, Projectile.rotation, vector, Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		if (Utils.NextBool(Main.rand))
		{
			Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 90);
			dust.noGravity = true;
			dust.scale = 1f;
		}
	}

	public override bool PreAI()
	{
		Projectile.rotation += 0.35f * (float)Projectile.direction;
		HomingTimer++;
		if (HomingTimer <= 120)
		{
			if (Projectile.ai[0] == 0f && Main.netMode != 1)
			{
				target = -1;
				float num = 2000f;
				for (int i = 0; i < 255; i++)
				{
					if (((Entity)Main.player[i]).active && !Main.player[i].dead)
					{
						float num2 = Vector2.Distance(Main.player[i].Center, Projectile.Center);
						if (num2 < num || target == -1)
						{
							num = num2;
							target = i;
						}
					}
				}
				if (target != -1)
				{
					Projectile.ai[0] = 1f;
					Projectile.netUpdate = true;
				}
			}
			else
			{
				Player player = Main.player[target];
				if (!((Entity)player).active || player.dead)
				{
					target = -1;
					Projectile.ai[0] = 0f;
					Projectile.netUpdate = true;
				}
				else
				{
					float num3 = Projectile.velocity.ToRotation();
					Vector2 vector = player.Center - Projectile.Center;
					float targetAngle = vector.ToRotation();
					if (vector == Vector2.Zero)
					{
						targetAngle = num3;
					}
					float num4 = num3.AngleLerp(targetAngle, 0.1f);
					Projectile.velocity = new Vector2(Projectile.velocity.Length(), 0f).RotatedBy(num4);
				}
			}
		}
		return false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(target);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		target = reader.Read();
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item14, new Vector2(Projectile.position.X, Projectile.position.Y));
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 90, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != Projectile.Center)
			{
				Main.dust[num].velocity = Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}
}
