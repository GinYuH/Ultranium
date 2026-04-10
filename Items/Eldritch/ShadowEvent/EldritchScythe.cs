using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.ShadowEvent;

public class EldritchScythe : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Soul Harvester");
		((ModItem)this).Tooltip.SetDefault("Throws fast moving tentacle scythe blades\nThese scythe blades will create a circle of tentacles upon death");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.value = Item.buyPrice(1, 50);
		((ModItem)this).item.damage = 230;
		((ModItem)this).item.melee = true;
		((ModItem)this).item.crit = 15;
		((Entity)(object)((ModItem)this).item).width = 42;
		((Entity)(object)((ModItem)this).item).height = 42;
		((ModItem)this).item.useTime = 20;
		((ModItem)this).item.useAnimation = 20;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 8f;
		((ModItem)this).item.noUseGraphic = true;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("EldritchScythe");
		((ModItem)this).item.shootSpeed = 10f;
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
