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
		// ((ModItem)this).DisplayName.SetDefault("Flayer's Lament");
		// ((ModItem)this).Tooltip.SetDefault("Creates piercing tentacles");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 140;
		((ModItem)this).Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((Entity)(object)((ModItem)this).Item).width = 80;
		((Entity)(object)((ModItem)this).Item).height = 80;
		((ModItem)this).Item.useTime = 25;
		((ModItem)this).Item.useAnimation = 25;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.value = Item.buyPrice(1);
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.UseSound = SoundID.Item1;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("DarkTentacle").Type;
		((ModItem)this).Item.shootSpeed = 24f;
		((ModItem)this).Item.autoReuse = true;
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
