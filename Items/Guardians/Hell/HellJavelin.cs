using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Hell;

public class HellJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Igneous Impaler");
		// Tooltip.SetDefault("Throws fast moving hell javelins\nExplodes into lingering flames upon enemy hits");
	}

	public override void SetDefaults()
	{
		Item.damage = 210;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 56;
		Item.height = 56;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = 1;
		Item.knockBack = 8f;
		Item.noUseGraphic = true;
		Item.value = Item.buyPrice(1);
		Item.rare = 11;
		Item.UseSound = SoundID.Item60;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("HellJavelin").Type;
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
		val.AddIngredient((Mod)null, "HellShard", 10);
		val.AddTile(412);
		val.Register();
	}
}
