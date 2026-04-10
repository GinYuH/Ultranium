using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.SmallErebus;

public class SmolErebusBody : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Erebus");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 28;
		((ModProjectile)this).Projectile.height = 32;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.timeLeft *= 5;
		((ModProjectile)this).Projectile.minion = true;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.alpha = 255;
		((ModProjectile)this).Projectile.netImportant = true;
		((ModProjectile)this).Projectile.minionSlots = 0.25f;
		((ModProjectile)this).Projectile.hide = true;
		((ModProjectile)this).Projectile.scale = 1f;
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

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		drawCacheProjsBehindProjectiles.Add(index);
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
		bool flag = false;
		Vector2 vector = Vector2.Zero;
		_ = Vector2.Zero;
		float num2 = 0f;
		if (((ModProjectile)this).Projectile.ai[1] == 1f)
		{
			((ModProjectile)this).Projectile.ai[1] = 0f;
			((ModProjectile)this).Projectile.netUpdate = true;
		}
		int byUUID = Projectile.GetByUUID(((ModProjectile)this).Projectile.owner, (int)((ModProjectile)this).Projectile.ai[0]);
		if (byUUID >= 0 && ((Entity)Main.projectile[byUUID]).active)
		{
			flag = true;
			vector = Main.projectile[byUUID].Center;
			_ = Main.projectile[byUUID].velocity;
			num2 = Main.projectile[byUUID].rotation;
			MathHelper.Clamp(Main.projectile[byUUID].scale, 0f, 50f);
			_ = Main.projectile[byUUID].alpha;
			Main.projectile[byUUID].localAI[0] = ((ModProjectile)this).Projectile.localAI[0] + 1f;
			if (Main.projectile[byUUID].type != ((ModProjectile)this).Mod.Find<ModProjectile>("SmolErebusHead").Type)
			{
				Main.projectile[byUUID].localAI[1] = ((ModProjectile)this).Projectile.whoAmI;
			}
		}
		if (!flag)
		{
			return;
		}
		if (((ModProjectile)this).Projectile.alpha > 0)
		{
			for (int i = 0; i < 2; i++)
			{
				int num3 = Dust.NewDust(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 89, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num3].noGravity = true;
				Main.dust[num3].noLight = true;
			}
		}
		((ModProjectile)this).Projectile.alpha -= 42;
		if (((ModProjectile)this).Projectile.alpha < 0)
		{
			((ModProjectile)this).Projectile.alpha = 0;
		}
		((ModProjectile)this).Projectile.velocity = Vector2.Zero;
		Vector2 vector2 = vector - ((ModProjectile)this).Projectile.Center;
		if (num2 != ((ModProjectile)this).Projectile.rotation)
		{
			float num4 = MathHelper.WrapAngle(num2 - ((ModProjectile)this).Projectile.rotation);
			vector2 = vector2.RotatedBy(num4 * 0.1f);
		}
		((ModProjectile)this).Projectile.rotation = vector2.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).Projectile.position = ((ModProjectile)this).Projectile.Center;
		((ModProjectile)this).Projectile.width = (((ModProjectile)this).Projectile.height = (int)((float)num * ((ModProjectile)this).Projectile.scale));
		((ModProjectile)this).Projectile.Center = ((ModProjectile)this).Projectile.position;
		float num5 = 26f;
		if (Main.projectile[byUUID].type == ((ModProjectile)this).Mod.Find<ModProjectile>("SmolErebusHead").Type)
		{
			num5 = 32f;
		}
		if (vector2 != Vector2.Zero)
		{
			((ModProjectile)this).Projectile.Center = vector - Vector2.Normalize(vector2) * num5;
		}
		((ModProjectile)this).Projectile.spriteDirection = ((vector2.X > 0f) ? 1 : (-1));
	}

	public override void OnKill(int timeLeft)
	{
		Player player = Main.player[((ModProjectile)this).Projectile.owner];
		if (!(player.slotsMinions + ((ModProjectile)this).Projectile.minionSlots > (float)player.maxMinions) || ((ModProjectile)this).Projectile.owner != Main.myPlayer)
		{
			return;
		}
		int byUUID = Projectile.GetByUUID(((ModProjectile)this).Projectile.owner, ((ModProjectile)this).Projectile.ai[0]);
		if (byUUID != -1)
		{
			Projectile projectile = Main.projectile[byUUID];
			if (projectile.type != ((ModProjectile)this).Mod.Find<ModProjectile>("SmolErebusHead").Type)
			{
				projectile.localAI[1] = ((ModProjectile)this).Projectile.localAI[1];
			}
			projectile = Main.projectile[(int)((ModProjectile)this).Projectile.localAI[1]];
			projectile.ai[0] = ((ModProjectile)this).Projectile.ai[0];
			projectile.ai[1] = 1f;
			projectile.netUpdate = true;
		}
	}
}
