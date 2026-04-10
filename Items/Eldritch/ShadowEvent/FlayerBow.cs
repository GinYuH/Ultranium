using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.ShadowEvent;

public class FlayerBow : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Life's Diminish");
		// Tooltip.SetDefault("Converts all arrows into dark matter arrow bolts");
	}

	public override void SetDefaults()
	{
		Item.damage = 170;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 46;
		Item.height = 18;
		Item.useTime = 14;
		Item.useAnimation = 14;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 4f;
		Item.value = Item.buyPrice(1);
		Item.rare = 11;
		Item.UseSound = SoundID.Item5;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("DarkMatterArrowBolt").Type;
		Item.shootSpeed = 16f;
		Item.useAmmo = AmmoID.Arrow;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-3f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		for (int i = 0; i < 1; i++)
		{
			Projectile.NewProjectile(null, position.X, position.Y, speedX, speedY, Mod.Find<ModProjectile>("DarkMatterArrowBolt").Type, Item.damage, knockBack, Item.playerIndexTheItemIsReservedFor, 0f, 0f);
		}
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "DarkMatter", 32);
		val.AddIngredient((Mod)null, "EldritchBlood", 8);
		val.AddTile(412);
		val.Register();
	}
}
