namespace Dawn.DMD.StarCruxExpansion.Realms.Builders;

using System.Collections.Generic;
using System.Diagnostics;
using Death.Darkness;
using Death.Run.Core;
using Death.Utils.Collections;

public class ChallengeDataMonsterEffectBuilder
{
    private readonly Dictionary<StatId, float> _statArray = [];
    private readonly EnumArray<MonsterType, bool> _monsterMask = [];
    private StatChangeType _statChangeType;
    private StatId? _primaryStat;

    public ChallengeDataMonsterEffectBuilder WithStatPerLevel(StatId statId, float statEveryLevel)
    {
        _statArray.Add(statId, statEveryLevel);
        return this;
    }

    public ChallengeDataMonsterEffectBuilder WithPrimaryStat(StatId primaryStat)
    {
        _primaryStat = primaryStat;
        return this;
    }

    public ChallengeDataMonsterEffectBuilder WithChangeType(StatChangeType changeType)
    {
        _statChangeType = changeType;
        return this;
    }

    public ChallengeDataMonsterEffectBuilder AffectsAllMonsters()
    {
        _monsterMask.SetAll(true);
        return this;
    }
    public ChallengeDataMonsterEffectBuilder AlsoAffects(MonsterType monsterType)
    {
        _monsterMask.Set(monsterType, true);
        return this;
    }
    
    public ChallengeDataMonsterEffectBuilder DoesntAffect(MonsterType monsterType)
    {
        _monsterMask.Set(monsterType, false);
        return this;
    }

    
    
    internal ChallengeData.Effect Build()
    {
        var statArray = _statArray.Select(x => (x.Key, x.Value)).ToArray();

        var requiredStat = _statChangeType is StatChangeType.Flat or StatChangeType.Bonus 
            ? StatId.EffectValue 
            : StatId.EffectPercent;
        var primaryStat = _primaryStat ?? statArray.First().Key;
        
        var statsPerLevel = new ModdedStatArray<float>(statArray);
        var creator = new GlobalEffectCreator(requiredStats: [requiredStat],
            stats =>
            {
                var value = stats.Get(primaryStat);
                
                return new GlobalEffect_MonsterStatChange(new(), primaryStat, value,
                    _statChangeType, _monsterMask);
            });

        return new ChallengeData.Effect(statsPerLevel, creator);
    }
}