public class DialogFlowController
{
    NpcDialogPlayback _currentNpc;
    string _currentKnot;
    string _autoNextKnot = string.Empty;

    public NpcDialogPlayback CurrentNpc => _currentNpc;
    public string CurrentKnot => _currentKnot;

    public void SetCurrentNpc(NpcDialogPlayback npc)
    {
        _currentNpc = npc;
    }

    public void Start(string startKnot)
    {
        _currentKnot = startKnot;
        _autoNextKnot = string.Empty;
    }

    public void SetCurrentKnot(string knot)
    {
        _currentKnot = knot;
    }

    public void SetAutoNext(string knot)
    {
        _autoNextKnot = knot;
    }

    public bool HasAutoNext() => string.IsNullOrEmpty(_autoNextKnot);

    public string ConsumeAutoNext()
    {
        string next = _autoNextKnot;
        _autoNextKnot = string.Empty;
        return next;
    }

    public bool IsExit(string knot) => knot == "Exit";

    public void Reset()
    {
        _currentNpc = null;
        _currentKnot = string.Empty;
        _autoNextKnot = string.Empty;
    }
}