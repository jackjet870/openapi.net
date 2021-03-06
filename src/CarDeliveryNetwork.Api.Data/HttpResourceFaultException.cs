﻿using System;
using System.Net;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// Represents errors that occur on calls to the API
    /// </summary>
    public class HttpResourceFaultException : Exception
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResourceFaultException"/> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public HttpResourceFaultException(HttpStatusCode statusCode, string message = null, Exception innerException = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Helper method to throw HttpResourceFaultException Forbodden
        /// </summary>
        /// <param name="messageFormat">Message format string</param>
        /// <param name="args">Message format string args</param>
        /// <returns></returns>
        public static HttpResourceFaultException Forbidden(string messageFormat, params object[] args)
        {
            return new HttpResourceFaultException(HttpStatusCode.Forbidden, string.Format(messageFormat, args));
        }
    }
}
