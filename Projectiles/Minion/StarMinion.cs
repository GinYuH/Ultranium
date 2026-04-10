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
		// DisplayName.SetDefault("Spacial Star");
		Main.projPet[Projectile.type] = true;
		ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		Projectile.width = 32;
		Projectile.height = 32;
		Projectile.minion = true;
		Projectile.penetrate = -1;
		Projectile.hostile = false;
		Projectile.friendly = false;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.alpha = 0;
		Projectile.timeLeft = 10000;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
		Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/Projectiles/Minion/StarMinionBack").Value, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(Color.White), Projectile.rotation, new Vector2(texture2D.Width / 2, texture2D.Height / 2), Projectile.scale, (Projectile.spriteDirection != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
		Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/Projectiles/Minion/StarMinion").Value, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(Color.White), Projectile.rotation * -1f, new Vector2(texture2D.Width / 2, texture2D.Height / 2), Projectile.scale, (Projectile.spriteDirection != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
		return false;
	}

	public override void AI()
	{
		Player player = Main.player[Projectile.owner];
		Projectile.rotation += 0.02f;
		Projectile.position.X = player.Center.X - 15f;
		Projectile.position.Y = player.Center.Y - 100f;
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.dead)
		{
			modPlayer.StarMinion = false;
		}
		if (modPlayer.StarMinion)
		{
			Projectile.timeLeft = 2;
		}
		shootTimer++;
		float num = 350f;
		Projectile.tileCollide = false;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (!((Entity)nPC).active || nPC.friendly || nPC.damage <= 0 || nPC.dontTakeDamage || !(Vector2.Distance(Projectile.Center, nPC.Center) <= num))
			{
				continue;
			}
			int num2 = 1;
			Vector2 vector = new Vector2(Projectile.position.X + (float)(Projectile.width / 2), Projectile.position.Y + (float)(Projectile.height / 2));
			int num3 = 79;
			float num4 = 10f;
			float num5 = (float)Math.Atan2(vector.Y - (nPC.position.Y + (float)nPC.height * 0.5f), vector.X - (nPC.position.X + (float)nPC.width * 0.5f));
			if (shootTimer >= 45)
			{
				for (int j = 0; j < num2; j++)
				{
					Vector2 vector2 = new Vector2((float)(Math.Cos(num5) * (double)num4 * -1.0), (float)(Math.Sin(num5) * (double)num4 * -1.0)).RotatedByRandom(MathHelper.ToRadians(20f));
					Projectile.NewProjectile(null, vector.X, vector.Y, vector2.X, vector2.Y, num3, Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
				}
				shootTimer = 0;
			}
		}
	}
}
