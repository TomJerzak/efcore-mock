using System;

namespace EFCore.Mock.Example.Application.Exceptions
{
    public class EntityAlreadyExistException : Exception
    {
        public EntityAlreadyExistException(string message) : base(message)
        {
        }
    }
}