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
		DisplayName.SetDefault("Soul Harvester");
		Tooltip.SetDefault("Throws fast moving tentacle scythe blades\nThese scythe blades will create a circle of tentacles upon death");
	}

	public override void SetDefaults()
	{
		Item.value = Item.buyPrice(1, 50);
		Item.damage = 230;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.crit = 15;
		Item.width = 42;
		Item.height = 42;
		Item.useTime = 20;
		Item.useAnimation = 20;
		Item.useStyle = 1;
		Item.knockBack = 8f;
		Item.noUseGraphic = true;
		Item.rare = 11;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("EldritchScythe").Type;
		Item.shootSpeed = 10f;
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
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "DarkMatter", 32);
		val.AddIngredient((Mod)null, "EldritchBlood", 8);
		val.AddTile(412);
		val.Register();
	}
}
