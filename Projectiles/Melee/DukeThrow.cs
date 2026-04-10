using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Melee;

public class DukeThrow : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("The Duke's Throw");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.CloneDefaults(555);
		((ModProjectile)this).projectile.extraUpdates = 1;
		base.aiType = 555;
		((ModProjectile)this).projectile.width = 24;
		((ModProjectile)this).projectile.height = 24;
		((ModProjectile)this).projectile.melee = true;
	}

	public override bool PreAI()
	{
		Player player = Main.player[((ModProjectile)this).projectile.owner];
		if (!player.CheckMana(player.inventory[player.selectedItem].mana, pay: true))
		{
			((ModProjectile)this).projectile.Kill();
		}
		return true;
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

	public override void AI()
	{
		((ModProjectile)this).projectile.frameCounter++;
		if (((ModProjectile)this).projectile.frameCounter >= 30)
		{
			((ModProjectile)this).projectile.frameCounter = 0;
			float num = (float)((double)Main.rand.Next(0, 361) * (Math.PI / 180.0));
			Vector2 vector = new Vector2((float)Math.Cos(num), (float)Math.Sin(num));
			int num2 = Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, vector.X, vector.Y, ((ModProjectile)this).mod.ProjectileType("MiniTyphoon"), ((ModProjectile)this).projectile.damage, (float)((ModProjectile)this).projectile.owner, 0, 0f, 0f);
			Main.projectile[num2].friendly = true;
			Main.projectile[num2].hostile = false;
			Main.projectile[num2].velocity *= 12f;
		}
	}
}
