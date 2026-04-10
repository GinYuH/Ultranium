using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch;

public class SolibusOrba : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Solibus Orba");
		((ModItem)this).Tooltip.SetDefault("Throws dark spears that chase down enemies");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 64;
		((Entity)(object)((ModItem)this).item).height = 64;
		((ModItem)this).item.damage = 250;
		((ModItem)this).item.knockBack = 9f;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.useTime = 17;
		((ModItem)this).item.useAnimation = 17;
		((ModItem)this).item.melee = true;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.noUseGraphic = true;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.value = Item.buyPrice(1, 50);
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("SolibusOrba");
		((ModItem)this).item.shootSpeed = 15f;
		((ModItem)this).item.UseSound = SoundID.Item7;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(34, 166, 118);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "NightmareScale", 8);
		val.AddIngredient((Mod)null, "NightmareBar", 12);
		val.AddIngredient((Mod)null, "DarkMatter", 10);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
