using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfrastructureLayer.Helper.OperationResult;

namespace InfrastructureLayer.Helper.OperationResult
{
    public class OperationResult : OperationResult<object>
    {
    }
}

public class OperationResult<T>
{
    #region Fields

    private bool hasError;

    public bool HasError
    {
        get => hasError;
        private set => hasError = value;
    }

    public List<Message> Messages;

    public T? Result;

    #endregion

    #region Ctor

    public OperationResult()
    {
        this.hasError = false;
        this.Messages = new List<Message>();
        this.setDefaultResult();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Create Error Message and set HasError to true.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="parameters"></param>
    public void SetError(string errorMessage, params object[] parameters)
    {
        string errorTextMessage = string.Format(errorMessage, parameters);
        Message message = new Message(errorTextMessage, OperationStatus.Error);
        this.Messages.Add(message);
        this.setStatus(OperationStatus.Error);
    }

    /// <summary>
    /// Create Warning Message.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="parameters"></param>
    public void SetWarning(string warningMessage, params object[] parameters)
    {
        string warningTextMessage = string.Format(warningMessage, parameters);
        Message message = new Message(warningTextMessage, OperationStatus.Warning);
        this.Messages.Add(message);
        this.setStatus(OperationStatus.Warning);
    }

    /// <summary>
    /// Create Success Message.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="parameters"></param>
    public void SetSuccess(string successMessage, params object[] parameters)
    {
        string successTextMessage = string.Format(successMessage, parameters);
        Message message = new Message(successTextMessage, OperationStatus.Success);
        this.Messages.Add(message);
        this.setStatus(OperationStatus.Success);
    }

    #endregion

    #region Private Methods

    private void setStatus(OperationStatus operationStatus)
    {
        if (operationStatus == OperationStatus.Error)
            this.hasError = true;
    }

    private void setDefaultResult()
    {
        switch (typeof(T).Name)
        {
            case "Boolean":
                this.Result = (T)(object)false;
                break;

            default:
                this.Result = default;
                break;
        }


    }

    #endregion
}
