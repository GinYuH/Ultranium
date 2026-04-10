using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch;

public class CavumNigrum : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Cavum Nigrum");
		// ((ModItem)this).Tooltip.SetDefault("Throws out eldritch discs\nHas a chance to create lingering images of the disc on enemy hits");
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
		((ModItem)this).Item.value = Item.buyPrice(1, 50);
		((ModItem)this).Item.UseSound = SoundID.Item60;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("CavumNigrum").Type;
		((ModItem)this).Item.shootSpeed = 3f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "NightmareScale", 8);
		val.AddIngredient((Mod)null, "NightmareBar", 12);
		val.AddIngredient((Mod)null, "DarkMatter", 10);
		val.AddTile(412);
		val.Register();
	}
}
