using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ethereal.Projectiles;

public class EtherealDeathray : ModProjectile
{
	internal const float charge = 100f;

	public const float LaserLengthMax = 5000f;

	private float multiplier = 1f;

	public float LaserLength
	{
		get
		{
			return Projectile.localAI[1];
		}
		set
		{
			Projectile.localAI[1] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Ethereal Death Ray");
	}

	public override void SetDefaults()
	{
		Projectile.width = 36;
		Projectile.height = 36;
		Projectile.aiStyle = -1;
		Projectile.hostile = true;
		Projectile.friendly = false;
		Projectile.damage = 100;
		Projectile.penetrate = -1;
		Projectile.alpha = 152;
		Projectile.timeLeft = 3600;
		Projectile.tileCollide = false;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void AI()
	{
		Player player = Main.player[Projectile.owner];
		Projectile.ai[1] += 3.5f * multiplier;
		if (Projectile.ai[1] >= 160f && multiplier == 1f)
		{
			multiplier = -0.6f;
		}
		if (multiplier < 0f && Projectile.ai[1] <= 0f)
		{
			Projectile.Kill();
		}
		Projectile.gfxOffY = player.gfxOffY;
		Projectile.rotation = Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		Projectile.velocity = Vector2.Normalize(Projectile.velocity);
		float[] array = new float[2];
		Collision.LaserScan(Projectile.Center, Projectile.velocity, 0f, 5000f, array);
		float num = 0f;
		for (int i = 0; i < array.Length; i++)
		{
			num += array[i];
		}
		num /= (float)array.Length;
		float amount = 0.75f;
		LaserLength = MathHelper.Lerp(LaserLength, num, amount);
		Projectile.ai[0] += 1f;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		float collisionPoint = 0f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity * LaserLength, projHitbox.Width, ref collisionPoint);
	}

	public override bool? CanCutTiles()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * LaserLength, (float)Projectile.width * Projectile.scale * 2f, new PerLinePoint(CutTilesAndBreakWalls));
		return true;
	}

	private bool CutTilesAndBreakWalls(int x, int y)
	{
		return DelegateMethods.CutTiles(x, y);
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(Mod.Find<ModBuff>("ShadowflameDebuff").Type, 120, quiet: false);
	}

	public override bool CanHitPlayer(Player target)
	{
		if (Projectile.ai[1] < 100f)
		{
			return false;
		}
		return CanHitPlayer(target);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		if (Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		Texture2D texture = ModContent.Request<Texture2D>("Ultranium/NPCs/Ethereal/Projectiles/EtherealDeathrayBottom").Value;
		Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
		Texture2D texture2 = ModContent.Request<Texture2D>("Ultranium/NPCs/Ethereal/Projectiles/EtherealDeathrayTop").Value;
		float laserLength = LaserLength;
		Color color = Color.White * 0.8f * ((Projectile.ai[1] >= 100f) ? 1f : 0.8f);
		Vector2 position = Projectile.Center + new Vector2(0f, Projectile.gfxOffY) - Main.screenPosition;
        Main.spriteBatch.Draw(texture, position, null, color, Projectile.rotation, texture.Size() / 2f, new Vector2(Math.Min(Projectile.ai[1], 100f) / 100f, 1f), SpriteEffects.None, 0f);
		laserLength -= (float)(texture.Height / 2 + texture2.Height) * Projectile.scale;
		Vector2 vector = Projectile.Center + new Vector2(0f, Projectile.gfxOffY);
		vector += Projectile.velocity * Projectile.scale * texture.Height / 2f;
		if (laserLength > 0f)
		{
			float num = 0f;
			Rectangle value = new Rectangle(0, 16 * (Projectile.timeLeft / 3 % 5), texture2D.Width, 16);
			while (num + 1f < laserLength)
			{
				if (laserLength - num < (float)value.Height)
				{
					value.Height = (int)(laserLength - num);
				}
				Main.spriteBatch.Draw(texture2D, vector - Main.screenPosition, value, color, Projectile.rotation, new Vector2(value.Width / 2, 0f), new Vector2(Math.Min(Projectile.ai[1], 100f) / 100f, 1f), SpriteEffects.None, 0f);
				num += (float)value.Height * Projectile.scale;
				vector += Projectile.velocity * value.Height * Projectile.scale;
				value.Y += 16;
				if (value.Y + value.Height > texture2D.Height)
				{
					value.Y = 0;
				}
			}
		}
		Main.spriteBatch.Draw(texture2, vector - Main.screenPosition, null, color, Projectile.rotation, Utils.Frame(texture2, 1, 1, 0, 0).Top(), new Vector2(Math.Min(Projectile.ai[1], 100f) / 100f, 1f), SpriteEffects.None, 0f);
		return false;
	}
}
