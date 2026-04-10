using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.SmallErebus;

public class SmolErebusBody : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Erebus");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 28;
		((ModProjectile)this).projectile.height = 32;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.timeLeft *= 5;
		((ModProjectile)this).projectile.minion = true;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.netImportant = true;
		((ModProjectile)this).projectile.minionSlots = 0.25f;
		((ModProjectile)this).projectile.hide = true;
		((ModProjectile)this).projectile.scale = 1f;
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

	public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
	{
		drawCacheProjsBehindProjectiles.Add(index);
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
		bool flag = false;
		Vector2 vector = Vector2.Zero;
		_ = Vector2.Zero;
		float num2 = 0f;
		if (((ModProjectile)this).projectile.ai[1] == 1f)
		{
			((ModProjectile)this).projectile.ai[1] = 0f;
			((ModProjectile)this).projectile.netUpdate = true;
		}
		int byUUID = Projectile.GetByUUID(((ModProjectile)this).projectile.owner, (int)((ModProjectile)this).projectile.ai[0]);
		if (byUUID >= 0 && ((Entity)Main.projectile[byUUID]).active)
		{
			flag = true;
			vector = Main.projectile[byUUID].Center;
			_ = Main.projectile[byUUID].velocity;
			num2 = Main.projectile[byUUID].rotation;
			MathHelper.Clamp(Main.projectile[byUUID].scale, 0f, 50f);
			_ = Main.projectile[byUUID].alpha;
			Main.projectile[byUUID].localAI[0] = ((ModProjectile)this).projectile.localAI[0] + 1f;
			if (Main.projectile[byUUID].type != ((ModProjectile)this).mod.ProjectileType("SmolErebusHead"))
			{
				Main.projectile[byUUID].localAI[1] = ((ModProjectile)this).projectile.whoAmI;
			}
		}
		if (!flag)
		{
			return;
		}
		if (((ModProjectile)this).projectile.alpha > 0)
		{
			for (int i = 0; i < 2; i++)
			{
				int num3 = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 89, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num3].noGravity = true;
				Main.dust[num3].noLight = true;
			}
		}
		((ModProjectile)this).projectile.alpha -= 42;
		if (((ModProjectile)this).projectile.alpha < 0)
		{
			((ModProjectile)this).projectile.alpha = 0;
		}
		((ModProjectile)this).projectile.velocity = Vector2.Zero;
		Vector2 vector2 = vector - ((ModProjectile)this).projectile.Center;
		if (num2 != ((ModProjectile)this).projectile.rotation)
		{
			float num4 = MathHelper.WrapAngle(num2 - ((ModProjectile)this).projectile.rotation);
			vector2 = vector2.RotatedBy(num4 * 0.1f);
		}
		((ModProjectile)this).projectile.rotation = vector2.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).projectile.position = ((ModProjectile)this).projectile.Center;
		((ModProjectile)this).projectile.width = (((ModProjectile)this).projectile.height = (int)((float)num * ((ModProjectile)this).projectile.scale));
		((ModProjectile)this).projectile.Center = ((ModProjectile)this).projectile.position;
		float num5 = 26f;
		if (Main.projectile[byUUID].type == ((ModProjectile)this).mod.ProjectileType("SmolErebusHead"))
		{
			num5 = 32f;
		}
		if (vector2 != Vector2.Zero)
		{
			((ModProjectile)this).projectile.Center = vector - Vector2.Normalize(vector2) * num5;
		}
		((ModProjectile)this).projectile.spriteDirection = ((vector2.X > 0f) ? 1 : (-1));
	}

	public override void Kill(int timeLeft)
	{
		Player player = Main.player[((ModProjectile)this).projectile.owner];
		if (!(player.slotsMinions + ((ModProjectile)this).projectile.minionSlots > (float)player.maxMinions) || ((ModProjectile)this).projectile.owner != Main.myPlayer)
		{
			return;
		}
		int byUUID = Projectile.GetByUUID(((ModProjectile)this).projectile.owner, ((ModProjectile)this).projectile.ai[0]);
		if (byUUID != -1)
		{
			Projectile projectile = Main.projectile[byUUID];
			if (projectile.type != ((ModProjectile)this).mod.ProjectileType("SmolErebusHead"))
			{
				projectile.localAI[1] = ((ModProjectile)this).projectile.localAI[1];
			}
			projectile = Main.projectile[(int)((ModProjectile)this).projectile.localAI[1]];
			projectile.ai[0] = ((ModProjectile)this).projectile.ai[0];
			projectile.ai[1] = 1f;
			projectile.netUpdate = true;
		}
	}
}
