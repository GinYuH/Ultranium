using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch;

public class Umbra : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Umbra");
		//Tooltip.SetDefault("Casts eldritch blasts");
	}

	public override void SetDefaults()
	{
		Item.damage = 220;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 13;
		Item.width = 40;
		Item.height = 40;
		Item.useTime = 17;
		Item.useAnimation = 17;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.rare = ItemRarityID.Purple;
		Item.value = Item.buyPrice(1, 50);
		Item.UseSound = SoundID.Item20;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("NoctisBlast").Type;
		Item.shootSpeed = 11f;
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
		val.AddTile(TileID.LunarCraftingStation);
		val.Register();
	}
}
