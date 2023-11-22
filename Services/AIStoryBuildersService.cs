﻿using AIStoryBuilders.AI;
using AIStoryBuilders.Model;
using AIStoryBuilders.Models;
using static AIStoryBuilders.AI.OrchestratorMethods;
using AIStoryBuilders.Models.JSON;
using Newtonsoft.Json;

namespace AIStoryBuilders.Services
{
    public partial class AIStoryBuildersService
    {
        public event EventHandler<TextEventArgs> TextEvent;

        private readonly AppMetadata _appMetadata;
        private LogService LogService { get; set; }

        private OrchestratorMethods OrchestratorMethods { get; set; }

        public string BasePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/AIStoryBuilders";
        public AIStoryBuildersService(
            AppMetadata appMetadata,
            LogService _LogService,
            OrchestratorMethods _OrchestratorMethods)
        {
            _appMetadata = appMetadata;
            LogService = _LogService;
            OrchestratorMethods = _OrchestratorMethods;
        }

        // Utility

        #region public string[] ReadCSVFile(string path)
        public string[] ReadCSVFile(string path)
        {
            string[] content;

            // Read the lines from the .csv file
            using (var file = new System.IO.StreamReader(path))
            {
                content = file.ReadToEnd().Split('\n');

                if (content[content.Length - 1].Trim() == "")
                {
                    content = content.Take(content.Length - 1).ToArray();
                }
            }

            return content;
        }
        #endregion

        #region public void CreateDirectory(string path)
        public void CreateDirectory(string path)
        {
            // Create directory if it doesn't exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        #endregion

        #region public string GetOnlyJSON(string json)
        public string GetOnlyJSON(string json)
        {
            string OnlyJSON = "";
            // Search for the first occurrence of the { character
            int FirstCurlyBrace = json.IndexOf('{');
            // Set ParsedStory to the string after the first occurrence of the { character
            OnlyJSON = json.Substring(FirstCurlyBrace);

            return OnlyJSON;
        }
        #endregion

        #region public void CreateFile(string path, string content)
        public void CreateFile(string path, string content)
        {
            // Create file if it doesn't exist
            if (!File.Exists(path))
            {
                File.WriteAllText(path, content);
            }
        }
        #endregion

        #region public class TextEventArgs : EventArgs
        public class TextEventArgs : EventArgs
        {
            public string Message { get; set; }
            public int DisplayLength { get; set; }

            public TextEventArgs(string message, int displayLength)
            {
                Message = message;
                DisplayLength = displayLength;
            }
        }
        #endregion

        #region public List<Models.Character> SimplifyCharacter(List<Models.Character> colCharacters, Paragraph objParagraph)
        public List<Models.Character> SimplifyCharacter(List<Models.Character> colCharacters, Paragraph objParagraph)
        {
            // If the Paragraph has a Timeline selected, filter the CharacterBackground 
            // to only those that are in the Timeline or empty Timeline
            List<Models.Character> colCharactersInTimeline = new List<Models.Character>();

            if (objParagraph.Timeline.TimelineName != null && objParagraph.Timeline.TimelineName.Length > 0)
            {
                foreach (var character in colCharacters)
                {
                    Models.Character objCharacter = new Models.Character();

                    objCharacter.CharacterName = character.CharacterName;

                    objCharacter.CharacterBackground = new List<CharacterBackground>();

                    foreach (var background in character.CharacterBackground)
                    {
                        if ((background.Timeline.TimelineName == objParagraph.Timeline.TimelineName) ||
                        (background.Timeline.TimelineName == null || background.Timeline.TimelineName == ""))
                        {
                            objCharacter.CharacterBackground.Add(background);
                        }
                    }

                    colCharactersInTimeline.Add(objCharacter);
                }
            }
            else
            {
                colCharactersInTimeline = colCharacters;
            }

            return colCharactersInTimeline;
        }
        #endregion
    }
}
