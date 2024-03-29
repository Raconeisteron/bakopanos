﻿using System;
using System.Data;
using System.Data.Common;

namespace DotNetDataProviderTemplate
{
    public class TemplateDataAdapter : DbDataAdapter, IDbDataAdapter
    {
        private TemplateCommand _selectCommand;
        private TemplateCommand _insertCommand;
        private TemplateCommand _updateCommand;
        private TemplateCommand _deleteCommand;

        /*
         * Inherit from Component through DbDataAdapter. The event
         * mechanism is designed to work with the Component.Events
         * property. These variables are the keys used to find the
         * events in the components list of events.
         */
        static private readonly object EventRowUpdated = new object();
        static private readonly object EventRowUpdating = new object();        

        public TemplateCommand SelectCommand
        {
            get { return _selectCommand; }
            set { _selectCommand = value; }
        }

        IDbCommand IDbDataAdapter.SelectCommand
        {
            get { return _selectCommand; }
            set { _selectCommand = (TemplateCommand)value; }
        }

        public TemplateCommand InsertCommand
        {
            get { return _insertCommand; }
            set { _insertCommand = value; }
        }

        IDbCommand IDbDataAdapter.InsertCommand
        {
            get { return _insertCommand; }
            set { _insertCommand = (TemplateCommand)value; }
        }

        public TemplateCommand UpdateCommand
        {
            get { return _updateCommand; }
            set { _updateCommand = value; }
        }

        IDbCommand IDbDataAdapter.UpdateCommand
        {
            get { return _updateCommand; }
            set { _updateCommand = (TemplateCommand)value; }
        }

        public TemplateCommand DeleteCommand
        {
            get { return _deleteCommand; }
            set { _deleteCommand = value; }
        }

        IDbCommand IDbDataAdapter.DeleteCommand
        {
            get { return _deleteCommand; }
            set { _deleteCommand = (TemplateCommand)value; }
        }

        /*
         * Implement abstract methods inherited from DbDataAdapter.
         */
        override protected RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
        {
            return new TemplateRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
        }

        override protected RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
        {
            return new TemplateRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
        }

        override protected void OnRowUpdating(RowUpdatingEventArgs value)
        {
            TemplateRowUpdatingEventHandler handler = (TemplateRowUpdatingEventHandler)Events[EventRowUpdating];
            if ((null != handler) && (value is TemplateRowUpdatingEventArgs))
            {
                handler(this, (TemplateRowUpdatingEventArgs)value);
            }
        }

        override protected void OnRowUpdated(RowUpdatedEventArgs value)
        {
            TemplateRowUpdatedEventHandler handler = (TemplateRowUpdatedEventHandler)Events[EventRowUpdated];
            if ((null != handler) && (value is TemplateRowUpdatedEventArgs))
            {
                handler(this, (TemplateRowUpdatedEventArgs)value);
            }
        }

        public event TemplateRowUpdatingEventHandler RowUpdating
        {
            add { Events.AddHandler(EventRowUpdating, value); }
            remove { Events.RemoveHandler(EventRowUpdating, value); }
        }

        public event TemplateRowUpdatedEventHandler RowUpdated
        {
            add { Events.AddHandler(EventRowUpdated, value); }
            remove { Events.RemoveHandler(EventRowUpdated, value); }
        }
    }

    public delegate void TemplateRowUpdatingEventHandler(object sender, TemplateRowUpdatingEventArgs e);
    public delegate void TemplateRowUpdatedEventHandler(object sender, TemplateRowUpdatedEventArgs e);

    public class TemplateRowUpdatingEventArgs : RowUpdatingEventArgs
    {
        public TemplateRowUpdatingEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
            : base(row, command, statementType, tableMapping)
        {
        }

        // Hide the inherited implementation of the command property.
        new public TemplateCommand Command
        {
            get { return (TemplateCommand)base.Command; }
            set { base.Command = value; }
        }
    }

    public class TemplateRowUpdatedEventArgs : RowUpdatedEventArgs
    {
        public TemplateRowUpdatedEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
            : base(row, command, statementType, tableMapping)
        {
        }

        // Hide the inherited implementation of the command property.
        new public TemplateCommand Command
        {
            get { return (TemplateCommand)base.Command; }
        }
    }
}