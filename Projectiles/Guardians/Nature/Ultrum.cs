using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Guardians.Nature;

public class Ultrum : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Ultrum");
		Main.projFrames[((ModProjectile)this).projectile.type] = 4;
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 4;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		ProjectileID.Sets.MinionSacrificable[((ModProjectile)this).projectile.type] = true;
		ProjectileID.Sets.Homing[((ModProjectile)this).projectile.type] = true;
		ProjectileID.Sets.MinionTargettingFeature[((ModProjectile)this).projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 32;
		((ModProjectile)this).projectile.height = 32;
		Main.projPet[((ModProjectile)this).projectile.type] = true;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.minion = true;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.aiStyle = -1;
		((ModProjectile)this).projectile.timeLeft = 18000;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.netImportant = true;
	}

	public override bool PreDraw(SpriteBatch sb, Color lightColor)
	{
		((ModProjectile)this).projectile.frameCounter++;
		if (((ModProjectile)this).projectile.frameCounter >= 9)
		{
			((ModProjectile)this).projectile.frame++;
			((ModProjectile)this).projectile.frameCounter = 0;
			if (((ModProjectile)this).projectile.frame >= 4)
			{
				((ModProjectile)this).projectile.frame = 0;
			}
		}
		Texture2D texture2D = Main.projectileTexture[((ModProjectile)this).projectile.type];
		Vector2 vector = new Vector2((float)texture2D.Width * 0.5f, (float)((ModProjectile)this).projectile.height * 0.5f);
		SpriteEffects effects = ((((ModProjectile)this).projectile.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
		for (int i = 0; i < ((ModProjectile)this).projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY);
			Color color = ((ModProjectile)this).projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).projectile.oldPos.Length - i) / (float)((ModProjectile)this).projectile.oldPos.Length);
			Rectangle value = new Rectangle(0, texture2D.Height / Main.projFrames[((ModProjectile)this).projectile.type] * ((ModProjectile)this).projectile.frame, texture2D.Width, texture2D.Height / Main.projFrames[((ModProjectile)this).projectile.type]);
			sb.Draw(texture2D, position, value, color, ((ModProjectile)this).projectile.rotation, vector, ((ModProjectile)this).projectile.scale, effects, 0f);
		}
		return true;
	}

	public override void AI()
	{
		bool num = ((ModProjectile)this).projectile.type == ((ModProjectile)this).mod.ProjectileType("Ultrum");
		Player player = Main.player[((ModProjectile)this).projectile.owner];
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (num)
		{
			if (player.dead)
			{
				modPlayer.UltrumMinion = false;
			}
			if (modPlayer.UltrumMinion)
			{
				((ModProjectile)this).projectile.timeLeft = 2;
			}
		}
		float num2 = (float)((ModProjectile)this).projectile.width * 1.1f;
		for (int i = 0; i < 1000; i++)
		{
			Projectile projectile = Main.projectile[i];
			if (i != projectile.whoAmI && ((Entity)projectile).active && ((ModProjectile)this).projectile.owner == projectile.owner && ((ModProjectile)this).projectile.type == projectile.type && Math.Abs(((ModProjectile)this).projectile.position.X - projectile.position.X) + Math.Abs(((ModProjectile)this).projectile.position.Y - projectile.position.Y) < num2)
			{
				if (((ModProjectile)this).projectile.position.X < Main.projectile[i].position.X)
				{
					((ModProjectile)this).projectile.velocity.X -= 0.08f;
				}
				else
				{
					((ModProjectile)this).projectile.velocity.X += 0.08f;
				}
				if (((ModProjectile)this).projectile.position.Y < Main.projectile[i].position.Y)
				{
					((ModProjectile)this).projectile.velocity.Y -= 0.08f;
				}
				else
				{
					((ModProjectile)this).projectile.velocity.Y += 0.08f;
				}
			}
		}
		Vector2 vector = ((ModProjectile)this).projectile.position;
		float num3 = 500f;
		bool flag = false;
		((ModProjectile)this).projectile.tileCollide = true;
		for (int j = 0; j < 200; j++)
		{
			NPC nPC = Main.npc[j];
			if (nPC.CanBeChasedBy(this))
			{
				float num4 = Vector2.Distance(nPC.Center, ((ModProjectile)this).projectile.Center);
				if ((num4 < num3 || !flag) && Collision.CanHitLine(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, nPC.position, nPC.width, nPC.height))
				{
					num3 = num4;
					vector = nPC.Center;
					flag = true;
				}
			}
		}
		if (Vector2.Distance(player.Center, ((ModProjectile)this).projectile.Center) > (flag ? 1000f : 500f))
		{
			((ModProjectile)this).projectile.ai[0] = 1f;
			((ModProjectile)this).projectile.netUpdate = true;
		}
		if (((ModProjectile)this).projectile.ai[0] == 1f)
		{
			((ModProjectile)this).projectile.tileCollide = false;
		}
		if (flag && ((ModProjectile)this).projectile.ai[0] == 0f)
		{
			Vector2 vector2 = vector - ((ModProjectile)this).projectile.Center;
			if (vector2.Length() > 200f)
			{
				vector2.Normalize();
				((ModProjectile)this).projectile.velocity = (((ModProjectile)this).projectile.velocity * 20f + vector2 * 6f) / 21f;
			}
			else
			{
				((ModProjectile)this).projectile.velocity *= (float)Math.Pow(0.97, 2.0);
			}
		}
		else
		{
			if (!Collision.CanHitLine(((ModProjectile)this).projectile.Center, 1, 1, player.Center, 1, 1))
			{
				((ModProjectile)this).projectile.ai[0] = 1f;
			}
			float num5 = 6f;
			if (((ModProjectile)this).projectile.ai[0] == 1f)
			{
				num5 = 15f;
			}
			Vector2 center = ((ModProjectile)this).projectile.Center;
			Vector2 vector3 = player.Center - center;
			((ModProjectile)this).projectile.ai[1] = 3600f;
			((ModProjectile)this).projectile.netUpdate = true;
			int num6 = 1;
			for (int k = 0; k < ((ModProjectile)this).projectile.whoAmI; k++)
			{
				if (((Entity)Main.projectile[k]).active && Main.projectile[k].owner == ((ModProjectile)this).projectile.owner && Main.projectile[k].type == ((ModProjectile)this).projectile.type)
				{
					num6++;
				}
			}
			vector3.X -= (10 + num6 * 40) * player.direction;
			vector3.Y -= 70f;
			float num7 = vector3.Length();
			if (num7 > 200f && num5 < 9f)
			{
				num5 = 9f;
			}
			if (num7 < 100f && ((ModProjectile)this).projectile.ai[0] == 1f && !Collision.SolidCollision(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height))
			{
				((ModProjectile)this).projectile.ai[0] = 0f;
				((ModProjectile)this).projectile.netUpdate = true;
			}
			if (num7 > 2000f)
			{
				((ModProjectile)this).projectile.Center = player.Center;
			}
			if (num7 > 48f)
			{
				vector3.Normalize();
				vector3 *= num5;
				float num8 = 10f;
				((ModProjectile)this).projectile.velocity = (((ModProjectile)this).projectile.velocity * num8 + vector3) / (num8 + 1f);
			}
			else
			{
				((ModProjectile)this).projectile.direction = Main.player[((ModProjectile)this).projectile.owner].direction;
				((ModProjectile)this).projectile.velocity *= (float)Math.Pow(0.9, 2.0);
			}
		}
		((ModProjectile)this).projectile.rotation = ((ModProjectile)this).projectile.velocity.X * 0.05f;
		if (((ModProjectile)this).projectile.velocity.X > 0f)
		{
			((ModProjectile)this).projectile.spriteDirection = (((ModProjectile)this).projectile.direction = -1);
		}
		else if (((ModProjectile)this).projectile.velocity.X < 0f)
		{
			((ModProjectile)this).projectile.spriteDirection = (((ModProjectile)this).projectile.direction = 1);
		}
		if (((ModProjectile)this).projectile.ai[1] > 0f)
		{
			((ModProjectile)this).projectile.ai[1] += 1f;
		}
		if (((ModProjectile)this).projectile.ai[1] > 45f)
		{
			((ModProjectile)this).projectile.ai[1] = 0f;
			((ModProjectile)this).projectile.netUpdate = true;
		}
		if (!(((ModProjectile)this).projectile.ai[0] == 0f && flag))
		{
			return;
		}
		if ((vector - ((ModProjectile)this).projectile.Center).X > 0f)
		{
			((ModProjectile)this).projectile.spriteDirection = (((ModProjectile)this).projectile.direction = -1);
		}
		else if ((vector - ((ModProjectile)this).projectile.Center).X < 0f)
		{
			((ModProjectile)this).projectile.spriteDirection = (((ModProjectile)this).projectile.direction = 1);
		}
		if (((ModProjectile)this).projectile.ai[1] != 0f)
		{
			return;
		}
		((ModProjectile)this).projectile.ai[1] = 1f;
		if (Main.myPlayer == ((ModProjectile)this).projectile.owner)
		{
			Vector2 vector4 = vector - ((ModProjectile)this).projectile.Center;
			if (vector4 == Vector2.Zero)
			{
				vector4 = new Vector2(0f, 1f);
			}
			vector4.Normalize();
			vector4 *= 8.5f;
			int num9 = Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, vector4.X, vector4.Y, ((ModProjectile)this).mod.ProjectileType("NatureBlast"), ((ModProjectile)this).projectile.damage, ((ModProjectile)this).projectile.knockBack, Main.myPlayer, 0f, 0f);
			Main.projectile[num9].timeLeft = 300;
			Main.projectile[num9].penetrate = 1;
			Main.projectile[num9].netUpdate = true;
			Main.projectile[num9].minion = true;
			((ModProjectile)this).projectile.netUpdate = true;
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}
}
