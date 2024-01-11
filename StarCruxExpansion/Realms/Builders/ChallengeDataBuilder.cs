namespace Dawn.DMD.StarCruxExpansion.Realms.Builders;

using System.Collections.Generic;
using Death.Darkness;
using Helpers;

public class ChallengeDataBuilder
{
    private string _title = string.Empty;
    private string _description = string.Empty;
    private string _code = string.Empty;
    private uint _maxLevel = 1;
    private uint _pointsPerLevel = 1;
    private uint _winsToUnlock;
    private string _iconPath = ChallengeDataEx.ToIconPath(ChallengeDataEx.ChallengeDataIcon.Angle);
    private readonly List<ChallengeDataMonsterEffectBuilder> _effectBuilders = [];

    public ChallengeDataBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public ChallengeDataBuilder WithDescription(string descriptionFormat)
    {
        _description = descriptionFormat;
        return this;
    }

    public ChallengeDataBuilder WithCode(string challengeCode)
    {
        _code = "sce_" + challengeCode;
        return this;
    }

    public ChallengeDataBuilder WithMaxLevel(uint maxLevel)
    {
        _maxLevel = maxLevel;
        return this;
    }

    public ChallengeDataBuilder WithPointsPerLevel(uint pointsPerLevel)
    {
        _pointsPerLevel = pointsPerLevel;
        return this;
    }

    public ChallengeDataBuilder WithWinsToUnlock(uint winsToUnlock)
    {
        _winsToUnlock = winsToUnlock;
        return this;
    }

    public ChallengeDataBuilder WithIcon(ChallengeDataEx.ChallengeDataIcon icon)
    {
        _iconPath = ChallengeDataEx.ToIconPath(icon);
        return this;
    }

    private readonly List<ChallengeData.Effect> _customEffects = [];
    public ChallengeDataBuilder WithCustomEffect(GlobalEffectCreator creator)
    {
        _customEffects.Add(new (new ModdedStatArray<float>(), creator));
        return this;
    }

    public ChallengeDataMonsterEffectBuilder WithEffect()
    {
        var effectBuilder = new ChallengeDataMonsterEffectBuilder();
        _effectBuilders.Add(effectBuilder);
        return effectBuilder;
    }

    internal ChallengeDataInformation Build()
    {
        _code ??= Guid.NewGuid().ToString();
        var effects = _effectBuilders.Select(builder => builder.Build()).ToList();
        effects.AddRange(_customEffects);

        return new((_title, _description),
            new(_code, (int)_maxLevel, (int)_pointsPerLevel, (int)_winsToUnlock, _iconPath, effects));
    }
}