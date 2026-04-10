using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class DreadBeacon : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Beacon of Fear");
		((ModItem)this).Tooltip.SetDefault("The flame is oddly cold...\nSummons Dread");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 20;
		((ModItem)this).item.maxStack = 20;
		((ModItem)this).item.rare = 4;
		((ModItem)this).item.useAnimation = 45;
		((ModItem)this).item.useTime = 45;
		((ModItem)this).item.useStyle = 4;
		((ModItem)this).item.UseSound = SoundID.Item44;
		((ModItem)this).item.consumable = true;
	}

	public override bool CanUseItem(Player player)
	{
		if (!NPC.AnyNPCs(((ModItem)this).mod.NPCType("DreadBoss")) && !NPC.AnyNPCs(((ModItem)this).mod.NPCType("DreadBossP2")) && !NPC.AnyNPCs(((ModItem)this).mod.NPCType("FakeDread")) && !NPC.AnyNPCs(((ModItem)this).mod.NPCType("TrueDread")))
		{
			return !Main.dayTime;
		}
		return false;
	}

	public override bool UseItem(Player player)
	{
		if (UltraniumWorld.downedUltrum && UltraniumWorld.downedIgnodium && !UltraniumWorld.downedTrueDread)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ((ModItem)this).mod.NPCType("FakeDread"));
		}
		else if (UltraniumWorld.downedUltrum && UltraniumWorld.downedIgnodium && UltraniumWorld.downedTrueDread)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ((ModItem)this).mod.NPCType("TrueDread"));
		}
		else
		{
			NPC.SpawnOnPlayer(player.whoAmI, ((ModItem)this).mod.NPCType("DreadBoss"));
		}
		Main.PlaySound(15, player.position, 0);
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "DreadFlame", 15);
		val.AddRecipeGroup("Ultranium:Adamantite/Titanium", 5);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
