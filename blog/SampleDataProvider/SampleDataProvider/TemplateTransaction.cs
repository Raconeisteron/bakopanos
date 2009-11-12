﻿using System;
using System.Data;

namespace DotNetDataProviderTemplate
{
    public class TemplateTransaction : IDbTransaction
    {
        public IsolationLevel IsolationLevel
        {
            /*
             * Should return the current transaction isolation
             * level. For the template, assume the default
             * which is ReadCommitted.
             */
            get { return IsolationLevel.ReadCommitted; }
        }

        public void Commit()
        {
            /*
             * Implement Commit here. Although the template does
             * not provide an implementation, it should never be 
             * a no-op because data corruption could result.
             */
        }

        public void Rollback()
        {
            /*
             * Implement Rollback here. Although the template does
             * not provide an implementation, it should never be
             * a no-op because data corruption could result.
             */
        }

        public IDbConnection Connection
        {
            /*
             * Return the connection for the current transaction.
             */

            get { return this.Connection; }
        }

        public void Dispose()
        {
            this.Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (null != this.Connection)
                {
                    // implicitly rollback if transaction still valid
                    this.Rollback();
                }
            }
        }

    }
}