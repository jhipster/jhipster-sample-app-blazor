using Microsoft.AspNetCore.Http;

namespace Jhipster.Web.Rest.Utilities
{
    public static class HeaderUtil
    {
        private static readonly string APPLICATION_NAME = "jhipsterApp";

        public static IHeaderDictionary CreateAlert(string message, string param)
        {
            IHeaderDictionary headers = new HeaderDictionary();
            headers.Add($"X-{APPLICATION_NAME}-alert", message);
            headers.Add($"X-{APPLICATION_NAME}-params", param);
            return headers;
        }

        public static IHeaderDictionary CreateEntityCreationAlert(string entityName, string param)
        {
            return CreateAlert($"{APPLICATION_NAME}.{entityName}.created", param);
        }

        public static IHeaderDictionary CreateEntityUpdateAlert(string entityName, string param)
        {
            return CreateAlert($"{APPLICATION_NAME}.{entityName}.updated", param);
        }

        public static IHeaderDictionary CreateEntityDeletionAlert(string entityName, string param)
        {
            return CreateAlert($"{APPLICATION_NAME}.{entityName}.deleted", param);
        }

        public static IHeaderDictionary CreateFailureAlert(string entityName, string errorKey, string defaultMessage)
        {
            //log.error("Entity processing failed, {}", defaultMessage);
            IHeaderDictionary headers = new HeaderDictionary();
            headers.Add($"X-{APPLICATION_NAME}-error", $"error.{errorKey}");
            headers.Add($"X-{APPLICATION_NAME}-params", entityName);
            return headers;
        }
    }
}
