using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Dread.Projectiles;

public class DreadSpit : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		// DisplayName.SetDefault("Dread Spit");
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1f;
		Projectile.width = 16;
		Projectile.height = 16;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.aiStyle = 0;
		Projectile.penetrate = 1;
		Projectile.extraUpdates = 1;
		Projectile.timeLeft = 3000;
		Projectile.tileCollide = true;
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 90, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != Projectile.Center)
			{
				Main.dust[num].velocity = Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
		int num2 = 1;
		int num3 = Main.rand.Next(0, 180);
		int num4 = (Main.expertMode ? 25 : 45);
		for (int j = 0; j < num2; j++)
		{
			float num5 = MathHelper.ToRadians(270 / num2 * j + num3);
			Vector2 vector = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(num5);
			vector.Normalize();
			vector.X *= 8f;
			vector.Y *= 8f;
			Projectile.NewProjectile(null, Projectile.Center.X, Projectile.Center.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("DreadSpitHoming").Type, num4, 2f, Projectile.owner, 0f, 0f);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		player.AddBuff(Mod.Find<ModBuff>("DreadDebuff").Type, 180, fromNetPvP: true);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/Dread/Projectiles/DreadSpitTrail").Width * 0.5f, (float)Projectile.height * 0.5f);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
			spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/Dread/Projectiles/DreadSpitTrail"), position, null, color, Projectile.rotation, vector, Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		Projectile.rotation += 0.35f * (float)Projectile.direction;
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] >= 80f)
		{
			Projectile.velocity.Y = Projectile.velocity.Y + 0.1f;
			Projectile.velocity.X = Projectile.velocity.X * 0.99f;
		}
		if (Utils.NextBool(Main.rand))
		{
			Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 90);
			dust.noGravity = true;
			dust.scale = 1f;
		}
	}
}
