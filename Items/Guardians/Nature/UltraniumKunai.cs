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
		//Tooltip.SetDefault("Throws homing Ultranium kunai blades");
		//DisplayName.SetDefault("Ultranium Kunai");
	}

	public override void SetDefaults()
	{
		Item.damage = 230;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 42;
		Item.height = 42;
		Item.useTime = 15;
		Item.useAnimation = 15;
		Item.useStyle = 1;
		Item.knockBack = 8f;
		Item.noUseGraphic = true;
		Item.rare = 11;
		Item.value = Item.buyPrice(1);
		Item.UseSound = SoundID.Item60;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("UltraniumKunai").Type;
		Item.shootSpeed = 15f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(241, 166, 0);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "UltrumShard", 10);
		val.AddTile(412);
		val.Register();
	}
}
