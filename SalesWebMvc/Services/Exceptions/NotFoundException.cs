using System;

namespace SalesWebMvc.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        // exeção personalizada
        // construtor que ira receber a mensagem
        // ': base' o contrutor ira repassar para a classe base 
        public NotFoundException( string message) : base(message)
        {

        }

    }
}
