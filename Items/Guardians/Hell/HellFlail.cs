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
		// ((ModItem)this).DisplayName.SetDefault("Hell's Fury");
		// ((ModItem)this).Tooltip.SetDefault("Creates a barrage of flame blasts");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 155;
		((Entity)(object)((ModItem)this).Item).width = 20;
		((Entity)(object)((ModItem)this).Item).height = 20;
		((ModItem)this).Item.useAnimation = 24;
		((ModItem)this).Item.useTime = 24;
		((ModItem)this).Item.knockBack = 15f;
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.value = Item.buyPrice(1);
		((ModItem)this).Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.noUseGraphic = true;
		((ModItem)this).Item.UseSound = SoundID.Item10;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("HellFlail").Type;
		((ModItem)this).Item.shootSpeed = 14f;
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
