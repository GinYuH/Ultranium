using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Aldin.Projectiles;

public class CosmicRayQuick : ModProjectile
{
	internal const float charge = 100f;

	public const float LaserLengthMax = 5000f;

	private float multiplier = 1f;

	private Color[] ColorCycle = new Color[2]
	{
		new Color(117, 235, 215),
		new Color(62, 30, 152)
	};

	public float LaserLength
	{
		get
		{
			return ((ModProjectile)this).Projectile.localAI[1];
		}
		set
		{
			((ModProjectile)this).Projectile.localAI[1] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Cosmic Deathbeam");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 36;
		((ModProjectile)this).Projectile.height = 36;
		((ModProjectile)this).Projectile.aiStyle = -1;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.damage = 100;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.alpha = 152;
		((ModProjectile)this).Projectile.timeLeft = 3600;
		((ModProjectile)this).Projectile.tileCollide = false;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void AI()
	{
		Player player = Main.player[((ModProjectile)this).Projectile.owner];
		((ModProjectile)this).Projectile.ai[1] += 20f * multiplier;
		if (((ModProjectile)this).Projectile.ai[1] >= 160f && multiplier == 1f)
		{
			multiplier = -0.5f;
		}
		if (multiplier < 0f && ((ModProjectile)this).Projectile.ai[1] <= 0f)
		{
			((ModProjectile)this).Projectile.Kill();
		}
		((ModProjectile)this).Projectile.gfxOffY = player.gfxOffY;
		((ModProjectile)this).Projectile.rotation = ((ModProjectile)this).Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		((ModProjectile)this).Projectile.velocity = Vector2.Normalize(((ModProjectile)this).Projectile.velocity);
		float[] array = new float[2];
		Collision.LaserScan(((ModProjectile)this).Projectile.Center, ((ModProjectile)this).Projectile.velocity, 0f, 5000f, array);
		float num = 0f;
		for (int i = 0; i < array.Length; i++)
		{
			num += array[i];
		}
		num /= (float)array.Length;
		float amount = 0.75f;
		LaserLength = MathHelper.Lerp(LaserLength, num, amount);
		((ModProjectile)this).Projectile.ai[0] += 1f;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		float collisionPoint = 0f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), ((ModProjectile)this).Projectile.Center, ((ModProjectile)this).Projectile.Center + ((ModProjectile)this).Projectile.velocity * LaserLength, projHitbox.Width, ref collisionPoint);
	}

	public override bool? CanCutTiles()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Utils.PlotTileLine(((ModProjectile)this).Projectile.Center, ((ModProjectile)this).Projectile.Center + ((ModProjectile)this).Projectile.velocity * LaserLength, (float)((ModProjectile)this).Projectile.width * ((ModProjectile)this).Projectile.scale * 2f, new PerLinePoint(CutTilesAndBreakWalls));
		return true;
	}

	private bool CutTilesAndBreakWalls(int x, int y)
	{
		return DelegateMethods.CutTiles(x, y);
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
	}

	public override bool CanHitPlayer(Player target)
	{
		if (((ModProjectile)this).Projectile.ai[1] < 100f)
		{
			return false;
		}
		return ((ModProjectile)this).CanHitPlayer(target);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		if (((ModProjectile)this).Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		Texture2D texture = ((ModProjectile)this).Mod.GetTexture("NPCs/Aldin/Projectiles/CosmicRayBottom");
		Texture2D texture2D = TextureAssets.Projectile[((ModProjectile)this).Projectile.type].Value;
		Texture2D texture2 = ((ModProjectile)this).Mod.GetTexture("NPCs/Aldin/Projectiles/CosmicRayTop");
		float laserLength = LaserLength;
		float amount = (float)(Main.GameUpdateCount % 60) / 60f;
		int num = (int)(Main.GameUpdateCount / 60 % 2);
		Color color = Color.Lerp(ColorCycle[num], ColorCycle[(num + 1) % 2], amount);
		Vector2 position = ((ModProjectile)this).Projectile.Center + new Vector2(0f, ((ModProjectile)this).Projectile.gfxOffY) - Main.screenPosition;
		spriteBatch.Draw(texture, position, null, color, ((ModProjectile)this).Projectile.rotation, texture.Size() / 2f, new Vector2(Math.Min(((ModProjectile)this).Projectile.ai[1], 100f) / 100f, 1f), SpriteEffects.None, 0f);
		laserLength -= (float)(texture.Height / 2 + texture2.Height) * ((ModProjectile)this).Projectile.scale;
		Vector2 vector = ((ModProjectile)this).Projectile.Center + new Vector2(0f, ((ModProjectile)this).Projectile.gfxOffY);
		vector += ((ModProjectile)this).Projectile.velocity * ((ModProjectile)this).Projectile.scale * texture.Height / 2f;
		if (laserLength > 0f)
		{
			float num2 = 0f;
			Rectangle value = new Rectangle(0, 16 * (((ModProjectile)this).Projectile.timeLeft / 3 % 5), texture2D.Width, 16);
			while (num2 + 1f < laserLength)
			{
				if (laserLength - num2 < (float)value.Height)
				{
					value.Height = (int)(laserLength - num2);
				}
				Main.spriteBatch.Draw(texture2D, vector - Main.screenPosition, value, color, ((ModProjectile)this).Projectile.rotation, new Vector2(value.Width / 2, 0f), new Vector2(Math.Min(((ModProjectile)this).Projectile.ai[1], 100f) / 100f, 1f), SpriteEffects.None, 0f);
				num2 += (float)value.Height * ((ModProjectile)this).Projectile.scale;
				vector += ((ModProjectile)this).Projectile.velocity * value.Height * ((ModProjectile)this).Projectile.scale;
				value.Y += 16;
				if (value.Y + value.Height > texture2D.Height)
				{
					value.Y = 0;
				}
			}
		}
		Main.spriteBatch.Draw(texture2, vector - Main.screenPosition, null, color, ((ModProjectile)this).Projectile.rotation, Utils.Frame(texture2, 1, 1, 0, 0).Top(), new Vector2(Math.Min(((ModProjectile)this).Projectile.ai[1], 100f) / 100f, 1f), SpriteEffects.None, 0f);
		return false;
	}
}
