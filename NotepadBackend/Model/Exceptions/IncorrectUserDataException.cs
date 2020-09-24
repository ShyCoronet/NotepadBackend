using System;

namespace NotepadBackend.Model.Exceptions
{
    public class IncorrectUserDataException : ArgumentException
    {
        public object Value { get; }
        
        public IncorrectUserDataException(string message, object value)
            : base(message)
        {
            Value = value;
        }
    }
}