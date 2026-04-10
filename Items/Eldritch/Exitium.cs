using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch;

public class Exitium : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Exitium");
		// ((ModItem)this).Tooltip.SetDefault("Fires tentacles when enemies are nearby");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 260;
		((ModItem)this).Item.rare = 11;
		((Entity)(object)((ModItem)this).Item).width = 24;
		((Entity)(object)((ModItem)this).Item).height = 24;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.useAnimation = 25;
		((ModItem)this).Item.useTime = 25;
		((ModItem)this).Item.UseSound = SoundID.Item1;
		((ModItem)this).Item.noUseGraphic = true;
		((ModItem)this).Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.channel = true;
		((ModItem)this).Item.value = Item.buyPrice(1, 50);
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("Exitium").Type;
		((ModItem)this).Item.shootSpeed = 16f;
		((ModItem)this).Item.knockBack = 2.5f;
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
