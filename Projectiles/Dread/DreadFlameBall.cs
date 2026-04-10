using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread;

public class DreadFlameBall : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		// DisplayName.SetDefault("Dread Fire Ball");
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1f;
		Projectile.width = 16;
		Projectile.height = 16;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.aiStyle = 0;
		Projectile.penetrate = 4;
		Projectile.extraUpdates = 1;
		Projectile.timeLeft = 600;
		Projectile.tileCollide = true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(Mod.Find<ModBuff>("DreadDebuff").Type, 180, quiet: false);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
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
		Projectile.rotation += 0.35f * (float)Projectile.direction;
		if (Utils.NextBool(Main.rand))
		{
			Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 90);
			dust.noGravity = true;
			dust.scale = 1f;
		}
	}
}
