using System.Collections.Generic;
using System.Linq;

namespace Planner.Library.HoleProcess;

public class HolePlanner
{
    private static readonly List<int> _allChoices = new()
    {
        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17
    };

    public int SelectedBlankType { get; set; }
    public int SelectedEcoPrecision { get; set; }
    public int SelectedMaterial { get; set; }

    public double SurfaceRoughness { get; set; }
    public double HoleDiameter { get; set; }

    public bool BulkProduction { get; set; }
    public bool HighQualityHole { get; set; }
    public bool HighQualityNonFerrousMetal { get; set; }

    private IEnumerable<int> EcoPrecisionChoices()
    {
        return SelectedEcoPrecision switch {
            0 => _allChoices,
            < 5 => new List<int>(),
            6 => new List<int> { 7 },
            7 => new List<int>
            {
                3,
                6,
                7,
                8,
                12,
                13,
                14,
                15,
                16,
                17
            },
            8 => new List<int>
            {
                3,
                5,
                11,
                12,
                13
            },
            9 => new List<int> { 5, 10, 11 },
            10 => new List<int> { 2, 10 },
            11 => new List<int>(),
            12 => new List<int> { 1, 4, 9 },
            _ => new List<int>()
        };
    }

    private IEnumerable<int> BlankTypeChoices()
    {
        switch (SelectedBlankType)
        {
            case 0:
                return _allChoices;
            case 1:
                return new List<int> { 1, 2, 3, 4, 5, 6, 7, /**/ 8 };
            case 2:
                return new List<int> { 9, 10, 11, 12, /**/ 8 };
            case 3:
                return new List<int> { 9, 10, 11, 12, /**/ 8 };
            default:
                return new List<int>();
        }
    }

    private IEnumerable<int> MaterialChoices()
    {
        return SelectedMaterial switch {
            0 => _allChoices,
            1 => new List<int> { 8, 13, 14, 15, 16, 17 },
            2 => new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 16, 17 },
            3 => new List<int> { 8, 9, 10, 11, 12, 15, 16, 17 },
            _ => new List<int>()
        };
    }

    private IEnumerable<int> SurfaceRoughnessChoices()
    {
        switch (SurfaceRoughness)
        {
            case 0.0:
                return _allChoices;
            case < 0.0 or > 20.0:
                return new List<int>();
        }

        IEnumerable<int> choices = new List<int>();

        if (SurfaceRoughness is >= 0.02 and <= 0.32)
        {
            choices = choices.Union(new List<int> { 16, 17 });
        }

        if (SurfaceRoughness is >= 0.08 and <= 0.63)
        {
            choices = choices.Union(new List<int> { 7, 14, 15 });
        }

        if (SurfaceRoughness is >= 0.08 and <= 2.5)
        {
            choices = choices.Union(new List<int> { 8 });
        }

        if (SurfaceRoughness is >= 0.16 and <= 1.25)
        {
            choices = choices.Union(new List<int> { 13 });
        }

        if (SurfaceRoughness is >= 0.32 and <= 1.25)
        {
            choices = choices.Union(new List<int> { 12 });
        }

        if (SurfaceRoughness is >= 0.63 and <= 2.5)
        {
            choices = choices.Union(new List<int> { 3, 6 });
        }

        if (SurfaceRoughness is >= 1.25 and <= 5)
        {
            choices = choices.Union(new List<int> { 2, 5, 11 });
        }

        if (SurfaceRoughness is >= 2.5 and <= 10)
        {
            choices = choices.Union(new List<int> { 10 });
        }

        if (SurfaceRoughness is >= 5 and <= 20)
        {
            choices = choices.Union(new List<int> { 4, 9 });
        }

        if (SurfaceRoughness is >= 10 and <= 20)
        {
            choices = choices.Union(new List<int> { 1 });
        }

        return choices;
    }

    private IEnumerable<int> HoleDiameterChoices()
    {
        switch (HoleDiameter)
        {
            case 0:
                return _allChoices;
            case < 0.0:
                return new List<int>();
        }

        IEnumerable<int> choices = new List<int> { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
        if (HoleDiameter <= 20)
        {
            choices = choices.Union(new List<int> { 1, 2, 3 });
        }
        if (HoleDiameter >= 15)
        {
            choices = choices.Union(new List<int> { 4, 5, 6, 7 });
        }

        return choices;
    }

    private IEnumerable<int> BulkProductionChoices()
    {
        if (BulkProduction)
        {
            return new List<int> { 8 };
        }

        return _allChoices.Except(new List<int> { 8 });
    }

    private IEnumerable<int> HighQualityHoleChoices()
    {
        if (HighQualityHole)
        {
            return new List<int> { 16, 17 };
        }

        return _allChoices.Except(new List<int> { 16, 17 });
    }

    private IEnumerable<int> HighQualityNonFerrousMetalChoices()
    {
        if (HighQualityNonFerrousMetal)
        {
            return new List<int> { 15 };
        }

        return _allChoices.Except(new List<int> { 15 });
    }

    public IEnumerable<int> GetPossibleProcess()
    {
        IEnumerable<int> possibleProcess = new List<int>(_allChoices);

        return possibleProcess.Intersect(EcoPrecisionChoices())
            .Intersect(BlankTypeChoices())
            .Intersect(MaterialChoices())
            .Intersect(SurfaceRoughnessChoices())
            .Intersect(HoleDiameterChoices())
            .Intersect(BulkProductionChoices())
            .Intersect(HighQualityHoleChoices())
            .Intersect(HighQualityNonFerrousMetalChoices());
    }
}