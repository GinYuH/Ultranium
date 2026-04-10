using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent.Projectiles;

public class DarkmatterTentacle : ModProjectile
{
	public float angleSpeed;

	public float lengthSpeed;

	public int ShadeMass = -1;

	private int netUpdateCounter;

	private const float maxAngleSpeed = 0.01f;

	private const float angleBuffer = (float)Math.PI / 12f;

	public const float minLength = 80f;

	private const float maxLength = 165f;

	private const float maxLengthSpeed = 1f;

	public float width
	{
		get
		{
			return ((ModProjectile)this).projectile.ai[0];
		}
		set
		{
			((ModProjectile)this).projectile.ai[0] = value;
		}
	}

	public float length
	{
		get
		{
			return ((ModProjectile)this).projectile.ai[1];
		}
		set
		{
			((ModProjectile)this).projectile.ai[1] = value;
		}
	}

	public float minAngle
	{
		get
		{
			return ((ModProjectile)this).projectile.localAI[0];
		}
		set
		{
			((ModProjectile)this).projectile.localAI[0] = value;
		}
	}

	public float maxAngle
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

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 1;
		((ModProjectile)this).projectile.height = 1;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.timeLeft = 2;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.ignoreWater = true;
	}

	public override void AI()
	{
		NPC nPC = Main.npc[ShadeMass];
		if (!((Entity)nPC).active || nPC.type != ModContent.NPCType<ShadeMass>())
		{
			return;
		}
		((ModProjectile)this).projectile.timeLeft = 2;
		Player obj = Main.player[nPC.target];
		((ModProjectile)this).projectile.position = nPC.Center;
		Vector2 vector = obj.Center - ((ModProjectile)this).projectile.position;
		float num = vector.Length() + 32f;
		Angle angle = new Angle(((ModProjectile)this).projectile.rotation);
		Angle other = new Angle((float)Math.Atan2(vector.Y, vector.X));
		Angle angle2 = new Angle(minAngle);
		Angle angle3 = new Angle(maxAngle);
		Angle angle4 = new Angle((minAngle + maxAngle) / 2f);
		if (angle4.Between(angle2, angle3))
		{
			angle4 = angle4.Opposite();
		}
		Angle angle5 = new Angle((float)Math.PI / 12f);
		if (other.Between(angle2 - angle5, angle3 + angle5))
		{
			if (angle.Between(angle3, angle4))
			{
				angleSpeed -= 0.0009999999f;
			}
			else if (angle.Between(angle4, angle2))
			{
				angleSpeed += 0.0009999999f;
			}
			else if (angle.ClockwiseFrom(other))
			{
				angleSpeed += 0.0009999999f;
			}
			else
			{
				angleSpeed -= 0.0009999999f;
			}
			if (length > 165f)
			{
				lengthSpeed -= 0.1f;
			}
			else if (length < 80f)
			{
				lengthSpeed += 0.1f;
			}
			else if (num > length)
			{
				lengthSpeed += 0.1f;
			}
			else if (num < length)
			{
				lengthSpeed -= 0.1f;
			}
		}
		else
		{
			if (angle.Between(angle3, angle4))
			{
				angleSpeed -= 0.0009999999f;
			}
			else if (angle.Between(angle4, angle2))
			{
				angleSpeed += 0.0009999999f;
			}
			else if (angleSpeed > 0f)
			{
				angleSpeed += 0.00049999997f;
			}
			else if (angleSpeed < 0f)
			{
				angleSpeed -= 0.00049999997f;
			}
			else
			{
				angleSpeed = 0.00049999997f;
			}
			if (length > 80f)
			{
				lengthSpeed -= 0.1f;
			}
			else
			{
				lengthSpeed += 0.1f;
			}
		}
		if (angleSpeed > 0.01f)
		{
			angleSpeed = 0.01f;
		}
		else if (angleSpeed < -0.01f)
		{
			angleSpeed = -0.01f;
		}
		if (lengthSpeed > 1f)
		{
			lengthSpeed = 1f;
		}
		else if (lengthSpeed < -1f)
		{
			lengthSpeed = -1f;
		}
		((ModProjectile)this).projectile.rotation += angleSpeed;
		length += lengthSpeed;
		if (Main.netMode == 2)
		{
			netUpdateCounter++;
			if (netUpdateCounter >= 300)
			{
				((ModProjectile)this).projectile.netUpdate = true;
				netUpdateCounter = 0;
			}
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(((ModProjectile)this).projectile.rotation);
		writer.Write(minAngle);
		writer.Write(maxAngle);
		writer.Write(angleSpeed);
		writer.Write(lengthSpeed);
		writer.Write((short)ShadeMass);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		((ModProjectile)this).projectile.rotation = reader.ReadSingle();
		minAngle = reader.ReadSingle();
		maxAngle = reader.ReadSingle();
		angleSpeed = reader.ReadSingle();
		lengthSpeed = reader.ReadSingle();
		ShadeMass = reader.ReadInt16();
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		float collisionPoint = 0f;
		return Collision.CheckAABBvLineCollision(new Vector2(targetHitbox.X, targetHitbox.Y), new Vector2(targetHitbox.Width, targetHitbox.Height), ((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.position + length * new Vector2((float)Math.Cos(((ModProjectile)this).projectile.rotation), (float)Math.Sin(((ModProjectile)this).projectile.rotation)), width, ref collisionPoint);
	}

	public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
	{
		((ModProjectile)this).projectile.rotation %= (float)Math.PI * 2f;
		if (((ModProjectile)this).projectile.rotation % (float)Math.PI == 0f)
		{
			((ModProjectile)this).projectile.direction = -target.direction;
			return;
		}
		if (((ModProjectile)this).projectile.rotation % (float)Math.PI / 2f == 0f)
		{
			((ModProjectile)this).projectile.direction = ((!(target.Center.X < ((ModProjectile)this).projectile.position.X)) ? 1 : (-1));
			return;
		}
		float num = target.Center.Y - ((ModProjectile)this).projectile.position.Y;
		float num2 = ((ModProjectile)this).projectile.position.X + num / (float)Math.Tan(((ModProjectile)this).projectile.rotation);
		((ModProjectile)this).projectile.direction = ((!(target.Center.X < num2)) ? 1 : (-1));
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Texture2D texture2D = Main.projectileTexture[((ModProjectile)this).projectile.type];
		int num = texture2D.Width;
		int num2 = (int)Math.Ceiling(length / (float)num);
		float num3 = 0f;
		if (num2 > 1)
		{
			num3 = (length - (float)num) / (float)(num2 - 1);
		}
		Vector2 vector = new Vector2((float)Math.Cos(((ModProjectile)this).projectile.rotation), (float)Math.Sin(((ModProjectile)this).projectile.rotation));
		SpriteEffects effects = SpriteEffects.None;
		if (((ModProjectile)this).projectile.spriteDirection == -1)
		{
			effects = SpriteEffects.FlipVertically;
		}
		for (int i = 1; i <= num2; i++)
		{
			Texture2D texture = texture2D;
			if (i == num2)
			{
				texture = ((ModProjectile)this).mod.GetTexture("NPCs/ShadowEvent/Projectiles/DarkmatterTentacleTop");
			}
			Vector2 vector2 = ((ModProjectile)this).projectile.position + vector * (num3 * (float)(i - 1) + (float)num / 2f);
			Color color = Lighting.GetColor((int)(vector2.X / 16f), (int)(vector2.Y / 16f));
			spriteBatch.Draw(texture, vector2 - Main.screenPosition, null, ((ModProjectile)this).projectile.GetAlpha(color), ((ModProjectile)this).projectile.rotation, new Vector2(texture2D.Width / 2, texture2D.Height / 2), 1f, effects, 0f);
		}
		return false;
	}

	public override void PostDraw(SpriteBatch sb, Color lightColor)
	{
		Main.instance.DrawNPC(ShadeMass, behindTiles: false);
	}
}
