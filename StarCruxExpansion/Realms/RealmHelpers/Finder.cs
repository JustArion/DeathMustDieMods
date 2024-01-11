namespace Dawn.DMD.StarCruxExpansion.Realms.RealmHelpers;

using Death.Darkness;
using Reflection;
using UI;

public static class Finder
{
    public static RealmData GetRealm(string realmName) =>
        ModdedRealmManager.moddedRealms.First(x => x.RealmName == realmName);

    public static DarknessOptions.Challenge GetRealmChallenge(string realmName, string challengeId)
        => ModdedRealmManager.moddedRealms.First(x => x.RealmName == realmName).options.Challenges()
            .First(x => x.Code == "sce_" + challengeId);
    
    public static DarknessOptions.Challenge GetRealmChallenge(RealmData realm, string challengeId)
        => realm.options.Challenges()
            .First(x => x.Code == "sce_" + challengeId);
}