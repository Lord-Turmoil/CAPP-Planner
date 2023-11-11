namespace Planner.Models;

class ResultItem
{
    public ResultItem(int no, string procedure)
    {
        No = no;
        Procedure = procedure;
    }

    public int No { get; private set; }
    public string Procedure { get; private set; }
}