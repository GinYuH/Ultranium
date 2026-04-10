using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent.Projectiles;

public class MotherPhantomBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 35;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
		// ((ModProjectile)this).DisplayName.SetDefault("Darkness Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.aiStyle = 132;
		((ModProjectile)this).Projectile.width = 72;
		((ModProjectile)this).Projectile.height = 72;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.aiStyle = 0;
		((ModProjectile)this).Projectile.penetrate = 3;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.timeLeft = 420;
		((ModProjectile)this).Projectile.alpha = 125;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)TextureAssets.Projectile[((ModProjectile)this).Projectile.type].Value.Width * 0.5f, (float)((ModProjectile)this).Projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).Projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).Projectile.gfxOffY);
			Color color = ((ModProjectile)this).Projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).Projectile.oldPos.Length - i) / (float)((ModProjectile)this).Projectile.oldPos.Length);
			spriteBatch.Draw(TextureAssets.Projectile[((ModProjectile)this).Projectile.type].Value, position, null, color, ((ModProjectile)this).Projectile.rotation, vector, ((ModProjectile)this).Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.rotation = ((ModProjectile)this).Projectile.velocity.ToRotation() + 1.57f;
		if (((ModProjectile)this).Projectile.localAI[0] == 0f)
		{
			((ModProjectile)this).Projectile.localAI[0] = 1f;
			SoundEngine.PlaySound(SoundID.Item20, ((ModProjectile)this).Projectile.position);
		}
		if ((((ModProjectile)this).Projectile.ai[0] -= 1f) > 0f)
		{
			float num = ((ModProjectile)this).Projectile.velocity.Length();
			num += ((ModProjectile)this).Projectile.ai[1];
			((ModProjectile)this).Projectile.velocity = Vector2.Normalize(((ModProjectile)this).Projectile.velocity) * num;
		}
		else if (((ModProjectile)this).Projectile.ai[0] == 0f)
		{
			((ModProjectile)this).Projectile.ai[1] = (int)Player.FindClosest(((ModProjectile)this).Projectile.Center, 0, 0);
			if (((ModProjectile)this).Projectile.ai[1] != -1f && ((Entity)Main.player[(int)((ModProjectile)this).Projectile.ai[1]]).active && !Main.player[(int)((ModProjectile)this).Projectile.ai[1]].dead)
			{
				((ModProjectile)this).Projectile.velocity = ((ModProjectile)this).Projectile.DirectionTo(Main.player[(int)((ModProjectile)this).Projectile.ai[1]].Center);
				((ModProjectile)this).Projectile.netUpdate = true;
			}
		}
		else if (((ModProjectile)this).Projectile.localAI[1] < 300f)
		{
			float curAngle = ((ModProjectile)this).Projectile.velocity.ToRotation();
			float targetAngle = (Main.player[(int)((ModProjectile)this).Projectile.ai[1]].Center - ((ModProjectile)this).Projectile.Center).ToRotation();
			((ModProjectile)this).Projectile.velocity = new Vector2(((ModProjectile)this).Projectile.velocity.Length(), 0f).RotatedBy(curAngle.AngleLerp(targetAngle, 0.01f));
		}
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item14, new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y));
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, ((ModProjectile)this).Mod.Find<ModDust>("ShadowDustPurple").Type, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).Projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).Projectile.DirectionTo(Main.dust[num].position) * 10f;
			}
		}
		for (int j = 0; j < 40; j++)
		{
			int num2 = Dust.NewDust(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, ((ModProjectile)this).Mod.Find<ModDust>("ShadowDustPurple").Type, 0f, -2f, 0, default(Color), 2f);
			Main.dust[num2].noGravity = true;
			Main.dust[num2].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num2].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num2].position != ((ModProjectile)this).Projectile.Center)
			{
				Main.dust[num2].velocity = ((ModProjectile)this).Projectile.DirectionTo(Main.dust[num2].position) * 3f;
			}
		}
	}
}
