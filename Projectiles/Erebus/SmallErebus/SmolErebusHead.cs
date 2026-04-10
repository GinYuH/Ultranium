using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.SmallErebus;

public class SmolErebusHead : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Erebus");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 28;
		((ModProjectile)this).projectile.height = 50;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.timeLeft *= 5;
		((ModProjectile)this).projectile.minion = true;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.netImportant = true;
		ProjectileID.Sets.MinionTargettingFeature[((ModProjectile)this).projectile.type] = true;
		((ModProjectile)this).projectile.usesIDStaticNPCImmunity = true;
		((ModProjectile)this).projectile.idStaticNPCHitCooldown = 20;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(((ModProjectile)this).projectile.localAI[0]);
		writer.Write(((ModProjectile)this).projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		((ModProjectile)this).projectile.localAI[0] = reader.ReadSingle();
		((ModProjectile)this).projectile.localAI[1] = reader.ReadSingle();
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Texture2D texture2D = Main.projectileTexture[((ModProjectile)this).projectile.type];
		int num = Main.projectileTexture[((ModProjectile)this).projectile.type].Height / Main.projFrames[((ModProjectile)this).projectile.type];
		Color color = Lighting.GetColor((int)(((ModProjectile)this).projectile.Center.X / 16f), (int)(((ModProjectile)this).projectile.Center.Y / 16f));
		int y = num * ((ModProjectile)this).projectile.frame;
		Main.spriteBatch.Draw(texture2D, ((ModProjectile)this).projectile.Center - Main.screenPosition + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY), new Rectangle(0, y, texture2D.Width, num), color, ((ModProjectile)this).projectile.rotation, new Vector2((float)texture2D.Width / 2f, (float)num / 2f), ((ModProjectile)this).projectile.scale, (((ModProjectile)this).projectile.spriteDirection != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
		return false;
	}

	public override void AI()
	{
		Player player = Main.player[((ModProjectile)this).projectile.owner];
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if ((int)Main.time % 120 == 0)
		{
			((ModProjectile)this).projectile.netUpdate = true;
		}
		if (!((Entity)player).active)
		{
			((Entity)((ModProjectile)this).projectile).active = false;
			return;
		}
		int num = 10;
		if (player.dead)
		{
			modPlayer.ErebusMinion = false;
		}
		if (modPlayer.ErebusMinion)
		{
			((ModProjectile)this).projectile.timeLeft = 2;
		}
		num = 30;
		Vector2 center = player.Center;
		float num2 = 1300f;
		float num3 = 1400f;
		int num4 = -1;
		if (((ModProjectile)this).projectile.Distance(center) > 2000f)
		{
			((ModProjectile)this).projectile.Center = center;
			((ModProjectile)this).projectile.netUpdate = true;
		}
		if (true)
		{
			NPC ownerMinionAttackTargetNPC = ((ModProjectile)this).projectile.OwnerMinionAttackTargetNPC;
			if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(((ModProjectile)this).projectile) && ((ModProjectile)this).projectile.Distance(ownerMinionAttackTargetNPC.Center) < num2 * 2f)
			{
				num4 = ownerMinionAttackTargetNPC.whoAmI;
				if (ownerMinionAttackTargetNPC.boss)
				{
					_ = ownerMinionAttackTargetNPC.whoAmI;
				}
				else
				{
					_ = ownerMinionAttackTargetNPC.whoAmI;
				}
			}
			if (num4 < 0)
			{
				for (int i = 0; i < 200; i++)
				{
					NPC nPC = Main.npc[i];
					if (nPC.CanBeChasedBy(((ModProjectile)this).projectile) && player.Distance(nPC.Center) < num3 && ((ModProjectile)this).projectile.Distance(nPC.Center) < num2)
					{
						num4 = i;
						_ = nPC.boss;
					}
				}
			}
		}
		if (num4 != -1)
		{
			NPC nPC2 = Main.npc[num4];
			Vector2 vector = nPC2.Center - ((ModProjectile)this).projectile.Center;
			(vector.X > 0f).ToDirectionInt();
			(vector.Y > 0f).ToDirectionInt();
			float num5 = 0.4f;
			if (vector.Length() < 600f)
			{
				num5 = 0.6f;
			}
			if (vector.Length() < 300f)
			{
				num5 = 0.8f;
			}
			if (vector.Length() > nPC2.Size.Length() * 0.75f)
			{
				((ModProjectile)this).projectile.velocity += Vector2.Normalize(vector) * num5 * 1.5f;
				if (Vector2.Dot(((ModProjectile)this).projectile.velocity, vector) < 0.25f)
				{
					((ModProjectile)this).projectile.velocity *= 0.8f;
				}
			}
			float num6 = 30f;
			if (((ModProjectile)this).projectile.velocity.Length() > num6)
			{
				((ModProjectile)this).projectile.velocity = Vector2.Normalize(((ModProjectile)this).projectile.velocity) * num6;
			}
		}
		else
		{
			float num7 = 0.2f;
			Vector2 vector2 = center - ((ModProjectile)this).projectile.Center;
			if (vector2.Length() < 200f)
			{
				num7 = 0.12f;
			}
			if (vector2.Length() < 140f)
			{
				num7 = 0.06f;
			}
			if (vector2.Length() > 100f)
			{
				if (Math.Abs(center.X - ((ModProjectile)this).projectile.Center.X) > 20f)
				{
					((ModProjectile)this).projectile.velocity.X = ((ModProjectile)this).projectile.velocity.X + num7 * (float)Math.Sign(center.X - ((ModProjectile)this).projectile.Center.X);
				}
				if (Math.Abs(center.Y - ((ModProjectile)this).projectile.Center.Y) > 10f)
				{
					((ModProjectile)this).projectile.velocity.Y = ((ModProjectile)this).projectile.velocity.Y + num7 * (float)Math.Sign(center.Y - ((ModProjectile)this).projectile.Center.Y);
				}
			}
			else if (((ModProjectile)this).projectile.velocity.Length() > 2f)
			{
				((ModProjectile)this).projectile.velocity *= 0.96f;
			}
			if (Math.Abs(((ModProjectile)this).projectile.velocity.Y) < 1f)
			{
				((ModProjectile)this).projectile.velocity.Y = ((ModProjectile)this).projectile.velocity.Y - 0.1f;
			}
			float num8 = 15f;
			if (((ModProjectile)this).projectile.velocity.Length() > num8)
			{
				((ModProjectile)this).projectile.velocity = Vector2.Normalize(((ModProjectile)this).projectile.velocity) * num8;
			}
		}
		((ModProjectile)this).projectile.rotation = ((ModProjectile)this).projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		int direction = ((ModProjectile)this).projectile.direction;
		((ModProjectile)this).projectile.direction = (((ModProjectile)this).projectile.spriteDirection = ((((ModProjectile)this).projectile.velocity.X > 0f) ? 1 : (-1)));
		if (direction != ((ModProjectile)this).projectile.direction)
		{
			((ModProjectile)this).projectile.netUpdate = true;
		}
		MathHelper.Clamp(((ModProjectile)this).projectile.localAI[0], 0f, 50f);
		((ModProjectile)this).projectile.position = ((ModProjectile)this).projectile.Center;
		((ModProjectile)this).projectile.scale = 0.9f;
		((ModProjectile)this).projectile.width = (((ModProjectile)this).projectile.height = (int)((float)num * ((ModProjectile)this).projectile.scale));
		((ModProjectile)this).projectile.Center = ((ModProjectile)this).projectile.position;
		if (((ModProjectile)this).projectile.alpha > 0)
		{
			((ModProjectile)this).projectile.alpha -= 42;
			if (((ModProjectile)this).projectile.alpha < 0)
			{
				((ModProjectile)this).projectile.alpha = 0;
			}
		}
		((ModProjectile)this).projectile.position -= ((ModProjectile)this).projectile.velocity / 2f;
	}
}
