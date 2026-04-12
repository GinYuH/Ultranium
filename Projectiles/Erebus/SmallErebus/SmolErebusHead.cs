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
		DisplayName.SetDefault("Erebus");
	}

	public override void SetDefaults()
	{
		Projectile.width = 28;
		Projectile.height = 50;
		Projectile.penetrate = -1;
		Projectile.timeLeft *= 5;
		Projectile.minion = true;
		Projectile.friendly = true;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		Projectile.alpha = 255;
		Projectile.netImportant = true;
		ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		Projectile.usesIDStaticNPCImmunity = true;
		Projectile.idStaticNPCHitCooldown = 20;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(Projectile.localAI[0]);
		writer.Write(Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		Projectile.localAI[0] = reader.ReadSingle();
		Projectile.localAI[1] = reader.ReadSingle();
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
		int num = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
		Color color = Lighting.GetColor((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f));
		int y = num * Projectile.frame;
		Main.spriteBatch.Draw(texture2D, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle(0, y, texture2D.Width, num), color, Projectile.rotation, new Vector2((float)texture2D.Width / 2f, (float)num / 2f), Projectile.scale, (Projectile.spriteDirection != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
		return false;
	}

	public override void AI()
	{
		Player player = Main.player[Projectile.owner];
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if ((int)Main.time % 120 == 0)
		{
			Projectile.netUpdate = true;
		}
		if (!((Entity)player).active)
		{
			((Entity)Projectile).active = false;
			return;
		}
		int num = 10;
		if (player.dead)
		{
			modPlayer.ErebusMinion = false;
		}
		if (modPlayer.ErebusMinion)
		{
			Projectile.timeLeft = 2;
		}
		num = 30;
		Vector2 center = player.Center;
		float num2 = 1300f;
		float num3 = 1400f;
		int num4 = -1;
		if (Projectile.Distance(center) > 2000f)
		{
			Projectile.Center = center;
			Projectile.netUpdate = true;
		}
		if (true)
		{
			NPC ownerMinionAttackTargetNPC = Projectile.OwnerMinionAttackTargetNPC;
			if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(Projectile) && Projectile.Distance(ownerMinionAttackTargetNPC.Center) < num2 * 2f)
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
					if (nPC.CanBeChasedBy(Projectile) && player.Distance(nPC.Center) < num3 && Projectile.Distance(nPC.Center) < num2)
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
			Vector2 vector = nPC2.Center - Projectile.Center;
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
				Projectile.velocity += Vector2.Normalize(vector) * num5 * 1.5f;
				if (Vector2.Dot(Projectile.velocity, vector) < 0.25f)
				{
					Projectile.velocity *= 0.8f;
				}
			}
			float num6 = 30f;
			if (Projectile.velocity.Length() > num6)
			{
				Projectile.velocity = Vector2.Normalize(Projectile.velocity) * num6;
			}
		}
		else
		{
			float num7 = 0.2f;
			Vector2 vector2 = center - Projectile.Center;
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
				if (Math.Abs(center.X - Projectile.Center.X) > 20f)
				{
					Projectile.velocity.X = Projectile.velocity.X + num7 * (float)Math.Sign(center.X - Projectile.Center.X);
				}
				if (Math.Abs(center.Y - Projectile.Center.Y) > 10f)
				{
					Projectile.velocity.Y = Projectile.velocity.Y + num7 * (float)Math.Sign(center.Y - Projectile.Center.Y);
				}
			}
			else if (Projectile.velocity.Length() > 2f)
			{
				Projectile.velocity *= 0.96f;
			}
			if (Math.Abs(Projectile.velocity.Y) < 1f)
			{
				Projectile.velocity.Y = Projectile.velocity.Y - 0.1f;
			}
			float num8 = 15f;
			if (Projectile.velocity.Length() > num8)
			{
				Projectile.velocity = Vector2.Normalize(Projectile.velocity) * num8;
			}
		}
		Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		int direction = Projectile.direction;
		Projectile.direction = (Projectile.spriteDirection = ((Projectile.velocity.X > 0f) ? 1 : (-1)));
		if (direction != Projectile.direction)
		{
			Projectile.netUpdate = true;
		}
		MathHelper.Clamp(Projectile.localAI[0], 0f, 50f);
		Projectile.position = Projectile.Center;
		Projectile.scale = 0.9f;
		Projectile.width = (Projectile.height = (int)((float)num * Projectile.scale));
		Projectile.Center = Projectile.position;
		if (Projectile.alpha > 0)
		{
			Projectile.alpha -= 42;
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
		}
		Projectile.position -= Projectile.velocity / 2f;
	}
}
