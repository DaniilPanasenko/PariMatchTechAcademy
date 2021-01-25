using System;
using NotesApp.Services.Abstractions;
using NotesApp.Services.Models;
using NotesApp.Services.Services;
using Moq;
using Xunit;

namespace NotesApp.Tests.Services
{
    public class NotesServiceTests
    {
        [Fact]
        public void AddNote_Should_Throw_ArgumentNullException_If_Note_Is_Null()
        {
            var notesStorage = new Mock<INotesStorage>();
            var noteEvents = new Mock<INoteEvents>();

            var notesService = new NotesService(notesStorage.Object, noteEvents.Object);

            Assert.Throws<ArgumentNullException>(() => notesService.AddNote(null, It.IsAny<int>()));
        }

        [Fact]
        public void AddNote_Should_Be_Added_With_INoteEvents_If_It_Was_Added_With_INotesStorage()
        {
            Note note = new Note();

            var notesStorage = new Mock<INotesStorage>();
            var noteEvents = new Mock<INoteEvents>();
            notesStorage.Setup(x => x.AddNote(note, It.IsAny<int>()));
            noteEvents.Setup(x => x.NotifyAdded(note, It.IsAny<int>()));

            var notesService = new NotesService(notesStorage.Object, noteEvents.Object);

            notesService.AddNote(note, It.IsAny<int>());

            noteEvents.Verify(x => x.NotifyAdded(note, It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void AddNote_Should_Not_Be_Added_With_INoteEvents_If_It_Was_Not_Added_With_INotesStorage()
        {
            Note note = new Note();

            var notesStorage = new Mock<INotesStorage>();
            var noteEvents = new Mock<INoteEvents>();
            notesStorage.Setup(x => x.AddNote(note, It.IsAny<int>())).Throws<Exception>();
            noteEvents.Setup(x => x.NotifyAdded(note, It.IsAny<int>()));

            var notesService = new NotesService(notesStorage.Object, noteEvents.Object);

            Assert.Throws<Exception>(() => notesService.AddNote(note, It.IsAny<int>()));
            noteEvents.Verify(x => x.NotifyAdded(note, 0), Times.Never);
        }

        [Fact]
        public void DeleteNote_Should_Be_Deleted_With_INoteEvents_If_It_Was_Deleted_With_INotesStorage()
        {
            Guid guid = Guid.NewGuid();

            var notesStorage = new Mock<INotesStorage>();
            var noteEvents = new Mock<INoteEvents>();
            notesStorage.Setup(s => s.DeleteNote(guid)).Returns(true);
            noteEvents.Setup(s => s.NotifyDeleted(guid, It.IsAny<int>()));

            var notesService = new NotesService(notesStorage.Object, noteEvents.Object);

            notesService.DeleteNote(guid, 0);

            noteEvents.Verify(x => x.NotifyDeleted(guid, 0), Times.Once);
        }

        [Fact]
        public void DeleteNote_Should_Not_Be_Deleted_With_INoteEvents_If_It_Was_Not_Deleted_With_INotesStorage()
        {
            Guid guid = Guid.NewGuid();

            var notesStorage = new Mock<INotesStorage>();
            var noteEvents = new Mock<INoteEvents>();
            notesStorage.Setup(s => s.DeleteNote(guid)).Returns(false);
            noteEvents.Setup(s => s.NotifyDeleted(guid, It.IsAny<int>()));

            var notesService = new NotesService(notesStorage.Object, noteEvents.Object);

            notesService.DeleteNote(guid, 0);

            noteEvents.Verify(x => x.NotifyDeleted(guid, 0), Times.Never);
        }
    }
}
