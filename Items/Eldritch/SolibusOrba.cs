using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch;

public class SolibusOrba : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Solibus Orba");
		// ((ModItem)this).Tooltip.SetDefault("Throws dark spears that chase down enemies");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 64;
		((Entity)(object)((ModItem)this).Item).height = 64;
		((ModItem)this).Item.damage = 250;
		((ModItem)this).Item.knockBack = 9f;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.useTime = 17;
		((ModItem)this).Item.useAnimation = 17;
		((ModItem)this).Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.noUseGraphic = true;
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.value = Item.buyPrice(1, 50);
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("SolibusOrba").Type;
		((ModItem)this).Item.shootSpeed = 15f;
		((ModItem)this).Item.UseSound = SoundID.Item7;
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
