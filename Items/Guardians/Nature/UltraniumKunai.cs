using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Nature;

public class UltraniumKunai : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).Tooltip.SetDefault("Throws homing Ultranium kunai blades");
		((ModItem)this).DisplayName.SetDefault("Ultranium Kunai");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 230;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 42;
		((Entity)(object)((ModItem)this).item).height = 42;
		((ModItem)this).item.useTime = 15;
		((ModItem)this).item.useAnimation = 15;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 8f;
		((ModItem)this).item.noUseGraphic = true;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.value = Item.buyPrice(1);
		((ModItem)this).item.UseSound = SoundID.Item60;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("UltraniumKunai");
		((ModItem)this).item.shootSpeed = 15f;
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
		val.AddIngredient((Mod)null, "UltrumShard", 10);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
