using System;
namespace Task3
{
    public interface INote
    {
        int Id { get; }

        string Title { get; }

        string Text { get; }

        DateTime CreatedOn { get; }
    }
}