using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

namespace TESTING
{
    public class Testing_Architect : MonoBehaviour
    {
        DialogueManager dm;
        DialogueArchitect architect;

        public DialogueArchitect.BuildMethod buildMethod = DialogueArchitect.BuildMethod.typewriter;

        string[] lines = new string[5]
        {
            "Acumalaka adalah seorang pengembala kambing yang sangat hebat dan dicintai oleh masyarakat",
            "Kambing adalah seekor hewan milik Acumalaka yang dirawat dengan sangat baik sehingga berukuran gebal gebol",
            "Pengembala adalah pekerjaan dari Acumalaka yang sangat dia banggakan",
            "Acumalaka dan Kambing adalah dua sahabat",
            "Mereka terlihat sedang bersama-sama"
        };

        private void Start() 
        {
            dm = DialogueManager.instance;
            // architect = new DialogueArchitect(dm.dialogueContainer.dialogueText);
            // architect = new DialogueArchitect(dm.dialogueText);
            architect.buildMethod = DialogueArchitect.BuildMethod.typewriter;
        }

        private void Update() 
        {
            if (buildMethod != architect.buildMethod)
            {
                architect.buildMethod = buildMethod;
                architect.StopBuild();
            }

            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                if (architect.isBuilding)
                {
                    architect.ForceComplete();
                }
                else
                    architect.BuildDialogue(lines[Random.Range(0, lines.Length)]);
            }
            // else if(Input.GetKeyDown(KeyCode.B)) 
            // {
            //     architect.AppendText(lines[Random.Range(0, lines.Length)]);
            // }
        }
    }
}

