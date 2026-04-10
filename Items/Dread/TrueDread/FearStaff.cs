using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.TrueDread;

public class FearStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Staff of Horror");
		((ModItem)this).Tooltip.SetDefault("Casts dread energy bolts");
		Item.staff[((ModItem)this).item.type] = true;
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 230;
		((ModItem)this).item.magic = true;
		((ModItem)this).item.mana = 12;
		((Entity)(object)((ModItem)this).item).width = 58;
		((Entity)(object)((ModItem)this).item).height = 56;
		((ModItem)this).item.useTime = 13;
		((ModItem)this).item.useAnimation = 13;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 5f;
		((ModItem)this).item.rare = 4;
		((ModItem)this).item.value = Item.buyPrice(1);
		((ModItem)this).item.UseSound = SoundID.Item20;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("DreadWaveBolt");
		((ModItem)this).item.shootSpeed = 12f;
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
