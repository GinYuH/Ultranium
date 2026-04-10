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
		((ModItem)this).DisplayName.SetDefault("Hell's Fury");
		((ModItem)this).Tooltip.SetDefault("Creates a barrage of flame blasts");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 155;
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 20;
		((ModItem)this).item.useAnimation = 24;
		((ModItem)this).item.useTime = 24;
		((ModItem)this).item.knockBack = 15f;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.value = Item.buyPrice(1);
		((ModItem)this).item.melee = true;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.noUseGraphic = true;
		((ModItem)this).item.UseSound = SoundID.Item10;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("HellFlail");
		((ModItem)this).item.shootSpeed = 14f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(241, 166, 0);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "HellShard", 10);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
