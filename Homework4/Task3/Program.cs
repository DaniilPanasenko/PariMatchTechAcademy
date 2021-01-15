using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Task3
{
    class Program
    {
        static List<Note> GetNotes()
        {
            try
            {
                var json = File.ReadAllText("storage.json");
                var notes = JsonConvert.DeserializeObject<List<Note>>(json);
                return notes;
            }
            catch (FileNotFoundException)
            {
                return new List<Note>(); 
            }
        }

        static void WriteNotes(List<Note> notes)
        {
            var json = JsonConvert.SerializeObject(notes);
            File.WriteAllText("storage.json", json);
        }

        static int InputCommand(string menuName, string[] commands)
        {
            Console.WriteLine($"\n{menuName}:");
            for (int i = 0; i < commands.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {commands[i]}");
            }
            Console.WriteLine("\nEnter the command number...");
            int command;
            while (!int.TryParse(Console.ReadLine(), out command) || command < 1 || command > commands.Length)
            {
                Console.WriteLine("You entered incorrect command");
                Console.WriteLine("\nEnter the command number...");
            }
            return command;
        }

        static void MainMenu()
        {
            while (true)
            {
                bool exit = false;
                int command = InputCommand("Notes Menu", new string[] { "Search", "View", "Create","Delete","Exit" });
                switch (command)
                {
                    case 1:
                        Search();
                        break;
                    case 2:
                        View();
                        break;
                    case 3:
                        Create();
                        break;
                    case 4:
                        Delete();
                        break;
                    case 5:
                        exit = true;
                        break;
                }
                if (exit) break;
            }
        }

        private static void Search()
        {
            var notes = GetNotes();
            Console.WriteLine("Enter filter...");
            string filter = Console.ReadLine().Trim();
            var result = notes.Where(x => x.Id.ToString().Contains(filter));
            result = notes.Where(x => x.Text.ToLower().Contains(filter.ToLower())).ToList();
            result = result.Concat(notes.Where(x => x.CreatedOn.ToString().Contains(filter)));
            result = result.Distinct().OrderBy(x => x.Id);
            if (result.Count() == 0)
            {
                Console.WriteLine("\nSearch didn't return any result");
            }
            else
            {
                Console.WriteLine();
                foreach(var note in result)
                { 
                    Console.WriteLine(note.ToShortString());
                }
            }
        }

        private static void View()
        {
            var notes = GetNotes();
            Console.WriteLine("Enter note ID...");
            string id = Console.ReadLine().Trim();
            var result = notes.Where(x => x.Id.ToString()==id).FirstOrDefault();
            if(result==null)
            {
                Console.WriteLine("\nYou don't have note with this ID");
            }
            else
            {
                Console.WriteLine("\n"+result.ToString());
            }
        }

        private static void Create()
        {
            Console.WriteLine("Enter note text...");
            string text = Console.ReadLine().Trim();
            if (text.Length == 0)
            {
                Console.WriteLine("\nEmpty note won't be created");
            }
            else
            {
                var notes = GetNotes();
                int id = 0;
                if (notes.Count != 0) id = notes.Max(x => x.Id) + 1;
                Note newNote = new Note(text, id);
                notes.Add(newNote);
                WriteNotes(notes);
            }
        }

        private static void Delete()
        {
            var notes = GetNotes();
            Console.WriteLine("Enter note ID...");
            string id = Console.ReadLine().Trim();
            var result = notes.Where(x => x.Id.ToString() == id).FirstOrDefault();
            if (result == null)
            {
                Console.WriteLine("\nYou don't have note with this ID");
            }
            else
            {
                Console.WriteLine(result.ToString());
                while (true)
                {
                    Console.WriteLine("\nDo you confirm this deletion (y/n)?");
                    var answer = Console.ReadLine().Trim();
                    if (answer == "y")
                    {
                        notes.RemoveAll(x => x.Id.ToString() == id);
                        WriteNotes(notes);
                        break;
                    }
                    else if (answer == "n") break;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Task3. Notes application by Daniil Panasenko\n");
            MainMenu();
        }
    }
}
