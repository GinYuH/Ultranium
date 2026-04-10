using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Aldin.Projectiles;

public class CosmicRayArena : ModProjectile
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
			return ((ModProjectile)this).projectile.localAI[1];
		}
		set
		{
			((ModProjectile)this).projectile.localAI[1] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Cosmic Deathbeam");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 36;
		((ModProjectile)this).projectile.height = 36;
		((ModProjectile)this).projectile.aiStyle = -1;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.damage = 100;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.alpha = 152;
		((ModProjectile)this).projectile.timeLeft = 3600;
		((ModProjectile)this).projectile.tileCollide = false;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void AI()
	{
		Player player = Main.player[((ModProjectile)this).projectile.owner];
		((ModProjectile)this).projectile.ai[1] += 0.8f * multiplier;
		if (((ModProjectile)this).projectile.ai[1] >= 160f && multiplier == 1f)
		{
			multiplier = -0.6f;
		}
		if (multiplier < 0f && ((ModProjectile)this).projectile.ai[1] <= 0f)
		{
			((ModProjectile)this).projectile.Kill();
		}
		((ModProjectile)this).projectile.gfxOffY = player.gfxOffY;
		((ModProjectile)this).projectile.rotation = ((ModProjectile)this).projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		((ModProjectile)this).projectile.velocity = Vector2.Normalize(((ModProjectile)this).projectile.velocity);
		float[] array = new float[2];
		Collision.LaserScan(((ModProjectile)this).projectile.Center, ((ModProjectile)this).projectile.velocity, 0f, 5000f, array);
		float num = 0f;
		for (int i = 0; i < array.Length; i++)
		{
			num += array[i];
		}
		num /= (float)array.Length;
		float amount = 0.75f;
		LaserLength = MathHelper.Lerp(LaserLength, num, amount);
		((ModProjectile)this).projectile.ai[0] += 1f;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		float collisionPoint = 0f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), ((ModProjectile)this).projectile.Center, ((ModProjectile)this).projectile.Center + ((ModProjectile)this).projectile.velocity * LaserLength, projHitbox.Width, ref collisionPoint);
	}

	public override bool? CanCutTiles()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Utils.PlotTileLine(((ModProjectile)this).projectile.Center, ((ModProjectile)this).projectile.Center + ((ModProjectile)this).projectile.velocity * LaserLength, (float)((ModProjectile)this).projectile.width * ((ModProjectile)this).projectile.scale * 2f, new PerLinePoint(CutTilesAndBreakWalls));
		return true;
	}

	private bool CutTilesAndBreakWalls(int x, int y)
	{
		return DelegateMethods.CutTiles(x, y);
	}

	public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
	{
	}

	public override bool CanHitPlayer(Player target)
	{
		if (((ModProjectile)this).projectile.ai[1] < 100f)
		{
			return false;
		}
		return ((ModProjectile)this).CanHitPlayer(target);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		if (((ModProjectile)this).projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		Texture2D texture = ((ModProjectile)this).mod.GetTexture("NPCs/Aldin/Projectiles/CosmicRayBottom");
		Texture2D texture2D = Main.projectileTexture[((ModProjectile)this).projectile.type];
		Texture2D texture2 = ((ModProjectile)this).mod.GetTexture("NPCs/Aldin/Projectiles/CosmicRayTop");
		float laserLength = LaserLength;
		float amount = (float)(Main.GameUpdateCount % 60) / 60f;
		int num = (int)(Main.GameUpdateCount / 60 % 2);
		Color color = Color.Lerp(ColorCycle[num], ColorCycle[(num + 1) % 2], amount);
		Vector2 position = ((ModProjectile)this).projectile.Center + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY) - Main.screenPosition;
		spriteBatch.Draw(texture, position, null, color, ((ModProjectile)this).projectile.rotation, texture.Size() / 2f, new Vector2(Math.Min(((ModProjectile)this).projectile.ai[1], 100f) / 100f, 1f), SpriteEffects.None, 0f);
		laserLength -= (float)(texture.Height / 2 + texture2.Height) * ((ModProjectile)this).projectile.scale;
		Vector2 vector = ((ModProjectile)this).projectile.Center + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY);
		vector += ((ModProjectile)this).projectile.velocity * ((ModProjectile)this).projectile.scale * texture.Height / 2f;
		if (laserLength > 0f)
		{
			float num2 = 0f;
			Rectangle value = new Rectangle(0, 16 * (((ModProjectile)this).projectile.timeLeft / 3 % 5), texture2D.Width, 16);
			while (num2 + 1f < laserLength)
			{
				if (laserLength - num2 < (float)value.Height)
				{
					value.Height = (int)(laserLength - num2);
				}
				Main.spriteBatch.Draw(texture2D, vector - Main.screenPosition, value, color, ((ModProjectile)this).projectile.rotation, new Vector2(value.Width / 2, 0f), new Vector2(Math.Min(((ModProjectile)this).projectile.ai[1], 100f) / 100f, 1f), SpriteEffects.None, 0f);
				num2 += (float)value.Height * ((ModProjectile)this).projectile.scale;
				vector += ((ModProjectile)this).projectile.velocity * value.Height * ((ModProjectile)this).projectile.scale;
				value.Y += 16;
				if (value.Y + value.Height > texture2D.Height)
				{
					value.Y = 0;
				}
			}
		}
		Main.spriteBatch.Draw(texture2, vector - Main.screenPosition, null, color, ((ModProjectile)this).projectile.rotation, Utils.Frame(texture2, 1, 1, 0, 0).Top(), new Vector2(Math.Min(((ModProjectile)this).projectile.ai[1], 100f) / 100f, 1f), SpriteEffects.None, 0f);
		return false;
	}
}
