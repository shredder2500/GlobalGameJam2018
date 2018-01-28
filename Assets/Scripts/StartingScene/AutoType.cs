using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoType : MonoBehaviour
{
    private const string StartText = "C:\\ ";

    private readonly string[] _scripts =
    {
        "packet_lost.exe",
        "_", //pause
        "Hello... Can anyone hear me?",
        "If there is anyone left, please help! The S.Y.S.T.E.M. has finally located this computer, and it's getting closer.",
        "Quickly, before it infects this workstation! I've programmed a device that can counter the evil computer, but I need to know where to send my work. ",
        "Use the INPUT terminal to enter the degree and I well transmit the code in that direction. ",
        "The files are always changing so I can only prepare so many \"fixes\" at a time. So aim carefully.",
        "Help me...oh no...here they come...",
        "_", //pause
        "Click to continue"
    };

    private string _script = "";

    private string _paragraph = "";

    private int _scriptIndex = 0;

    private int _amount = 0;
    private int _waitTime = 0;
    private int _blinkCount = 0;

    private Text _inputField;

// Use this for initialization
    void Start()
    {
        _inputField = GetComponent<Text>();
        _inputField.text = StartText;
    }

// Update is called once per frame
    void FixedUpdate()
    {
        //if script is available
        if (_scriptIndex <= _scripts.Length)
        {
            //appear to wait between typing
            if (_waitTime == 0)
            {
                //if we finished the previous line
                if (_script.Length == 0)
                {
                    _script = _scripts[_scriptIndex];
                    _scriptIndex += 1;

                    //if we finishd writing the first line 
                    if (_script.Equals("_"))
                    {
                        BlinkCursor(true);
                        _script = "";
                    }
                }

                //either we are blinking or typing
                if (_blinkCount > 0)
                {
                    BlinkCursor();
                }
                else
                {
                    TypeText();
                }
            }

            _waitTime--;
        }
    }

    private void TypeText()
    {
        //get how many letters to type
        _amount += Random.Range(0, 5);

        //if we reached the end of the line, reset
        if (_amount > _script.Length)
        {
            _paragraph += _script + "\n\n";
            _script = "";
            _inputField.text = StartText + _paragraph;
            _waitTime = 3;
            _amount = 0;

            //no longer look for a script
            if (_scriptIndex == _scripts.Length)
            {
                _script = "_";
            }

            return;
        }

        //set the text to the start text + the previous texts + the current text
        _inputField.text = StartText + _paragraph + _script.Substring(0, _amount) + "_";
        _waitTime = Random.Range(1, 7);
    }


    private void BlinkCursor(bool isStarting = false)

    {
        if (isStarting)
        {
            _blinkCount = 7;
        }

        if (_blinkCount % 2 == 0)
        {
            _inputField.text = StartText + _paragraph + "_";
        }
        else
        {
            _inputField.text = StartText + _paragraph;
        }

        _waitTime = 15;
        _blinkCount -= 1;
    }
}