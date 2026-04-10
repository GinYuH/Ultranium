using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.TrueDread;

public class DreadDisc : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Disc of Dismay");
		((ModItem)this).Tooltip.SetDefault("Throws out discs that scatter dread bolts in random directions\nOnly up to 3 discs can be active at once");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 260;
		((ModItem)this).item.ranged = true;
		((ModItem)this).item.crit = 10;
		((Entity)(object)((ModItem)this).item).width = 42;
		((Entity)(object)((ModItem)this).item).height = 42;
		((ModItem)this).item.useTime = 20;
		((ModItem)this).item.useAnimation = 20;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 8f;
		((ModItem)this).item.noUseGraphic = true;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.value = Item.buyPrice(1);
		((ModItem)this).item.UseSound = SoundID.Item60;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("DreadDisc");
		((ModItem)this).item.shootSpeed = 15f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[((ModItem)this).item.shoot] < 3;
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
