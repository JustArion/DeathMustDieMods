namespace Dawn.DMD.StarCruxExpansion.Realms.Persistence;

using UI;

public static class RealmStateManager
{
    public static void SaveAll()
    {
        foreach (var realm in ModdedRealmManager.moddedRealms)
        {
            var darknessOptions = realm.options;

            RealmData.SaveToConfig(realm.RealmName, darknessOptions);
        }
    }
}