using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ethereal;

public class EtherealWisp : ModProjectile
{
	public int shootTimer = 30;

	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Ethereal Wisp");
		Main.projFrames[((ModProjectile)this).projectile.type] = 4;
		ProjectileID.Sets.MinionSacrificable[((ModProjectile)this).projectile.type] = true;
		ProjectileID.Sets.MinionTargettingFeature[((ModProjectile)this).projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 30;
		((ModProjectile)this).projectile.height = 30;
		((ModProjectile)this).projectile.netImportant = true;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.timeLeft = 18000;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.minion = true;
		((ModProjectile)this).projectile.minionSlots = 1f;
		((ModProjectile)this).projectile.aiStyle = 62;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		_ = ((ModProjectile)this).projectile.type;
		((ModProjectile)this).mod.ProjectileType("Wisp");
		Player obj = Main.player[((ModProjectile)this).projectile.owner];
		UltraniumPlayer ultraniumPlayer = (UltraniumPlayer)(object)obj.GetModPlayer(((ModProjectile)this).mod, "UltraniumPlayer");
		obj.AddBuff(((ModProjectile)this).mod.BuffType("WispBuff"), 3600, fromNetPvP: true);
		if (obj.dead)
		{
			ultraniumPlayer.Wisp = false;
		}
		if (ultraniumPlayer.Wisp)
		{
			((ModProjectile)this).projectile.timeLeft = 2;
		}
		shootTimer--;
		float num = 400f;
		((ModProjectile)this).projectile.tileCollide = false;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (!((Entity)nPC).active || nPC.friendly || nPC.damage <= 0 || nPC.dontTakeDamage || !(Vector2.Distance(((ModProjectile)this).projectile.Center, nPC.Center) <= num))
			{
				continue;
			}
			int num2 = 1;
			Vector2 vector = new Vector2(((ModProjectile)this).projectile.position.X + (float)(((ModProjectile)this).projectile.width / 2), ((ModProjectile)this).projectile.position.Y + (float)(((ModProjectile)this).projectile.height / 2));
			int num3 = ((ModProjectile)this).mod.ProjectileType("WispBolt");
			float num4 = 10f;
			float num5 = (float)Math.Atan2(vector.Y - (nPC.position.Y + (float)nPC.height * 0.5f), vector.X - (nPC.position.X + (float)nPC.width * 0.5f));
			int num6 = 55;
			if (shootTimer <= 0)
			{
				for (int j = 0; j < num2; j++)
				{
					Vector2 vector2 = new Vector2((float)(Math.Cos(num5) * (double)num4 * -1.0), (float)(Math.Sin(num5) * (double)num4 * -1.0)).RotatedByRandom(MathHelper.ToRadians(20f));
					Projectile.NewProjectile(vector.X, vector.Y, vector2.X, vector2.Y, num3, num6, 0f, Main.myPlayer, 0f, 0f);
				}
				shootTimer = 30;
			}
		}
	}

	public override bool PreDraw(SpriteBatch sb, Color lightColor)
	{
		((ModProjectile)this).projectile.frameCounter++;
		if (((ModProjectile)this).projectile.frameCounter >= 4)
		{
			((ModProjectile)this).projectile.frame++;
			((ModProjectile)this).projectile.frameCounter = 0;
			if (((ModProjectile)this).projectile.frame >= 4)
			{
				((ModProjectile)this).projectile.frame = 0;
			}
		}
		return true;
	}
}
