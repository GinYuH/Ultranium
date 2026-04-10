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
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
		// ((ModProjectile)this).DisplayName.SetDefault("Dread Spit");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = (((ModProjectile)this).Projectile.height = 16);
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.scale = 1.2f;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.timeLeft = 180;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		player.AddBuff(((ModProjectile)this).Mod.Find<ModBuff>("DreadDebuff").Type, 180, fromNetPvP: true);
		((ModProjectile)this).Projectile.Kill();
		SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y));
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/Dread/Projectiles/DreadSpitTrail").Width * 0.5f, (float)((ModProjectile)this).Projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).Projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).Projectile.gfxOffY);
			Color color = ((ModProjectile)this).Projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).Projectile.oldPos.Length - i) / (float)((ModProjectile)this).Projectile.oldPos.Length);
			spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/Dread/Projectiles/DreadSpitTrail"), position, null, color, ((ModProjectile)this).Projectile.rotation, vector, ((ModProjectile)this).Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		if (Utils.NextBool(Main.rand))
		{
			Dust dust = Dust.NewDustDirect(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 90);
			dust.noGravity = true;
			dust.scale = 1f;
		}
	}

	public override bool PreAI()
	{
		((ModProjectile)this).Projectile.rotation += 0.35f * (float)((ModProjectile)this).Projectile.direction;
		HomingTimer++;
		if (HomingTimer <= 120)
		{
			if (((ModProjectile)this).Projectile.ai[0] == 0f && Main.netMode != 1)
			{
				target = -1;
				float num = 2000f;
				for (int i = 0; i < 255; i++)
				{
					if (((Entity)Main.player[i]).active && !Main.player[i].dead)
					{
						float num2 = Vector2.Distance(Main.player[i].Center, ((ModProjectile)this).Projectile.Center);
						if (num2 < num || target == -1)
						{
							num = num2;
							target = i;
						}
					}
				}
				if (target != -1)
				{
					((ModProjectile)this).Projectile.ai[0] = 1f;
					((ModProjectile)this).Projectile.netUpdate = true;
				}
			}
			else
			{
				Player player = Main.player[target];
				if (!((Entity)player).active || player.dead)
				{
					target = -1;
					((ModProjectile)this).Projectile.ai[0] = 0f;
					((ModProjectile)this).Projectile.netUpdate = true;
				}
				else
				{
					float num3 = ((ModProjectile)this).Projectile.velocity.ToRotation();
					Vector2 vector = player.Center - ((ModProjectile)this).Projectile.Center;
					float targetAngle = vector.ToRotation();
					if (vector == Vector2.Zero)
					{
						targetAngle = num3;
					}
					float num4 = num3.AngleLerp(targetAngle, 0.1f);
					((ModProjectile)this).Projectile.velocity = new Vector2(((ModProjectile)this).Projectile.velocity.Length(), 0f).RotatedBy(num4);
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
		SoundEngine.PlaySound(SoundID.Item14, new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y));
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 90, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).Projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}
}
