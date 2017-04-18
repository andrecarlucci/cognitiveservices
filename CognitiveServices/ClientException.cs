using System;
using System.Net;

namespace CognitiveServices {
    public class ClientException : Exception {

        public ClientError Error { get; }
        public HttpStatusCode HttpStatus { get; }

        public ClientException() {
        }

        public ClientException(string message)
            : base(message) {
            Error = new ClientError {
                Code = HttpStatusCode.InternalServerError.ToString(),
                Message = message
            };
        }

        public ClientException(string message, HttpStatusCode httpStatus) : base(message) {
            HttpStatus = httpStatus;

            Error = new ClientError {
                Code = HttpStatus.ToString(),
                Message = message
            };
        }

        public ClientException(string message, Exception innerException) : base(message, innerException) {
            Error = new ClientError {
                Code = HttpStatusCode.InternalServerError.ToString(),
                Message = message
            };
        }

        public ClientException(string message, string errorCode, HttpStatusCode httpStatus, Exception innerException)
            : base(message, innerException) {
            HttpStatus = httpStatus;

            Error = new ClientError {
                Code = errorCode,
                Message = message
            };
        }

        public ClientException(ClientError error, HttpStatusCode httpStatus) {
            Error = error;
            HttpStatus = httpStatus;
        }

        public static ClientException BadRequest(string message) {
            return new ClientException(
                new ClientError {
                    Code = ((int) HttpStatusCode.BadRequest).ToString(),
                    Message = message
                },
                HttpStatusCode.BadRequest);
        }
    }
}