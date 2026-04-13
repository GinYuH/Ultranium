using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class EtherealLantern : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ethereal Lantern");
		//Tooltip.SetDefault("Calls forth the ethereal demon\nCan only be used at night");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;
		Item.maxStack = 20;
		Item.rare = ItemRarityID.Pink;
		Item.useAnimation = 45;
		Item.useTime = 45;
		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.UseSound = SoundID.Item44;
		Item.consumable = true;
	}

	public override bool CanUseItem(Player player)
	{
		if (!NPC.AnyNPCs(Mod.Find<ModNPC>("Xenanis").Type))
		{
			return !Main.dayTime;
		}
		return false;
	}

	public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
	{
		NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Xenanis").Type);
		SoundEngine.PlaySound(SoundID.Roar, player.position);
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(ItemID.Ectoplasm, 10);
		val.AddIngredient(null, "ShadowFlame", 10);
		val.AddIngredient(ItemID.Bone, 30);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
