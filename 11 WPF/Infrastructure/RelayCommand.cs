// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelayCommand.cs" author="Esbjörn Redmo" company="Rerity AB">
//   Copyright © 2016-2022 Esbjörn Redmo, Rerity AB. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Windows.Input;

namespace AD11.Infrastructure;

public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private bool _canExecute;

    public RelayCommand(Action execute, bool canExecute = true)
    {
        ArgumentNullException.ThrowIfNull(execute);
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute
    {
        get => _canExecute;
        set
        {
            if (_canExecute != value)
            {
                _canExecute = value;
                InternalCanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private event EventHandler? InternalCanExecuteChanged;

    #region ICommand interface implementation

    bool ICommand.CanExecute(object? parameter)
    {
        return _canExecute;
    }

    event EventHandler? ICommand.CanExecuteChanged
    {
        add => InternalCanExecuteChanged += value;
        remove => InternalCanExecuteChanged -= value;
    }

    void ICommand.Execute(object? parameter)
    {
        _execute();
    }

    #endregion
}