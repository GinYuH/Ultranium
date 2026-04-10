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
		// ((ModItem)this).DisplayName.SetDefault("Disc of Dismay");
		// ((ModItem)this).Tooltip.SetDefault("Throws out discs that scatter dread bolts in random directions\nOnly up to 3 discs can be active at once");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 260;
		((ModItem)this).Item.DamageType = DamageClass.Ranged;
		((ModItem)this).Item.crit = 10;
		((Entity)(object)((ModItem)this).Item).width = 42;
		((Entity)(object)((ModItem)this).Item).height = 42;
		((ModItem)this).Item.useTime = 20;
		((ModItem)this).Item.useAnimation = 20;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 8f;
		((ModItem)this).Item.noUseGraphic = true;
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.value = Item.buyPrice(1);
		((ModItem)this).Item.UseSound = SoundID.Item60;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("DreadDisc").Type;
		((ModItem)this).Item.shootSpeed = 15f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[((ModItem)this).Item.shoot] < 3;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(200, 0, 0);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "NightmareFuel", 10);
		val.AddIngredient((Mod)null, "DreadScale", 6);
		val.AddTile(412);
		val.Register();
	}
}
