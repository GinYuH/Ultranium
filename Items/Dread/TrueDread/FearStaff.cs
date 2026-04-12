using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.TrueDread;

public class FearStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Staff of Horror");
		Tooltip.SetDefault("Casts dread energy bolts");
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.damage = 230;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 12;
		Item.width = 58;
		Item.height = 56;
		Item.useTime = 13;
		Item.useAnimation = 13;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.rare = 4;
		Item.value = Item.buyPrice(1);
		Item.UseSound = SoundID.Item20;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("DreadWaveBolt").Type;
		Item.shootSpeed = 12f;
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
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "NightmareFuel", 10);
		val.AddIngredient((Mod)null, "DreadScale", 6);
		val.AddTile(412);
		val.Register();
	}
}
