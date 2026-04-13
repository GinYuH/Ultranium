using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Melee;

public class DukeThrow : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("The Duke's Throw");
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		Projectile.CloneDefaults(555);
		Projectile.extraUpdates = 1;
		base.AIType = ProjectileID.TheEyeOfCthulhu;
		Projectile.width = 24;
		Projectile.height = 24;
		Projectile.DamageType = DamageClass.Melee;
	}

	public override bool PreAI()
	{
		Player player = Main.player[Projectile.owner];
		if (!player.CheckMana(player.inventory[player.selectedItem].mana, pay: true))
		{
			Projectile.Kill();
		}
		return true;
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
		Projectile.frameCounter++;
		if (Projectile.frameCounter >= 30)
		{
			Projectile.frameCounter = 0;
			float num = (float)((double)Main.rand.Next(0, 361) * (Math.PI / 180.0));
			Vector2 vector = new Vector2((float)Math.Cos(num), (float)Math.Sin(num));
			int num2 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("MiniTyphoon").Type, Projectile.damage, (float)Projectile.owner, 0, 0f, 0f);
			Main.projectile[num2].friendly = true;
			Main.projectile[num2].hostile = false;
			Main.projectile[num2].velocity *= 12f;
		}
	}
}
