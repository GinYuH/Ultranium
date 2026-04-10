using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ranged;

public class SkyStar : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		((ModProjectile)this).DisplayName.SetDefault("Sanctus Stella");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 28;
		((ModProjectile)this).projectile.height = 28;
		((ModProjectile)this).projectile.aiStyle = 18;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ranged = true;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.timeLeft = 100;
		((ModProjectile)this).projectile.CloneDefaults(3);
		base.aiType = 3;
	}

	public override void AI()
	{
		if (Utils.NextBool(Main.rand))
		{
			Dust dust = Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 228);
			dust.noGravity = true;
			dust.scale = 1.6f;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Vector2 vector = new Vector2((float)Main.projectileTexture[((ModProjectile)this).projectile.type].Width * 0.5f, (float)((ModProjectile)this).projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY);
			Color color = ((ModProjectile)this).projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).projectile.oldPos.Length - i) / (float)((ModProjectile)this).projectile.oldPos.Length);
			spriteBatch.Draw(Main.projectileTexture[((ModProjectile)this).projectile.type], position, null, color, ((ModProjectile)this).projectile.rotation, vector, ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void Kill(int timeleft)
	{
		Projectile.NewProjectile(((ModProjectile)this).projectile.Center, Vector2.Zero, ((ModProjectile)this).mod.ProjectileType("SkyStarExplosion"), ((ModProjectile)this).projectile.damage, ((ModProjectile)this).projectile.knockBack, ((ModProjectile)this).projectile.owner, 0f, 0f);
		Main.PlaySound(SoundID.Item74, ((ModProjectile)this).projectile.position);
	}
}
