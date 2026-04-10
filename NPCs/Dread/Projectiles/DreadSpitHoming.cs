using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
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
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		((ModProjectile)this).DisplayName.SetDefault("Dread Spit");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = (((ModProjectile)this).projectile.height = 16);
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.scale = 1.2f;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.timeLeft = 180;
	}

	public override void OnHitPlayer(Player player, int damage, bool crit)
	{
		player.AddBuff(((ModProjectile)this).mod.BuffType("DreadDebuff"), 180, fromNetPvP: true);
		((ModProjectile)this).projectile.Kill();
		Main.PlaySound(2, (int)((ModProjectile)this).projectile.position.X, (int)((ModProjectile)this).projectile.position.Y, 20, 1f, 0f);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/Dread/Projectiles/DreadSpitTrail").Width * 0.5f, (float)((ModProjectile)this).projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY);
			Color color = ((ModProjectile)this).projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).projectile.oldPos.Length - i) / (float)((ModProjectile)this).projectile.oldPos.Length);
			spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/Dread/Projectiles/DreadSpitTrail"), position, null, color, ((ModProjectile)this).projectile.rotation, vector, ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		if (Utils.NextBool(Main.rand))
		{
			Dust dust = Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 90);
			dust.noGravity = true;
			dust.scale = 1f;
		}
	}

	public override bool PreAI()
	{
		((ModProjectile)this).projectile.rotation += 0.35f * (float)((ModProjectile)this).projectile.direction;
		HomingTimer++;
		if (HomingTimer <= 120)
		{
			if (((ModProjectile)this).projectile.ai[0] == 0f && Main.netMode != 1)
			{
				target = -1;
				float num = 2000f;
				for (int i = 0; i < 255; i++)
				{
					if (((Entity)Main.player[i]).active && !Main.player[i].dead)
					{
						float num2 = Vector2.Distance(Main.player[i].Center, ((ModProjectile)this).projectile.Center);
						if (num2 < num || target == -1)
						{
							num = num2;
							target = i;
						}
					}
				}
				if (target != -1)
				{
					((ModProjectile)this).projectile.ai[0] = 1f;
					((ModProjectile)this).projectile.netUpdate = true;
				}
			}
			else
			{
				Player player = Main.player[target];
				if (!((Entity)player).active || player.dead)
				{
					target = -1;
					((ModProjectile)this).projectile.ai[0] = 0f;
					((ModProjectile)this).projectile.netUpdate = true;
				}
				else
				{
					float num3 = ((ModProjectile)this).projectile.velocity.ToRotation();
					Vector2 vector = player.Center - ((ModProjectile)this).projectile.Center;
					float targetAngle = vector.ToRotation();
					if (vector == Vector2.Zero)
					{
						targetAngle = num3;
					}
					float num4 = num3.AngleLerp(targetAngle, 0.1f);
					((ModProjectile)this).projectile.velocity = new Vector2(((ModProjectile)this).projectile.velocity.Length(), 0f).RotatedBy(num4);
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

	public override void Kill(int timeLeft)
	{
		Main.PlaySound(2, (int)((ModProjectile)this).projectile.position.X, (int)((ModProjectile)this).projectile.position.Y, 14, 1f, 0f);
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 90, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}
}
