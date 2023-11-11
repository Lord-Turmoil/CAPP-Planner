namespace Planner.Models;

class BlankTypeItem
{
    public BlankTypeItem(int kind, string option)
    {
        Kind = kind;
        Option = option;
    }

    public string Option { get; private set; }
    public int Kind { get; private set; }
}