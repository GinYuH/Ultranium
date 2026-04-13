using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.ShadowEvent;

public class EldritchTome : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Yawning Abyss");
		//Tooltip.SetDefault("Casts abyssal vortexes");
	}

	public override void SetDefaults()
	{
		Item.damage = 200;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 13;
		Item.width = 40;
		Item.height = 40;
		Item.useTime = 22;
		Item.useAnimation = 22;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.value = Item.buyPrice(1, 50);
		Item.rare = ItemRarityID.Purple;
		Item.UseSound = SoundID.Item84;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("EldritchVortex").Type;
		Item.shootSpeed = 16f;
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
		val.AddTile(TileID.LunarCraftingStation);
		val.Register();
	}
}
