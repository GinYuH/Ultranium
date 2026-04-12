using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus;

public class Exitium : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		DisplayName.SetDefault("Exitium");
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1f;
		Projectile.width = 24;
		Projectile.height = 18;
		Projectile.aiStyle = 99;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.scale = 1f;
		ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f;
		ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 500f;
		ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 18f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, (float)Projectile.height * 0.5f);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
			Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, position, null, color, Projectile.rotation, vector, Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 6;
	}

	public override void PostAI()
	{
		Projectile.rotation -= 20f;
	}

	public override void AI()
	{
		Projectile.frameCounter++;
		if (Projectile.frameCounter < 45)
		{
			return;
		}
		Projectile.frameCounter = 0;
		float num = 8000f;
		int num2 = -1;
		for (int i = 0; i < 200; i++)
		{
			float num3 = Vector2.Distance(Projectile.Center, Main.npc[i].Center);
			if (num3 < num && num3 < 640f && Main.npc[i].CanBeChasedBy(Projectile))
			{
				num2 = i;
				num = num3;
			}
		}
		if (num2 == -1 || !Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num2].position, Main.npc[num2].width, Main.npc[num2].height))
		{
			return;
		}
		Vector2 spinningpoint = new Vector2(6f, 0f).RotatedByRandom(Math.PI * 2.0);
		for (int j = 0; j < 10; j++)
		{
			Vector2 vector = spinningpoint.RotatedBy(Math.PI / 3.0 * ((double)j + Main.rand.NextDouble() - 0.5));
			float num4 = (float)Main.rand.Next(10, 80) * 0.001f;
			if (Main.rand.Next(2) == 0)
			{
				num4 *= -1f;
			}
			float num5 = (float)Main.rand.Next(10, 80) * 0.001f;
			if (Main.rand.Next(2) == 0)
			{
				num5 *= -1f;
			}
			Projectile.NewProjectile(null, Projectile.Center, vector, Mod.Find<ModProjectile>("ExitiumTentacle").Type, Projectile.damage, 0f, Main.myPlayer, num5, num4);
		}
	}
}
