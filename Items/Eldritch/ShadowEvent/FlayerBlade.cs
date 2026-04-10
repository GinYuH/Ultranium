using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.ShadowEvent;

public class FlayerBlade : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Flayer's Lament");
		((ModItem)this).Tooltip.SetDefault("Creates piercing tentacles");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 140;
		((ModItem)this).item.melee = true;
		((Entity)(object)((ModItem)this).item).width = 80;
		((Entity)(object)((ModItem)this).item).height = 80;
		((ModItem)this).item.useTime = 25;
		((ModItem)this).item.useAnimation = 25;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(1);
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("DarkTentacle");
		((ModItem)this).item.shootSpeed = 24f;
		((ModItem)this).item.autoReuse = true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(34, 166, 118);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "DarkMatter", 32);
		val.AddIngredient((Mod)null, "EldritchBlood", 8);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
