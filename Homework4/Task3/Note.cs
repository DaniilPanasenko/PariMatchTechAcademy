using System;
using Newtonsoft.Json;

namespace Task3
{
    public class Note:INote
    {
        public Note(string text, int id)
        {
            Id = id;
            Text = text;
            Title = Text;
            if (Title.Length > 32)
            {
                Title = Title.Substring(0, 32);
            }
            CreatedOn = DateTime.UtcNow;
        }

        public Note() { }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("createdon")]
        public DateTime CreatedOn { get; set; }

        public string ToShortString()
        {
            return $"ID: {Id}, Title: {Title}, Created on: {CreatedOn}";
        }
        public override string ToString()
        {
            return $"ID: {Id}\nTitle: {Title}\nText: {Text}\nCreated on: {CreatedOn}";
        }
    }
}
