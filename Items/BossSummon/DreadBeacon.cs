using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class DreadBeacon : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Beacon of Fear");
		// ((ModItem)this).Tooltip.SetDefault("The flame is oddly cold...\nSummons Dread");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 20;
		((Entity)(object)((ModItem)this).Item).height = 20;
		((ModItem)this).Item.maxStack = 20;
		((ModItem)this).Item.rare = 4;
		((ModItem)this).Item.useAnimation = 45;
		((ModItem)this).Item.useTime = 45;
		((ModItem)this).Item.useStyle = 4;
		((ModItem)this).Item.UseSound = SoundID.Item44;
		((ModItem)this).Item.consumable = true;
	}

	public override bool CanUseItem(Player player)
	{
		if (!NPC.AnyNPCs(((ModItem)this).Mod.Find<ModNPC>("DreadBoss").Type) && !NPC.AnyNPCs(((ModItem)this).Mod.Find<ModNPC>("DreadBossP2").Type) && !NPC.AnyNPCs(((ModItem)this).Mod.Find<ModNPC>("FakeDread").Type) && !NPC.AnyNPCs(((ModItem)this).Mod.Find<ModNPC>("TrueDread").Type))
		{
			return !Main.dayTime;
		}
		return false;
	}

	public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
	{
		if (UltraniumWorld.downedUltrum && UltraniumWorld.downedIgnodium && !UltraniumWorld.downedTrueDread)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ((ModItem)this).Mod.Find<ModNPC>("FakeDread").Type);
		}
		else if (UltraniumWorld.downedUltrum && UltraniumWorld.downedIgnodium && UltraniumWorld.downedTrueDread)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ((ModItem)this).Mod.Find<ModNPC>("TrueDread").Type);
		}
		else
		{
			NPC.SpawnOnPlayer(player.whoAmI, ((ModItem)this).Mod.Find<ModNPC>("DreadBoss").Type);
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
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "DreadFlame", 15);
		val.AddRecipeGroup("Ultranium:Adamantite/Titanium", 5);
		val.AddTile(134);
		val.Register();
	}
}
