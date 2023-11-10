namespace Planner.Models;

class EcoPrecisionItem
{
    public string Option { get; private set; }
    public int Level { get; private set; }

    public EcoPrecisionItem(int level)
    {
        Level = level;
        Option = "IT " + level;
    }
}