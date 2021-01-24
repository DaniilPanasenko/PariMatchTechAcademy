using System;
using NotesApp.Services.Abstractions;
using NotesApp.Services.Models;
using NotesApp.Services.Services;
using Moq;
using Xunit;

namespace NotesApp.Tests
{
    public class NotesServiceTests
    {
        [Fact]
        public void AddNote_Should_Throw_ArgumentNullException_If_Note_Is_Null()
        {
            var notesStorage = new Mock<INotesStorage>();
            var noteEvents = new Mock<INoteEvents>();

            var notesService = new NotesService(notesStorage.Object, noteEvents.Object);

            Assert.Throws<ArgumentNullException>(() => notesService.AddNote(null, 0));
        }

        [Fact]
        public void AddNote_Should_Be_Added_With_INoteEvents_If_It_Was_Added_With_INotesStorage()
        {
            var notesStorage = new Mock<INotesStorage>();
            var noteEvents = new Mock<INoteEvents>();

            var notesService = new NotesService(notesStorage.Object, noteEvents.Object);

            Note note = new Note();
            notesService.AddNote(note, 0);

            notesStorage.Verify(x => x.AddNote(note, 0), Times.Once);
            noteEvents.Verify(x => x.NotifyAdded(note, 0), Times.Once);
        }

        [Fact]
        public void AddNote_Should_Not_Be_Added_With_INoteEvents_If_It_Was_Not_Added_With_INotesStorage()
        {
            var notesStorage = new Mock<INotesStorage>();
            var noteEvents = new Mock<INoteEvents>();

            var notesService = new NotesService(notesStorage.Object, noteEvents.Object);

            Note note = new Note();
            notesService.AddNote(note, 0);
            notesStorage.Verify(x => x.AddNote(note, 0), Times.Never);
            noteEvents.Verify(x => x.NotifyAdded(note, 0), Times.Never);
        }
    }
}
