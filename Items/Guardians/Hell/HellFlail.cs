using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Hell;

public class HellFlail : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Hell's Fury");
		// Tooltip.SetDefault("Creates a barrage of flame blasts");
	}

	public override void SetDefaults()
	{
		Item.damage = 155;
		Item.width = 20;
		Item.height = 20;
		Item.useAnimation = 24;
		Item.useTime = 24;
		Item.knockBack = 15f;
		Item.rare = 11;
		Item.value = Item.buyPrice(1);
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.noMelee = true;
		Item.noUseGraphic = true;
		Item.UseSound = SoundID.Item10;
		Item.autoReuse = true;
		Item.useStyle = 5;
		Item.shoot = Mod.Find<ModProjectile>("HellFlail").Type;
		Item.shootSpeed = 14f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(241, 166, 0);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "HellShard", 10);
		val.AddTile(412);
		val.Register();
	}
}
