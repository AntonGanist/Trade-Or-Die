using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPlayback
{
    readonly MonoBehaviour _coroutineRunner;
    readonly DialogPanel _panel;
    readonly DialogTriggerExecutor _triggerExecutor;

    Coroutine _currentCoroutine;

    public Action<string> OnAutoNextRequested;
    public Action OnNodeFinished;

    public DialogPlayback(MonoBehaviour coroutineRunner, DialogPanel panel,
        DialogTriggerExecutor triggerExecutor)
    {
        _coroutineRunner = coroutineRunner;
        _panel = panel;
        _triggerExecutor = triggerExecutor;
    }

    public void PlayNode(List<string> dialogLines, List<string> optionLines)
    {
        Stop();

        _currentCoroutine = _coroutineRunner.StartCoroutine(PlayCoroutine(dialogLines, optionLines));
    }
    public void Stop()
    {
        if (_currentCoroutine != null)
        {
            _coroutineRunner.StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
    }

    IEnumerator PlayCoroutine(List<string> dialogLines, List<string> optionLines)
    {
        for (int i = 0; i < dialogLines.Count; i++)
        {
            if (!TryParseLine(dialogLines[i], out string text, out float time, out string autoNext))
                continue;

            _panel.ShowText(text);

            yield return new WaitForSeconds(time);

            if (!string.IsNullOrEmpty(autoNext))
            {
                OnAutoNextRequested?.Invoke(autoNext);
                _currentCoroutine = null;
                yield break;
            }
        }


        if (optionLines != null && optionLines.Count > 0)
        {
            List<string> answers = new();
            List<string> meta = new();

            foreach (var option in optionLines)
            {
                string[] parts = option.Split( new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length < 3)
                    continue;

                answers.Add(parts[0].Trim());

                string other = "";
                for (int i = 1; i < parts.Length; i++)
                    other += "\t" + parts[i];

                meta.Add(other);
            }

            _panel.TurnOnPanel(answers, meta);
        }

        _currentCoroutine = null;
        OnNodeFinished?.Invoke();
    }

    bool TryParseLine(string raw, out string text, out float time, out string autoNext)
    {
        text = "";
        time = 0f;
        autoNext = "";

        if (!raw.Contains(":"))
            return false;

        string[] speakerSplit = raw.Split(new[] { ':' }, 2);
        if (speakerSplit.Length < 2)
            return false;

        string[] parts = speakerSplit[1].Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length < 2)
            return false;

        text = parts[0];

        if (!float.TryParse(parts[1], out time))
            time = 2f;

        for (int i = 2; i < parts.Length; i++)
        {
            if (i == parts.Length - 1 && !parts[i].Contains("."))
            {
                autoNext = parts[i];
                break;
            }

            if (parts[i].Contains("."))
            {
                _triggerExecutor?.Execute(parts[i], false);
            }
        }

        return true;
    }
    public void TryParseLine(string raw)
    {
        string[] parts = raw.Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length < 2)
            return;

        for (int i = 1; i < parts.Length; i++)
        {
            if (i == parts.Length - 1 && !parts[i].Contains("."))
                break;

            if (parts[i].Contains("."))
                _triggerExecutor?.Execute(parts[i], true);
        }
    }
}