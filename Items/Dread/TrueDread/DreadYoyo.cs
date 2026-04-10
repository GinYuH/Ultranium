using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.TrueDread;

public class DreadYoyo : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("The Toothball");
		((ModItem)this).Tooltip.SetDefault("Has a chance to shoot circles of dread teeth on enemy hits");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 230;
		((ModItem)this).item.rare = 11;
		((Entity)(object)((ModItem)this).item).width = 24;
		((Entity)(object)((ModItem)this).item).height = 24;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.useAnimation = 25;
		((ModItem)this).item.useTime = 25;
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.noUseGraphic = true;
		((ModItem)this).item.melee = true;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.channel = true;
		((ModItem)this).item.value = Item.buyPrice(1);
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("DreadYoyo");
		((ModItem)this).item.shootSpeed = 16f;
		((ModItem)this).item.knockBack = 2.5f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(200, 0, 0);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "NightmareFuel", 10);
		val.AddIngredient((Mod)null, "DreadScale", 6);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
