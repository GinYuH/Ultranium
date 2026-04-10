using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Minion;

public class StarMinion : ModProjectile
{
	public int shootTimer = 20;

	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Spacial Star");
		Main.projPet[((ModProjectile)this).projectile.type] = true;
		ProjectileID.Sets.MinionTargettingFeature[((ModProjectile)this).projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 32;
		((ModProjectile)this).projectile.height = 32;
		((ModProjectile)this).projectile.minion = true;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.hostile = false;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.alpha = 0;
		((ModProjectile)this).projectile.timeLeft = 10000;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Texture2D texture2D = Main.projectileTexture[((ModProjectile)this).projectile.type];
		spriteBatch.Draw(ModContent.GetTexture("Ultranium/Projectiles/Minion/StarMinionBack"), ((ModProjectile)this).projectile.Center - Main.screenPosition, null, ((ModProjectile)this).projectile.GetAlpha(Color.White), ((ModProjectile)this).projectile.rotation, new Vector2(texture2D.Width / 2, texture2D.Height / 2), ((ModProjectile)this).projectile.scale, (((ModProjectile)this).projectile.spriteDirection != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
		spriteBatch.Draw(ModContent.GetTexture("Ultranium/Projectiles/Minion/StarMinion"), ((ModProjectile)this).projectile.Center - Main.screenPosition, null, ((ModProjectile)this).projectile.GetAlpha(Color.White), ((ModProjectile)this).projectile.rotation * -1f, new Vector2(texture2D.Width / 2, texture2D.Height / 2), ((ModProjectile)this).projectile.scale, (((ModProjectile)this).projectile.spriteDirection != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
		return false;
	}

	public override void AI()
	{
		Player player = Main.player[((ModProjectile)this).projectile.owner];
		((ModProjectile)this).projectile.rotation += 0.02f;
		((ModProjectile)this).projectile.position.X = player.Center.X - 15f;
		((ModProjectile)this).projectile.position.Y = player.Center.Y - 100f;
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.dead)
		{
			modPlayer.StarMinion = false;
		}
		if (modPlayer.StarMinion)
		{
			((ModProjectile)this).projectile.timeLeft = 2;
		}
		shootTimer++;
		float num = 350f;
		((ModProjectile)this).projectile.tileCollide = false;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (!((Entity)nPC).active || nPC.friendly || nPC.damage <= 0 || nPC.dontTakeDamage || !(Vector2.Distance(((ModProjectile)this).projectile.Center, nPC.Center) <= num))
			{
				continue;
			}
			int num2 = 1;
			Vector2 vector = new Vector2(((ModProjectile)this).projectile.position.X + (float)(((ModProjectile)this).projectile.width / 2), ((ModProjectile)this).projectile.position.Y + (float)(((ModProjectile)this).projectile.height / 2));
			int num3 = 79;
			float num4 = 10f;
			float num5 = (float)Math.Atan2(vector.Y - (nPC.position.Y + (float)nPC.height * 0.5f), vector.X - (nPC.position.X + (float)nPC.width * 0.5f));
			if (shootTimer >= 45)
			{
				for (int j = 0; j < num2; j++)
				{
					Vector2 vector2 = new Vector2((float)(Math.Cos(num5) * (double)num4 * -1.0), (float)(Math.Sin(num5) * (double)num4 * -1.0)).RotatedByRandom(MathHelper.ToRadians(20f));
					Projectile.NewProjectile(vector.X, vector.Y, vector2.X, vector2.Y, num3, ((ModProjectile)this).projectile.damage, 0f, Main.myPlayer, 0f, 0f);
				}
				shootTimer = 0;
			}
		}
	}
}
