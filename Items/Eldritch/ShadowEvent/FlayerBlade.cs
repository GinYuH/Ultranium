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
		// DisplayName.SetDefault("Flayer's Lament");
		// Tooltip.SetDefault("Creates piercing tentacles");
	}

	public override void SetDefaults()
	{
		Item.damage = 140;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.width = 80;
		Item.height = 80;
		Item.useTime = 25;
		Item.useAnimation = 25;
		Item.useStyle = 1;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(1);
		Item.rare = 11;
		Item.UseSound = SoundID.Item1;
		Item.shoot = Mod.Find<ModProjectile>("DarkTentacle").Type;
		Item.shootSpeed = 24f;
		Item.autoReuse = true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "DarkMatter", 32);
		val.AddIngredient((Mod)null, "EldritchBlood", 8);
		val.AddTile(412);
		val.Register();
	}
}
