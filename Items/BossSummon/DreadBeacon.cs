using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class DreadBeacon : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Beacon of Fear");
		//Tooltip.SetDefault("The flame is oddly cold...\nSummons Dread");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;
		Item.maxStack = 20;
		Item.rare = ItemRarityID.LightRed;
		Item.useAnimation = 45;
		Item.useTime = 45;
		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.UseSound = SoundID.Item44;
		Item.consumable = true;
	}

	public override bool CanUseItem(Player player)
	{
		if (!NPC.AnyNPCs(Mod.Find<ModNPC>("DreadBoss").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("DreadBossP2").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("FakeDread").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("TrueDread").Type))
		{
			return !Main.dayTime;
		}
		return false;
	}

	public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
	{
		if (UltraniumWorld.downedUltrum && UltraniumWorld.downedIgnodium && !UltraniumWorld.downedTrueDread)
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("FakeDread").Type);
		}
		else if (UltraniumWorld.downedUltrum && UltraniumWorld.downedIgnodium && UltraniumWorld.downedTrueDread)
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("TrueDread").Type);
		}
		else
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("DreadBoss").Type);
		}
		SoundEngine.PlaySound(SoundID.Roar, player.position);
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "DreadFlame", 15);
		val.AddRecipeGroup("Ultranium:Adamantite/Titanium", 5);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
