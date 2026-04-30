using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Nature;

public class UltraniumScepter : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Nature Scepter");
		//Tooltip.SetDefault("Summons a miniature ultrum to fight with you\nOnly one can be summoned at once\nDoes not take up any minion slots");
	}

	public override void SetDefaults()
	{
		Item.mana = 35;
		Item.damage = 200;
		Item.width = 26;
		Item.height = 26;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.noMelee = true;
		Item.DamageType = DamageClass.Summon;
		Item.knockBack = 0f;
		Item.value = Item.buyPrice(1);
		Item.rare = ItemRarityID.Purple;
		Item.UseSound = SoundID.Item44;
		Item.shoot = Mod.Find<ModProjectile>("Ultrum").Type;
		Item.shootSpeed = 10f;
		Item.buffType = Mod.Find<ModBuff>("UltrumBuff").Type;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(241, 166, 0);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Ultrum").Type] > 0)
		{
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "UltrumShard", 10);
		val.AddTile(TileID.LunarCraftingStation);
		val.Register();
	}
}
