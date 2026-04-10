using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.SmallErebus;

public class SmolErebusHead : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Erebus");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 28;
		((ModProjectile)this).Projectile.height = 50;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.timeLeft *= 5;
		((ModProjectile)this).Projectile.minion = true;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.alpha = 255;
		((ModProjectile)this).Projectile.netImportant = true;
		ProjectileID.Sets.MinionTargettingFeature[((ModProjectile)this).Projectile.type] = true;
		((ModProjectile)this).Projectile.usesIDStaticNPCImmunity = true;
		((ModProjectile)this).Projectile.idStaticNPCHitCooldown = 20;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(((ModProjectile)this).Projectile.localAI[0]);
		writer.Write(((ModProjectile)this).Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		((ModProjectile)this).Projectile.localAI[0] = reader.ReadSingle();
		((ModProjectile)this).Projectile.localAI[1] = reader.ReadSingle();
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D = TextureAssets.Projectile[((ModProjectile)this).Projectile.type].Value;
		int num = TextureAssets.Projectile[((ModProjectile)this).Projectile.type].Value.Height / Main.projFrames[((ModProjectile)this).Projectile.type];
		Color color = Lighting.GetColor((int)(((ModProjectile)this).Projectile.Center.X / 16f), (int)(((ModProjectile)this).Projectile.Center.Y / 16f));
		int y = num * ((ModProjectile)this).Projectile.frame;
		Main.spriteBatch.Draw(texture2D, ((ModProjectile)this).Projectile.Center - Main.screenPosition + new Vector2(0f, ((ModProjectile)this).Projectile.gfxOffY), new Rectangle(0, y, texture2D.Width, num), color, ((ModProjectile)this).Projectile.rotation, new Vector2((float)texture2D.Width / 2f, (float)num / 2f), ((ModProjectile)this).Projectile.scale, (((ModProjectile)this).Projectile.spriteDirection != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
		return false;
	}

	public override void AI()
	{
		Player player = Main.player[((ModProjectile)this).Projectile.owner];
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if ((int)Main.time % 120 == 0)
		{
			((ModProjectile)this).Projectile.netUpdate = true;
		}
		if (!((Entity)player).active)
		{
			((Entity)((ModProjectile)this).Projectile).active = false;
			return;
		}
		int num = 10;
		if (player.dead)
		{
			modPlayer.ErebusMinion = false;
		}
		if (modPlayer.ErebusMinion)
		{
			((ModProjectile)this).Projectile.timeLeft = 2;
		}
		num = 30;
		Vector2 center = player.Center;
		float num2 = 1300f;
		float num3 = 1400f;
		int num4 = -1;
		if (((ModProjectile)this).Projectile.Distance(center) > 2000f)
		{
			((ModProjectile)this).Projectile.Center = center;
			((ModProjectile)this).Projectile.netUpdate = true;
		}
		if (true)
		{
			NPC ownerMinionAttackTargetNPC = ((ModProjectile)this).Projectile.OwnerMinionAttackTargetNPC;
			if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(((ModProjectile)this).Projectile) && ((ModProjectile)this).Projectile.Distance(ownerMinionAttackTargetNPC.Center) < num2 * 2f)
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
					if (nPC.CanBeChasedBy(((ModProjectile)this).Projectile) && player.Distance(nPC.Center) < num3 && ((ModProjectile)this).Projectile.Distance(nPC.Center) < num2)
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
			Vector2 vector = nPC2.Center - ((ModProjectile)this).Projectile.Center;
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
				((ModProjectile)this).Projectile.velocity += Vector2.Normalize(vector) * num5 * 1.5f;
				if (Vector2.Dot(((ModProjectile)this).Projectile.velocity, vector) < 0.25f)
				{
					((ModProjectile)this).Projectile.velocity *= 0.8f;
				}
			}
			float num6 = 30f;
			if (((ModProjectile)this).Projectile.velocity.Length() > num6)
			{
				((ModProjectile)this).Projectile.velocity = Vector2.Normalize(((ModProjectile)this).Projectile.velocity) * num6;
			}
		}
		else
		{
			float num7 = 0.2f;
			Vector2 vector2 = center - ((ModProjectile)this).Projectile.Center;
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
				if (Math.Abs(center.X - ((ModProjectile)this).Projectile.Center.X) > 20f)
				{
					((ModProjectile)this).Projectile.velocity.X = ((ModProjectile)this).Projectile.velocity.X + num7 * (float)Math.Sign(center.X - ((ModProjectile)this).Projectile.Center.X);
				}
				if (Math.Abs(center.Y - ((ModProjectile)this).Projectile.Center.Y) > 10f)
				{
					((ModProjectile)this).Projectile.velocity.Y = ((ModProjectile)this).Projectile.velocity.Y + num7 * (float)Math.Sign(center.Y - ((ModProjectile)this).Projectile.Center.Y);
				}
			}
			else if (((ModProjectile)this).Projectile.velocity.Length() > 2f)
			{
				((ModProjectile)this).Projectile.velocity *= 0.96f;
			}
			if (Math.Abs(((ModProjectile)this).Projectile.velocity.Y) < 1f)
			{
				((ModProjectile)this).Projectile.velocity.Y = ((ModProjectile)this).Projectile.velocity.Y - 0.1f;
			}
			float num8 = 15f;
			if (((ModProjectile)this).Projectile.velocity.Length() > num8)
			{
				((ModProjectile)this).Projectile.velocity = Vector2.Normalize(((ModProjectile)this).Projectile.velocity) * num8;
			}
		}
		((ModProjectile)this).Projectile.rotation = ((ModProjectile)this).Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		int direction = ((ModProjectile)this).Projectile.direction;
		((ModProjectile)this).Projectile.direction = (((ModProjectile)this).Projectile.spriteDirection = ((((ModProjectile)this).Projectile.velocity.X > 0f) ? 1 : (-1)));
		if (direction != ((ModProjectile)this).Projectile.direction)
		{
			((ModProjectile)this).Projectile.netUpdate = true;
		}
		MathHelper.Clamp(((ModProjectile)this).Projectile.localAI[0], 0f, 50f);
		((ModProjectile)this).Projectile.position = ((ModProjectile)this).Projectile.Center;
		((ModProjectile)this).Projectile.scale = 0.9f;
		((ModProjectile)this).Projectile.width = (((ModProjectile)this).Projectile.height = (int)((float)num * ((ModProjectile)this).Projectile.scale));
		((ModProjectile)this).Projectile.Center = ((ModProjectile)this).Projectile.position;
		if (((ModProjectile)this).Projectile.alpha > 0)
		{
			((ModProjectile)this).Projectile.alpha -= 42;
			if (((ModProjectile)this).Projectile.alpha < 0)
			{
				((ModProjectile)this).Projectile.alpha = 0;
			}
		}
		((ModProjectile)this).Projectile.position -= ((ModProjectile)this).Projectile.velocity / 2f;
	}
}
