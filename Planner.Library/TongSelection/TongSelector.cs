using System.Collections.Generic;

namespace Planner.Library.TongSelection;

public class TongSelector
{
    private static readonly List<int> _allMethos = new() {
        0, 1, 2, 3, 4, 5, 6, 7, 8
    };

    public int ProcessMethod { get; set; }
    public double LengthDiameterRatio { get; set; }
    public int NumFaces { get; set; }
    public bool IsSolid { get; set; }
    public int ProcessSide { get; set; }

    public IEnumerable<int> GetPossibleMethods()
    {
        switch (ProcessMethod)
        {
            case 0:
                return _allMethos;
            case 1:
                return LengthDiameterRatio switch {
                    0 => new List<int> { 0, 1, 2 },
                    < 4 => new List<int> { 0 },
                    < 16 => new List<int> { 1 },
                    _ => new List<int> { 2 }
                };
            case 2:
                return NumFaces switch {
                    0 => new List<int> { 3, 4 },
                    1 => new List<int> { 3 },
                    _ => new List<int> { 4 }
                };
            case 3:
                switch (LengthDiameterRatio)
                {
                    case 0.0:
                        return new List<int> { 5, 6, 7, 8 };
                    case > 4:
                        return new List<int> { 8 };
                    default:
                    {
                        if (IsSolid)
                        {
                            return ProcessSide switch {
                                0 => new List<int> { 5, 6 },
                                1 => new List<int> { 5 },
                                2 => new List<int> { 6 },
                                _ => new List<int>()
                            };
                        }

                        return new List<int> { 7 };
                    }
                }
            default:
                return new List<int>();
        }
    }

    public int GetView()
    {
        return ProcessMethod switch {
            0 => 0,
            1 => 1,
            2 => 2,
            3 => 3,
            _ => -1
        };
    }
}