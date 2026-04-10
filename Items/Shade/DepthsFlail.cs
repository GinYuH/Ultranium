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
		// ((ModItem)this).DisplayName.SetDefault("Azathoth");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 30;
		((Entity)(object)((ModItem)this).Item).height = 11;
		((ModItem)this).Item.damage = 70;
		((ModItem)this).Item.knockBack = 4f;
		((ModItem)this).Item.rare = 7;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.useAnimation = 19;
		((ModItem)this).Item.useTime = 19;
		((ModItem)this).Item.UseSound = SoundID.Item1;
		((ModItem)this).Item.value = Item.buyPrice(0, 68);
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.noUseGraphic = true;
		((ModItem)this).Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("EldritchFlail").Type;
		((ModItem)this).Item.shootSpeed = 15f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		float num = (Main.rand.NextFloat() - 0.75f) * ((float)Math.PI / 4f);
		Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, num);
		return false;
	}
}
