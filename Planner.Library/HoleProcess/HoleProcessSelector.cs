using System.Collections.Generic;
using System.Linq;

namespace Planner.Library.HoleProcess;

public class HoleProcessSelector
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

    private List<int> BlankTypeChoices()
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

    private List<int> EcoPrecisionChoices()
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

    public IEnumerable<int> GetPossibleProcess()
    {
        List<int> possibleProcess = new(_allChoices);

        return possibleProcess.Intersect(EcoPrecisionChoices())
            .Intersect(BlankTypeChoices());
    }
}