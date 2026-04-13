using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Guardians.Nature;

public class UltraniumKunai : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		//DisplayName.SetDefault("Ultranium Kunai");
	}

	public override void SetDefaults()
	{
		Projectile.width = 30;
		Projectile.height = 30;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.penetrate = 2;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 200;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 3;
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

	public override void AI()
	{
		if (Utils.NextBool(Main.rand))
		{
			Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("UltraniumDust").Type);
			dust.noGravity = true;
			dust.scale = 1.6f;
		}
		Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 0.8f;
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] > 7f)
		{
			Projectile.ai[0] = 7f;
			int num = HomeOnTarget();
			if (num != -1)
			{
				NPC nPC = Main.npc[num];
				Vector2 value = Projectile.DirectionTo(nPC.Center) * 25f;
				Projectile.velocity = Vector2.Lerp(Projectile.velocity, value, 0.05f);
			}
		}
	}

	private int HomeOnTarget()
	{
		int num = -1;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (nPC.CanBeChasedBy(Projectile))
			{
				_ = nPC.wet;
				float num2 = Projectile.Distance(nPC.Center);
				if (num2 <= 400f && (num == -1 || Projectile.Distance(Main.npc[num].Center) > num2))
				{
					num = i;
				}
			}
		}
		return num;
	}

	public override void OnKill(int timeLeft)
	{
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("UltraniumDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("UltraniumDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("UltraniumDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("UltraniumDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("UltraniumDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("UltraniumDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("UltraniumDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("UltraniumDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("UltraniumDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("UltraniumDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("UltraniumDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("UltraniumDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("UltraniumDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("UltraniumDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("UltraniumDust").Type);
	}
}
