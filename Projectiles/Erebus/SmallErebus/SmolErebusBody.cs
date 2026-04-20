using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.SmallErebus;

public class SmolErebusBody : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Erebus");
	}

	public override void SetDefaults()
	{
		Projectile.width = 28;
		Projectile.height = 32;
		Projectile.penetrate = -1;
		Projectile.timeLeft *= 5;
		Projectile.minion = true;
		Projectile.friendly = true;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		Projectile.alpha = 255;
		Projectile.netImportant = true;
		Projectile.minionSlots = 0.25f;
		Projectile.hide = true;
		Projectile.scale = 1f;
		Projectile.usesIDStaticNPCImmunity = true;
		Projectile.idStaticNPCHitCooldown = 20;
        Projectile.DamageType = DamageClass.Summon;
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

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindProjectiles.Add(index);
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
		if (!player.active)
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
		bool flag = false;
		Vector2 vector = Vector2.Zero;
		_ = Vector2.Zero;
		float num2 = 0f;
		if (Projectile.ai[1] == 1f)
		{
			Projectile.ai[1] = 0f;
			Projectile.netUpdate = true;
		}
		int byUUID = Projectile.GetByUUID(Projectile.owner, (int)Projectile.ai[0]);
		if (byUUID >= 0 && ((Entity)Main.projectile[byUUID]).active)
		{
			flag = true;
			vector = Main.projectile[byUUID].Center;
			_ = Main.projectile[byUUID].velocity;
			num2 = Main.projectile[byUUID].rotation;
			MathHelper.Clamp(Main.projectile[byUUID].scale, 0f, 50f);
			_ = Main.projectile[byUUID].alpha;
			Main.projectile[byUUID].localAI[0] = Projectile.localAI[0] + 1f;
			if (Main.projectile[byUUID].type != Mod.Find<ModProjectile>("SmolErebusHead").Type)
			{
				Main.projectile[byUUID].localAI[1] = Projectile.whoAmI;
			}
		}
		if (!flag)
		{
			return;
		}
		if (Projectile.alpha > 0)
		{
			for (int i = 0; i < 2; i++)
			{
				int num3 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemEmerald, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num3].noGravity = true;
				Main.dust[num3].noLight = true;
			}
		}
		Projectile.alpha -= 42;
		if (Projectile.alpha < 0)
		{
			Projectile.alpha = 0;
		}
		Projectile.velocity = Vector2.Zero;
		Vector2 vector2 = vector - Projectile.Center;
		if (num2 != Projectile.rotation)
		{
			float num4 = MathHelper.WrapAngle(num2 - Projectile.rotation);
			vector2 = vector2.RotatedBy(num4 * 0.1f);
		}
		Projectile.rotation = vector2.ToRotation() + (float)Math.PI / 2f;
		Projectile.position = Projectile.Center;
		Projectile.width = (Projectile.height = (int)((float)num * Projectile.scale));
		Projectile.Center = Projectile.position;
		float num5 = 26f;
		if (Main.projectile[byUUID].type == Mod.Find<ModProjectile>("SmolErebusHead").Type)
		{
			num5 = 32f;
		}
		if (vector2 != Vector2.Zero)
		{
			Projectile.Center = vector - Vector2.Normalize(vector2) * num5;
		}
		Projectile.spriteDirection = ((vector2.X > 0f) ? 1 : (-1));
	}

	public override void OnKill(int timeLeft)
	{
		Player player = Main.player[Projectile.owner];
		if (!(player.slotsMinions + Projectile.minionSlots > (float)player.maxMinions) || Projectile.owner != Main.myPlayer)
		{
			return;
		}
		int byUUID = Projectile.GetByUUID(Projectile.owner, Projectile.ai[0]);
		if (byUUID != -1)
		{
			Projectile projectile = Main.projectile[byUUID];
			if (projectile.type != Mod.Find<ModProjectile>("SmolErebusHead").Type)
			{
				projectile.localAI[1] = Projectile.localAI[1];
			}
			projectile = Main.projectile[(int)Projectile.localAI[1]];
			projectile.ai[0] = Projectile.ai[0];
			projectile.ai[1] = 1f;
			projectile.netUpdate = true;
		}
	}
}
