using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Minion;

public class StarMinion : ModProjectile
{
	public int shootTimer = 20;

	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Spacial Star");
		Main.projPet[((ModProjectile)this).Projectile.type] = true;
		ProjectileID.Sets.MinionTargettingFeature[((ModProjectile)this).Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 32;
		((ModProjectile)this).Projectile.height = 32;
		((ModProjectile)this).Projectile.minion = true;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.hostile = false;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.alpha = 0;
		((ModProjectile)this).Projectile.timeLeft = 10000;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D = TextureAssets.Projectile[((ModProjectile)this).Projectile.type].Value;
		spriteBatch.Draw(ModContent.GetTexture("Ultranium/Projectiles/Minion/StarMinionBack"), ((ModProjectile)this).Projectile.Center - Main.screenPosition, null, ((ModProjectile)this).Projectile.GetAlpha(Color.White), ((ModProjectile)this).Projectile.rotation, new Vector2(texture2D.Width / 2, texture2D.Height / 2), ((ModProjectile)this).Projectile.scale, (((ModProjectile)this).Projectile.spriteDirection != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
		spriteBatch.Draw(ModContent.GetTexture("Ultranium/Projectiles/Minion/StarMinion"), ((ModProjectile)this).Projectile.Center - Main.screenPosition, null, ((ModProjectile)this).Projectile.GetAlpha(Color.White), ((ModProjectile)this).Projectile.rotation * -1f, new Vector2(texture2D.Width / 2, texture2D.Height / 2), ((ModProjectile)this).Projectile.scale, (((ModProjectile)this).Projectile.spriteDirection != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
		return false;
	}

	public override void AI()
	{
		Player player = Main.player[((ModProjectile)this).Projectile.owner];
		((ModProjectile)this).Projectile.rotation += 0.02f;
		((ModProjectile)this).Projectile.position.X = player.Center.X - 15f;
		((ModProjectile)this).Projectile.position.Y = player.Center.Y - 100f;
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.dead)
		{
			modPlayer.StarMinion = false;
		}
		if (modPlayer.StarMinion)
		{
			((ModProjectile)this).Projectile.timeLeft = 2;
		}
		shootTimer++;
		float num = 350f;
		((ModProjectile)this).Projectile.tileCollide = false;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (!((Entity)nPC).active || nPC.friendly || nPC.damage <= 0 || nPC.dontTakeDamage || !(Vector2.Distance(((ModProjectile)this).Projectile.Center, nPC.Center) <= num))
			{
				continue;
			}
			int num2 = 1;
			Vector2 vector = new Vector2(((ModProjectile)this).Projectile.position.X + (float)(((ModProjectile)this).Projectile.width / 2), ((ModProjectile)this).Projectile.position.Y + (float)(((ModProjectile)this).Projectile.height / 2));
			int num3 = 79;
			float num4 = 10f;
			float num5 = (float)Math.Atan2(vector.Y - (nPC.position.Y + (float)nPC.height * 0.5f), vector.X - (nPC.position.X + (float)nPC.width * 0.5f));
			if (shootTimer >= 45)
			{
				for (int j = 0; j < num2; j++)
				{
					Vector2 vector2 = new Vector2((float)(Math.Cos(num5) * (double)num4 * -1.0), (float)(Math.Sin(num5) * (double)num4 * -1.0)).RotatedByRandom(MathHelper.ToRadians(20f));
					Projectile.NewProjectile(vector.X, vector.Y, vector2.X, vector2.Y, num3, ((ModProjectile)this).Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
				}
				shootTimer = 0;
			}
		}
	}
}
