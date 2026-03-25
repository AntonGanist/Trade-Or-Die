public class DialogTriggerExecutor
{
    NpcDialogPlayback _npc;
    PlayerDialogPlayback _player;

    public DialogTriggerExecutor(NpcDialogPlayback npc, PlayerDialogPlayback player)
    {
        _npc = npc;
        _player = player;
    }

    public void Execute(string trigger, bool isPlayer)
    {
        var parts = trigger.Split('.', 2);
        if (parts.Length != 2) return;


        if (parts[0] == "Animated")
        {
            if (isPlayer)
                _player.StartAnimation(parts[1]);
            else
                _npc.StartAnimation(parts[1]);
        }

        if (parts[0] == "Audio")
        {
            if (isPlayer)
                _player.StartAudio(parts[1]);
            else
                _npc.StartAudio(parts[1]);
        }
        if (parts[0] == "State")
        {
            if (isPlayer)
                _player.ChangeState(parts[1]);
            else
                _npc.ChangeState(parts[1]);
        }
    }
}