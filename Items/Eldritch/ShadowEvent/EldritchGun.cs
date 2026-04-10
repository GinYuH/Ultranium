using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.ShadowEvent;

public class EldritchGun : ModItem
{
	public override void SetStaticDefaults()
	{
		// Tooltip.SetDefault("Fires out eldritch tentacles\nDoes not require ammo to use");
		// DisplayName.SetDefault("Death's Raze");
	}

	public override void SetDefaults()
	{
		Item.value = Item.buyPrice(1, 50);
		Item.damage = 280;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 20;
		Item.height = 40;
		Item.useTime = 6;
		Item.useAnimation = 6;
		Item.useStyle = 5;
		Item.knockBack = 6f;
		Item.rare = 11;
		Item.UseSound = SoundID.Item34;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("ShadeTentacle").Type;
		Item.shootSpeed = 32f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-6f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 vector = new Vector2(velocity.X, velocity.Y).SafeNormalize(-Vector2.UnitY);
		Vector2 vector2 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101)).SafeNormalize(-Vector2.UnitY);
		vector = (vector * 4f + vector2).SafeNormalize(-Vector2.UnitY) * Item.shootSpeed;
		float num = (float)Main.rand.Next(10, 80) * 0.001f;
		if (Main.rand.Next(2) == 0)
		{
			num *= -1f;
		}
		float num2 = (float)Main.rand.Next(10, 80) * 0.001f;
		if (Main.rand.Next(2) == 0)
		{
			num2 *= -1f;
		}
		Projectile.NewProjectile(null, position, vector, type, damage, knockback, player.whoAmI, num, num2);
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "DarkMatter", 32);
		val.AddIngredient((Mod)null, "EldritchBlood", 8);
		val.AddTile(412);
		val.Register();
	}
}
