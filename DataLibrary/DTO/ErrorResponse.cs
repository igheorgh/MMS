using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.DTO
{
    public class ErrorResponse
    {

        public int code { get; set; }
        public int httpCode { get; set; }
        public string message { get; set; }

        public ErrorResponse(int code, int httpCode, string message)
        {
            this.code = code;
            this.httpCode = httpCode;
            this.message = message;
        }

        public static ErrorResponse DuplicateError() { return new ErrorResponse(4009, 409, "Un obiect similar exista deja."); }
        public static ErrorResponse NotFound() { return new ErrorResponse(4004, 404, "Resursa cautata nu exista."); }
        public static ErrorResponse UnknownError() { return new ErrorResponse(5000, 500, "A aparut o eroare, va rugam sa reincercati."); }
        public static ErrorResponse NotValid() { return new ErrorResponse(4000, 400, "Cererea nu este valida."); }
        public static ErrorResponse InvalidCredentials() { return new ErrorResponse(4001, 401, "Credentialele folosite nu sunt corecte."); }
        public static ErrorResponse DuplicateError(string message) { return new ErrorResponse(4109, 409, message); }
        public static ErrorResponse NotFound(string message) { return new ErrorResponse(4104, 404, message); }
        public static ErrorResponse Unauthorized() { return new ErrorResponse(4101, 401, "Acces interzis."); }
        public static ErrorResponse NotValid(string message) { return new ErrorResponse(4100, 400, message); }
    }
}
