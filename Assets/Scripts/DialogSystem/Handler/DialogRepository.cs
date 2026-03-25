using System.Collections.Generic;
using UnityEngine;

public class DialogRepository
{
    Dictionary<string, Dictionary<string, List<string>>> _dialogDictionary = new();
    Dictionary<string, Dictionary<string, List<string>>> _optionDictionary = new();

    DialogParser _parser = new DialogParser();

    public void Load(string npcName)
    {
        if (_dialogDictionary.ContainsKey(npcName)) return;

        TextAsset textAsset = Resources.Load<TextAsset>(npcName);
        if (textAsset == null)
        {
            Debug.LogError($"Файл диалога '{npcName}' не найден");
            return;
        }

        var result = _parser.Parse(textAsset.text);

        _dialogDictionary[npcName] = result.Dialogs;
        _optionDictionary[npcName] = result.Options;
    }

    public List<string> GetDialog(string npc, string knot) => _dialogDictionary[npc][knot];

    public List<string> GetOptions(string npc, string knot) => _optionDictionary[npc][knot];

    public bool Has(string npc, string knot)
        => _dialogDictionary.ContainsKey(npc) && _dialogDictionary[npc].ContainsKey(knot);


}