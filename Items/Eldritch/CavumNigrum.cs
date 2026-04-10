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
		// DisplayName.SetDefault("Cavum Nigrum");
		// Tooltip.SetDefault("Throws out eldritch discs\nHas a chance to create lingering images of the disc on enemy hits");
	}

	public override void SetDefaults()
	{
		Item.damage = 260;
		Item.DamageType = DamageClass.Ranged;
		Item.crit = 10;
		Item.width = 42;
		Item.height = 42;
		Item.useTime = 20;
		Item.useAnimation = 20;
		Item.useStyle = 1;
		Item.knockBack = 8f;
		Item.noUseGraphic = true;
		Item.rare = 11;
		Item.value = Item.buyPrice(1, 50);
		Item.UseSound = SoundID.Item60;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("CavumNigrum").Type;
		Item.shootSpeed = 3f;
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
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "NightmareScale", 8);
		val.AddIngredient((Mod)null, "NightmareBar", 12);
		val.AddIngredient((Mod)null, "DarkMatter", 10);
		val.AddTile(412);
		val.Register();
	}
}
