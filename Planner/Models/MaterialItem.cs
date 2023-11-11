namespace Planner.Models;

class MaterialItem
{
    public MaterialItem(int kind, string option)
    {
        Kind = kind;
        Option = option;
    }

    public string Option { get; private set; }
    public int Kind { get; private set; }
}