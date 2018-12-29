using System;
using Audit.Core;

namespace Audit.AzureTableStorage.ConfigurationApi
{
    /// <summary>
    /// Azure Blob Provider Configurator
    /// </summary>
    public interface IAzureBlobProviderConfigurator
    {
        /// <summary>
        /// Specifies the Azure Storage connection string
        /// </summary>
        /// <param name="connectionString">The Azure Storage connection string.</param>
        IAzureBlobProviderConfigurator ConnectionString(string connectionString);
        /// <summary>
        /// Specifies the container name (must be lower case)
        /// </summary>
        /// <param name="containerName">The container name (must be lower case).</param>
        IAzureBlobProviderConfigurator ContainerName(string containerName);
        /// <summary>
        /// Specifies a function that returns the connection string for an event
        /// </summary>
        /// <param name="connectionStringBuilder">A function that returns the connection string for an event.</param>
        IAzureBlobProviderConfigurator ConnectionStringBuilder(Func<AuditEvent, string> connectionStringBuilder);
        /// <summary>
        /// Specifies a function that returns the unique blob name for an event (can contain folders)
        /// </summary>
        /// <param name="blobNameBuilder">A function that returns the unique blob name for an event (can contain folders).</param>
        IAzureBlobProviderConfigurator BlobNameBuilder(Func<AuditEvent, string> blobNameBuilder);
        /// <summary>
        /// Specifies a function that returns the container name to use for an event
        /// </summary>
        /// <param name="containerNameBuilder">A function that returns the container name for an event.</param>
        IAzureBlobProviderConfigurator ContainerNameBuilder(Func<AuditEvent, string> containerNameBuilder);
    }
}