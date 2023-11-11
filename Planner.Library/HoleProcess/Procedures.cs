using System.Collections.Generic;

namespace Planner.Library.HoleProcess;

public static class Procedures
{
    private static readonly Dictionary<int, List<string>> _procedures = new()
    {
        { 1, new List<string> { "钻" } },
        { 2, new List<string> { "钻", "铰" } },
        { 3, new List<string> { "钻", "粗铰", "精铰" } },
        { 4, new List<string> { "钻", "扩" } },
        { 5, new List<string> { "钻", "扩", "铰" } },
        { 6, new List<string> { "钻", "扩", "粗铰", "精铰" } },
        { 7, new List<string> { "钻", "扩", "机铰", "手铰" } },
        { 8, new List<string> { "钻", "（扩）", "拉" } },
        { 9, new List<string> { "粗镗（或扩孔）" } },
        { 10, new List<string> { "粗镗（或粗扩）", "细镗（或精扩）" } },
        { 11, new List<string> { "粗镗（扩）", "细镗（精扩）", "精镗（铰）" } },
        { 12, new List<string> { "粗镗（扩）", "细镗（精扩）", "精镗", "浮动镗刀块精镗" } },
        { 13, new List<string> { "粗镗（扩）", "细镗", "磨孔" } },
        { 14, new List<string> { "粗镗", "细镗", "粗磨", "精磨" } },
        { 15, new List<string> { "粗镗", "细镗", "精镗", "金刚石镗" } },
        { 16, new List<string> { "钻", "（扩）", "粗铰", "精铰", "珩磨" } },
        { 17, new List<string> { "钻", "（扩）", "拉", "珩磨" } }
    };

    public static List<string> GetProcedure(int id)
    {
        if (_procedures.ContainsKey(id))
        {
            return _procedures[id];
        }

        return new List<string>();
    }
}