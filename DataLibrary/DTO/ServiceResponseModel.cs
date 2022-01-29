using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.DTO
{
    public class ServiceResponseModel<T>
    {

        public T responseData { get; set; }
        public ErrorResponse errorResponse { get; set; }

        public ServiceResponseModel(T responseData, ErrorResponse errorResponse)
        {
            this.errorResponse = errorResponse;
            this.responseData = responseData;
        }

        public static ServiceResponseModel<object> ok(object obj)
        {
            return new ServiceResponseModel<object>(obj, null);
        }
        public static ServiceResponseModel<object> bad(ErrorResponse err)
        {
            return new ServiceResponseModel<object>(null, err);
        }
        public static ServiceResponseModel<object> bad(object obj, ErrorResponse err)
        {
            return new ServiceResponseModel<object>(obj, err);
        }


        public static ServiceResponseModel<object> Conflict(string message) { return ServiceResponseModel<object>.bad(ErrorResponse.DuplicateError(message)); }

        public static ServiceResponseModel<object> NotFound(string message) { return ServiceResponseModel<object>.bad(ErrorResponse.NotFound(message)); }

        public static ServiceResponseModel<object> NotValid(string message) { return ServiceResponseModel<object>.bad(ErrorResponse.NotValid(message)); }


        public static ServiceResponseModel<object> StringNotValid(string field, int maxChar) { return ServiceResponseModel<object>.bad(ErrorResponse.NotValid("Campul '" + field + "' trebuie sa contine cel mult " + maxChar + " de caractere.")); }

        public static ServiceResponseModel<object> StringNotValid(string field, int maxChar, int minChar)
        {
            if (maxChar == minChar)
            {
                return ServiceResponseModel<object>.bad(ErrorResponse.NotValid("Campul '" + field + "' trebuie sa aiba exact " + minChar + " caractere."));
            }
            return ServiceResponseModel<object>.bad(ErrorResponse.NotValid("Campul '" + field + "' trebuie sa aiba intre " + minChar + " si " + maxChar + " caractere."));
        }

        public static ServiceResponseModel<object> IntegerNotValid(string field, int max) { return ServiceResponseModel<object>.bad(ErrorResponse.NotValid("Campul '" + field + "' poate fi maxim " + max + ".")); }

        public static ServiceResponseModel<object> IntegerNotValid(string field, int max, int min)
        {
            return ServiceResponseModel<object>.bad(ErrorResponse.NotValid("Campul '" + field + "' trebuie sa fie in intervalul " + min + " si " + max + "."));
        }

        public static ServiceResponseModel<object> InvalidDate(string field, int max, int min) { return ServiceResponseModel<object>.bad(ErrorResponse.NotValid("Campul '" + field + "' trebuie aiba anul in intervalul " + min + " si " + max + ".")); }


        public static ServiceResponseModel<object> Unauthorized() { return ServiceResponseModel<object>.bad(ErrorResponse.Unauthorized()); }

        public static ServiceResponseModel<object> InvalidCredentials() { return ServiceResponseModel<object>.bad(ErrorResponse.InvalidCredentials()); }

        public bool hasError()
        {
            return errorResponse != null;
        }

        public ObjectResult toHttpResponse()
        {
            if (errorResponse != null)
            {
                return new ObjectResult(errorResponse.message) { StatusCode = errorResponse.httpCode };
            }
            return new ObjectResult(responseData) { StatusCode = 200 };
        }

    }
}
