using System;

namespace SellersWebMVC.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
       
        /*
         * sem o ponto "!" ele n�o exibe o erro.
         * recomendavel que o ao publicar n�o seja exibido o erro por quest�es de seguran�a. 
         */
    }
}