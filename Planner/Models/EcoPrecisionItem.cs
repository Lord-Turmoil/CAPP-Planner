namespace Planner.Models;

class EcoPrecisionItem
{
    public EcoPrecisionItem(int level)
    {
        Level = level;
        Option = "IT " + level;
    }

    public string Option { get; private set; }
    public int Level { get; private set; }
}