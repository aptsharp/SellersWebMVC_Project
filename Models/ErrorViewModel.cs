using System;

namespace SellersWebMVC.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
       
        /*
         * sem o ponto "!" ele não exibe o erro.
         * recomendavel que o ao publicar não seja exibido o erro por questões de segurança. 
         */
    }
}