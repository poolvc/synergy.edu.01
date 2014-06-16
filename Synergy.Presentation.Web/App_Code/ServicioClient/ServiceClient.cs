using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

/// <summary>
/// A WCF service client.
/// </summary>
/// <typeparam name="TContract">The type of the service contract.</typeparam>
public class ServiceClient<TContract> : IDisposable
   where TContract : class
{
    /// <summary>
    /// The service client object.
    /// </summary>
    private TContract client;

    /// <summary>
    /// Initializes a new instance of the ServiceClient class.
    /// </summary>
    /// <param name="endpointName">The name of the endpoint to use.</param>
    public ServiceClient(string endpointName)
    {

        ChannelFactory<TContract> f = new ChannelFactory<TContract>(endpointName);
        this.client = f.CreateChannel();
    }

    /// <summary>
    /// Calls an operation on the service.
    /// </summary>
    /// <typeparam name="TResult">The type of the object returned by the service operation.</typeparam>
    /// <param name="operation">The operation to call.</param>
    /// <returns>The results of the service operation.</returns>
    public TResult ServiceOperation<TResult>(Func<TContract, TResult> operation)
    {
        //TResult tr;
        //try
        //{
        //    tr = operation(this.client);
        //}
        //catch (Exception e)
        //{
        //    throw e;
        //}
        //return tr;
        return operation(this.client);
    }

    /// <summary>
    /// Calls an operation on the service.
    /// </summary>
    /// <param name="operation">The operation to call.</param>
    public void ServiceOperation(Action<TContract> operation)
    {
        operation(this.client);
    }

    /// <summary>
    /// Disposes this object.
    /// </summary>
    public void Dispose()
    {
        try
        {
            if (this.client != null)
            {
                IClientChannel channel = this.client as IClientChannel;
                if (channel.State == CommunicationState.Faulted)
                {
                    channel.Abort();
                }
                else
                {
                    channel.Close();
                }
            }
        }
        finally
        {
            this.client = null;
            GC.SuppressFinalize(this);
        }
    }
}