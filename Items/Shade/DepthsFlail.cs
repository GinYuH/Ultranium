using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class DepthsFlail : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Azathoth");
	}

	public override void SetDefaults()
	{
		Item.width = 30;
		Item.height = 11;
		Item.damage = 70;
		Item.knockBack = 4f;
		Item.rare = 7;
		Item.useStyle = 5;
		Item.useAnimation = 19;
		Item.useTime = 19;
		Item.UseSound = SoundID.Item1;
		Item.value = Item.buyPrice(0, 68);
		Item.noMelee = true;
		Item.noUseGraphic = true;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.autoReuse = true;
		Item.noMelee = true;
		Item.shoot = Mod.Find<ModProjectile>("EldritchFlail").Type;
		Item.shootSpeed = 15f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		float num = (Main.rand.NextFloat() - 0.75f) * ((float)Math.PI / 4f);
		Projectile.NewProjectile(null, position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, num);
		return false;
	}
}
