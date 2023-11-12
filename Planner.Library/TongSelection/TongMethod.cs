using Planner.Library.HoleProcess;
using System.Collections.Generic;
using System.Linq;

namespace Planner.Library.TongSelection;

public class TongMethod
{
    private static readonly Dictionary<int, string> _equipments = new()
    {
        { 0, "卡盘" },
        { 1, "尾顶尖" },
        { 2, "顶尖" },
        { 3, "跟刀架" },
        { 4, "平口钳" },
        { 5, "分度头" },
        { 6, "心轴" }
    };

    private static readonly Dictionary<int, List<int>> _methods = new()
    {
        { 0, new List<int> { 0 } },
        { 1, new List<int> { 0, 1 } },
        { 2, new List<int> { 2, 3, 1 } },
        { 3, new List<int> { 4 } },
        { 4, new List<int> { 5, 2 } },
        { 5, new List<int> { 6 } },
        { 6, new List<int> { 0 } },
        { 7, new List<int> { 0 } },
        { 8, new List<int> { 2, 2 } }
    };

    public static List<string> GetMethod(int id)
    {
        if (_methods.TryGetValue(id, out List<int>? method))
        {
            return method.Select(equip => _equipments[equip]).ToList();
        }

        return new List<string>();
    }

}