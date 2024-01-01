namespace Dawn.DMD.StarCruxExpansion.UI;

using UnityEngine;

public readonly struct AffordanceOptions(string identifier, GameObject location, Vector3 coordinates)
{
    public readonly string Identifier = identifier;
    public readonly GameObject Location = location;
    public readonly Vector3 Coordinates = coordinates;
}