using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ignodium.Projectiles;

public class IgnodiumBeam : ModProjectile
{
	private int target;

	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Ignodium Bolt");
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1.5f;
		Projectile.width = 24;
		Projectile.height = 24;
		Projectile.hostile = true;
		Projectile.tileCollide = false;
		Projectile.penetrate = 10;
		Projectile.timeLeft = 300;
		Projectile.extraUpdates = 1;
		Projectile.ignoreWater = true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.Request<Texture2D>("Ultranium/NPCs/Ignodium/Projectiles/IgnodiumBeamTrail").Width() * 0.5f, (float)Projectile.height * 0.5f);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/NPCs/Ignodium/Projectiles/IgnodiumBeamTrail").Value, position, null, color, Projectile.rotation, vector, Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override bool PreAI()
	{
		Projectile.velocity *= 1.02f;
		Projectile.rotation = Projectile.velocity.ToRotation() + 1.57f;
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		Projectile.Kill();
		SoundEngine.PlaySound(SoundID.Item20, new Vector2(Projectile.position.X, Projectile.position.Y));
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(target);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		target = reader.Read();
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != Projectile.Center)
			{
				Main.dust[num].velocity = Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}
}
