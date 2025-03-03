using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using UnityEngine;

namespace TESTING
{
    public class TestParsing : MonoBehaviour
    {
        [SerializeField] private TextAsset dialogueFile;
        private void Start() 
        {
            SendFileToParse();
        }

        void SendFileToParse()
        {
            List<string> lines = FileManager.ReadTextAsset(dialogueFile, false);

            foreach(string line in lines)
            {
                DIALOGUE_LINE dl = DialogueParser.Parse(line);
            }
        }
    }
}